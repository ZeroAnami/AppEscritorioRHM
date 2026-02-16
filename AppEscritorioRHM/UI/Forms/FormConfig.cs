using AppEscritorioRHM.Core.Application;
using AppEscritorioRHM.Core.Entities;
using AppEscritorioRHM.Core.Interfaces.Infrastructure;
using AppEscritorioRHM.Core.Interfaces.Services;
using AppEscritorioRHM.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AppEscritorioRHM.UI.Forms
{
    public partial class FormConfig : Form
    {
        private readonly IUserProfileHandle _userProfileHandle;
        private readonly IEcommerceConnectionValidator _ecommerceConnectionValidator;
        private readonly IEcommerceServiceManager _ecommerceServiceManager;
        private readonly IServiceProvider _serviceProvider;
        private ProjectConfiguration? _selectedProject; // La tienda seleccionada
        private string? _selectedApiId; // La API que estamos editando actualmente dentro de esa tienda
        private bool _isRefreshing; // Evita que SelectedIndexChanged sobreescriba _selectedProject durante el refresco
        private string? _passwordForSensitiveActions; // Variable para almacenar la contraseña validada temporalmente

        // Diccionario temporal para guardar el estado de las validaciones de conexión (UI Effect)
        private Dictionary<string, bool> _connectionStatus = new Dictionary<string, bool>();

        public FormConfig(IUserProfileHandle userProfileHandle, IEcommerceConnectionValidator ecommerceConnectionValidator, IEcommerceServiceManager ecommerceServiceManager, IServiceProvider serviceProvider)
        {
            _userProfileHandle = userProfileHandle;
            _ecommerceConnectionValidator = ecommerceConnectionValidator;
            _ecommerceServiceManager = ecommerceServiceManager;
            _serviceProvider = serviceProvider;

            InitializeComponent();

            // Cargar plataformas disponibles
            cmbPlatform.DataSource = Ecommerces.GetAllSoportedEcommerces();
            cmbPlatform.DisplayMember = "Name";
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            await LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            if (!_userProfileHandle.IsLoggedIn())
            {
                MessageBox.Show("Sesión no iniciada.");
                this.Close();
                return;
            }
            _selectedProject = _userProfileHandle.GetProjectSelected();
            RefreshStoreList();
        }

        private void RefreshStoreList(bool clearConnectionStatus = true)
        {
            _isRefreshing = true;
            if (clearConnectionStatus)
                _connectionStatus.Clear();
            try
            {
                lstStores.DataSource = null;
                lstStores.DataSource = _userProfileHandle.GetProjects();
                lstStores.DisplayMember = "ProjectName";

                if (_selectedProject != null)
                {
                    var match = _userProfileHandle.GetProjects()
                        .Select((p, i) => new { Project = p, Index = i })
                        .FirstOrDefault(x => x.Project.ProjectId == _selectedProject.ProjectId);

                    if (match != null)
                    {
                        lstStores.SelectedIndex = match.Index;
                        _selectedProject = (ProjectConfiguration)match.Project.Clone();
                        EnableRightPanel();
                        return;
                    }
                }

                if (lstStores.Items.Count > 0)
                {
                    lstStores.SelectedIndex = 0;
                    var selectedItem = lstStores.SelectedItem as ProjectConfiguration;
                    if (selectedItem != null)
                    {
                        _selectedProject = (ProjectConfiguration)selectedItem.Clone();
                        EnableRightPanel();
                    }
                }
                else
                {
                    _selectedProject = null;
                    ClearRightPanel();
                }
            }
            finally
            {
                _isRefreshing = false;
                // Disparar manualmente la carga de UI con el _selectedProject correcto
                if (_selectedProject != null)
                    LoadStoreUI();
            }
        }

        private void lstStores_SelectedIndexChanged(object sender, EventArgs e)
        {
             
            if (_isRefreshing) return; // Ignorar durante refresco
            // Limpiar estados de conexión al cambiar de tienda
            _connectionStatus.Clear();
            var project = (lstStores.SelectedItem as ProjectConfiguration)?.Clone() as ProjectConfiguration;
            if (project == null) return;
            _selectedProject = project;

            EnableRightPanel();
            LoadStoreUI();
        }

        private void LoadStoreUI()
        {
            if (_selectedProject == null) return;

            txtStoreName.Text = _selectedProject.ProjectName;
            txtStoreUrl.Text = _selectedProject.Domain;

            foreach (var item in cmbPlatform.Items)
            {
                if (((IEcommercePlatform)item).Name == _selectedProject.EcommerceIdSelected)
                {
                    cmbPlatform.SelectedItem = item;
                    break;
                }
            }

            RenderApiButtons();
        }

        private void RenderApiButtons()
        {
            if (_selectedProject is null) return;
            pnlApiSelector.Controls.Clear();

            // Obtenemos los endpoints disponibles para esta plataforma
            // Si el proyecto ya tiene configuraciones, iteramos sobre ellas. 
            // Si es nuevo, usamos los defaults de la clase Ecommerces

            if (_selectedProject.ConnectionsTokens == null || _selectedProject.ConnectionsTokens.Count == 0)
            {
                // Inicializar tokens vacíos si no existen
                var platform = cmbPlatform.SelectedItem as IEcommercePlatform;
                if (platform != null)
                {
                    // Aquí deberías tener lógica para inicializar los tokens por defecto de la plataforma
                    // Simplificación: Asumimos que ya vienen en el objeto o se crean
                }
            }

            foreach (var connection in _selectedProject.ConnectionsTokens)
            {
                Button btnApi = new Button();
                btnApi.Text = connection.EndpointId;
                btnApi.Tag = connection.EndpointId;
                btnApi.AutoSize = true;
                btnApi.Padding = new Padding(5);
                btnApi.FlatStyle = FlatStyle.Flat;
                btnApi.Margin = new Padding(3);

                // Efecto visual de error si falló antes
                if (_connectionStatus.ContainsKey(connection.EndpointId))
                {
                    btnApi.BackColor = _connectionStatus[connection.EndpointId]
                        ? Color.LightGreen // CORRECTO 
                        : Color.Salmon; // ERROR

                    btnApi.ForeColor = Color.White;
                }
                else
                {
                    btnApi.BackColor = _selectedApiId == connection.EndpointId
                        ? Color.LightGray // SELECCIONADO
                        : Color.WhiteSmoke; // NORMAL

                    btnApi.ForeColor = Color.Black;
                }

                btnApi.Click += (s, e) => SelectApi(connection.EndpointId);
                pnlApiSelector.Controls.Add(btnApi);
            }

            // Auto-seleccionar sin llamar a SelectApi (rompe el ciclo)
            if (string.IsNullOrEmpty(_selectedApiId) && _selectedProject.ConnectionsTokens.Count > 0)
            {
                _selectedApiId = _selectedProject.ConnectionsTokens[0].EndpointId;
            }

            LoadApiFields();
        }

        private void SelectApi(string endpointId)
        {
            SaveCurrentApiToMemory();
            _selectedApiId = endpointId;
            RenderApiButtons(); // Re-renderiza con el color actualizado
        }

        private void LoadApiFields()
        {
            if (string.IsNullOrEmpty(_selectedApiId)) return;

            var tokens = _selectedProject.ConnectionsTokens.FirstOrDefault(x => x.EndpointId == _selectedApiId);
            if (tokens != null)
            {
                txtConsumerKey.Text = tokens.ConsumerKey;
                txtConsumerSecret.Text = tokens.ConsumerSecret;
                grpApiDetails.Text = $"Credenciales para: {_selectedApiId}";
                btnVerify.Text = $"Verificar Conexión{" " + _selectedApiId ?? ""}";
            }
        }

        private void SaveCurrentApiToMemory()
        {
            if (_selectedProject == null || string.IsNullOrEmpty(_selectedApiId)) return;
            // Guardamos lo que hay en los textbox en el objeto en memoria
            var tokens = _selectedProject.ConnectionsTokens.FirstOrDefault(x => x.EndpointId == _selectedApiId);
            if (tokens != null)
            {
                tokens.ConsumerKey = txtConsumerKey.Text.Trim();
                tokens.ConsumerSecret = txtConsumerSecret.Text.Trim();
            }

            // Guardamos datos de la tienda también
            _selectedProject.ProjectName = txtStoreName.Text.Trim();
            _selectedProject.Domain = txtStoreUrl.Text.Trim();
            var platform = cmbPlatform.SelectedItem as IEcommercePlatform;
            if (platform != null) _selectedProject.EcommerceIdSelected = platform.Name;
        }

        private async void btnVerify_Click(object sender, EventArgs e)
        {
            btnVerify.Enabled = false;
            btnVerify.Text = "Verificando...";

            try
            {
                Uri uri = new Uri(txtStoreUrl.Text.Trim()); //Salta excepción si no es una URL válida
                SaveCurrentApiToMemory();
                IEcommercePlatform? platform = cmbPlatform.SelectedItem as IEcommercePlatform;
                if (platform == null)
                {
                    MessageBox.Show("Selecciona una plataforma válida.");
                    return;
                }

                // Obtener el endpoint seleccionado
                var endpoint = platform.GetConnections().FirstOrDefault(x => x.EndpointId == _selectedApiId);
                if (endpoint == null)
                {
                    MessageBox.Show("No se encontró el endpoint seleccionado.");
                    return;
                }

                var result = await _ecommerceConnectionValidator.ValidateEndpointAsync(endpoint, _selectedProject);
                bool success = result.Success;

                // Guardar estado visual
                if (!_connectionStatus.TryAdd(_selectedApiId, success)) { }
                _connectionStatus[_selectedApiId] = success;
                if (success)
                    MessageBox.Show("Conexión exitosa", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Falló la conexión", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (UriFormatException)
            {
                MessageBox.Show("La URL de la tienda no es válida.", "Error de URL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
                _connectionStatus[_selectedApiId] = false;
            }
            finally
            {
                SaveCurrentApiToMemory(); // Guardar cambios en local, guardar en BBDD solo al hacer click en Guardar
                btnVerify.Enabled = true;
                btnVerify.Text = $"Verificar Conexión{" " + _selectedApiId ?? ""}";
                RenderApiButtons(); // Actualizar colores de botones (Rojo si falló)
            }
        }

        private async void btnAddStore_Click(object sender, EventArgs e)
        {
            if (!RequestPassword()) return;
            string baseName = "Nueva Tienda";
            int count = 1;
            while (_userProfileHandle.GetProjects().Any(p => p.ProjectName == $"{baseName} {count}")) count++;

            var ecommerces = Ecommerces.GetAllSoportedEcommerces();
            if (ecommerces == null || ecommerces.Count == 0)
            {
                MessageBox.Show("No hay plataformas disponibles.");
                return;
            }

            var newStore = new ProjectConfiguration
            {
                ProjectName = $"{baseName} {count}",
                EcommerceIdSelected = ecommerces[0].Id,
                Domain = "https://",
                ConnectionsTokens = []
            };
            LoadAPIs(ecommerces[0], newStore);
            var result = await _userProfileHandle.AddProjectAsync(newStore, _passwordForSensitiveActions); // TODO: Pass si es necesario

            if (result.Success)
            {
                _selectedProject = (ProjectConfiguration)newStore.Clone();
                RefreshStoreList();
                EnableRightPanel();
            }
            else
            {
                MessageBox.Show("Error al agregar tienda: " + result.ErrorMessage);
            }
        }

        private async void btnDeleteStore_Click(object sender, EventArgs e)
        {
            if (!RequestPassword()) return;
            if (_selectedProject == null) return;

            var result = MessageBox.Show($"¿Eliminar tienda '{_selectedProject.ProjectName}'?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                // Llamar al servicio para borrar de BBDD
                var res = await _userProfileHandle.RemoveProjectAsync(_selectedProject.ProjectId, _passwordForSensitiveActions);

                if (res.Success)
                {
                    _selectedProject = null;
                    RefreshStoreList();
                }
                else
                {
                    MessageBox.Show("Error al borrar: " + res.ErrorMessage);
                }
            }
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (!RequestPassword()) return;
            if (!AllApisVerified())
            {
                var result = MessageBox.Show("No todas las conexiones han sido verificadas exitosamente. ¿Deseas guardar de todas formas?", "Confirmar guardado", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.No)
                    return;
            }

            SaveCurrentApiToMemory();

            if (_selectedProject != null)
            {
                var res = await _userProfileHandle.UpdateProjectAsync(_selectedProject, _passwordForSensitiveActions); // Necesitas gestionar la pass
                if (res.Success)
                {
                    RefreshStoreList(false);
                    MessageBox.Show("Configuración guardada.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("Error al guardar: " + res.ErrorMessage);
            }
        }

        private void ClearRightPanel()
        {
            txtStoreName.Text = "";
            txtStoreUrl.Text = "";
            pnlApiSelector.Controls.Clear();
            txtConsumerKey.Text = "";
            txtConsumerSecret.Text = "";
            grpStoreInfo.Enabled = false;
            grpApiDetails.Enabled = false;
        }

        private void EnableRightPanel()
        {
            grpStoreInfo.Enabled = true;
            grpApiDetails.Enabled = true;
        }

        private void chkShowSecret_CheckedChanged(object sender, EventArgs e)
        {
            if (chkShowSecret.Checked && !RequestPassword())
            {
                chkShowSecret.Checked = false;
                return;
            }
            txtConsumerSecret.UseSystemPasswordChar = !chkShowSecret.Checked;
        }
            

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbPlatform_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_selectedProject is null) return;
            IEcommercePlatform? selectedPlatform = cmbPlatform.SelectedItem as IEcommercePlatform;
            if (selectedPlatform == null || selectedPlatform.Id == _selectedProject.EcommerceIdSelected) return;
            _selectedProject.EcommerceIdSelected = selectedPlatform.Id;
            LoadAPIs(selectedPlatform, _selectedProject);
        }

        private void LoadAPIs(IEcommercePlatform platform, ProjectConfiguration? project)
        {
            try
            {
                if (project is null) return;
                project.ConnectionsTokens.Clear();
                if (platform is null) return;

                var endpoints = platform.GetConnections();
                foreach (var endpoint in endpoints)
                {
                    project.ConnectionsTokens.Add(
                        new Tokens
                        {
                            EndpointId = endpoint.EndpointId
                        }
                        );
                }
            }
            finally
            {
                RenderApiButtons();
            }
        }
        private bool AllApisVerified()
        {
            if (_selectedProject == null || _selectedProject.ConnectionsTokens.Count == 0)
                return false;

            foreach (var token in _selectedProject.ConnectionsTokens)
            {
                if (!_connectionStatus.TryGetValue(token.EndpointId, out bool success) || !success)
                    return false;
            }
            return true;
        }

        private bool RequestPassword()
        {
            if (!string.IsNullOrEmpty(_passwordForSensitiveActions))
                return true;

            using (var prompt = _serviceProvider.GetRequiredService<FormPasswordPrompt>())
            {
                var result = prompt.ShowDialog();
                if (result == DialogResult.OK)
                {
                    _passwordForSensitiveActions = prompt.Password;
                    return true;
                }
            }
            return false;
        }

        protected override async void OnFormClosing(FormClosingEventArgs e)
        {
            // Si no todas están verificadas y ninguna falló explícitamente, intentar auto-verificación
            if (!AllApisVerified())
            {
                bool anyExplicitlyFailed = _connectionStatus.Any(kvp => !kvp.Value);

                if (!anyExplicitlyFailed && _selectedProject != null)
                {
                    e.Cancel = true;
                    SaveCurrentApiToMemory();

                    // Feedback visual: deshabilitar formulario y mostrar estado
                    string originalTitle = this.Text;
                    this.Text = "Verificando conexiones, por favor espera...";
                    this.Enabled = false;
                    this.Cursor = Cursors.WaitCursor;

                    try
                    {
                        IEcommercePlatform? platform = cmbPlatform.SelectedItem as IEcommercePlatform;
                        if (platform != null)
                        {
                            var endpoints = platform.GetConnections();
                            foreach (var token in _selectedProject.ConnectionsTokens)
                            {
                                // Saltar las que ya fueron verificadas con éxito
                                if (_connectionStatus.TryGetValue(token.EndpointId, out bool alreadyOk) && alreadyOk)
                                    continue;

                                this.Text = $"Verificando conexión: {token.EndpointId}...";

                                var endpoint = endpoints.FirstOrDefault(ep => ep.EndpointId == token.EndpointId);
                                if (endpoint != null)
                                {
                                    var result = await _ecommerceConnectionValidator.ValidateEndpointAsync(endpoint, _selectedProject);
                                    _connectionStatus[token.EndpointId] = result.Success;
                                }
                                else
                                {
                                    _connectionStatus[token.EndpointId] = false;
                                }
                            }
                        }
                        else
                        {
                            // Sin plataforma no se puede verificar: marcar todas como fallidas para romper el ciclo
                            foreach (var token in _selectedProject.ConnectionsTokens)
                                _connectionStatus.TryAdd(token.EndpointId, false);
                        }
                    }
                    finally
                    {
                        this.Text = originalTitle;
                        this.Enabled = true;
                        this.Cursor = Cursors.Default;
                    }

                    RenderApiButtons();
                    this.Close(); // Re-dispara OnFormClosing con el estado actualizado
                    return;
                }
            }

            if (AllApisVerified())
            {
                try
                {
                    _ecommerceServiceManager.ConfigureForProject(_selectedProject!);
                    await _userProfileHandle.SetProjectSelectedAsync(_selectedProject!.ProjectId);
                    this.DialogResult = DialogResult.OK;
                    base.OnFormClosing(e);
                }
                catch (Exception ex)
                {
                    var result = MessageBox.Show($"Error al configurar servicios: {ex.Message}\n¿Deseas cerrar la aplicación?", "Fallo de configuración", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes)
                    {
                        this.DialogResult = DialogResult.Cancel;
                        base.OnFormClosing(e);
                    }
                    else
                        e.Cancel = true;
                }
            }
            else
            {
                // Mensaje personalizado que lista las APIs que fallaron
                List<string> failedApis = [];
                if (_selectedProject != null)
                {
                    foreach (var ct in _selectedProject.ConnectionsTokens)
                    {
                        if (!_connectionStatus.TryGetValue(ct.EndpointId, out bool success) || !success)
                            failedApis.Add(ct.EndpointId);
                    }
                }

                string failedApisString = "\"" + string.Join("\" \"", failedApis) + "\"";
                string message = _selectedProject == null
                    ? "Debes elegir una tienda. ¿Cerrar de todas formas?"
                    : $"Han fallado las verificaciones: {failedApisString} de la tienda '{_selectedProject.ProjectName}'." +
                    $"\nComprueba credenciales o elige otra tienda.\n¿Cerrar de todas formas?";
                var result = MessageBox.Show(message, "Confirmar cierre", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    this.DialogResult = DialogResult.Cancel;
                    base.OnFormClosing(e);
                }
                else
                    e.Cancel = true;
            }
        }
    }
}