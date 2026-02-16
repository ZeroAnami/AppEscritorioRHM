using AppEscritorioRHM.Controls;
using AppEscritorioRHM.Core.Application;
using AppEscritorioRHM.Core.Entities;
using AppEscritorioRHM.Core.Interfaces.Infrastructure;
using AppEscritorioRHM.Core.Interfaces.Services;
using AppEscritorioRHM.Core.Services;
using AppEscritorioRHM.Infrastructure.Data;
using AppEscritorioRHM.UI.Forms;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Windows.Forms;

namespace AppEscritorioRHM
{
    public partial class MainForm : Form
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IUserProfileHandle _userProfileHandle;
        private readonly IEcommerceConnectionValidator _ecommerceConnectionValidator;
        private List<UserControl> backPanels = [];
        private UserControl? actualPanel;

        public MainForm(IServiceProvider serviceProvider, IUserProfileHandle userProfileHandle, IEcommerceConnectionValidator ecommerceConnectionValidator)
        {
            _serviceProvider = serviceProvider;
            _userProfileHandle = userProfileHandle;
            _ecommerceConnectionValidator = ecommerceConnectionValidator;
            InitializeComponent();
            MostrarImportacion();
        }

        protected override async void OnShown(EventArgs e)
        {
            base.OnShown(e);

            var checkConnection = await CheckEcommerceConnections();
            if (!checkConnection)
            {
                MessageBox.Show(
                    "No se pudieron validar las conexiones de comercio electrónico. La aplicación se cerrará.",
                    "Error crítico",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                this.Close();
                return;
            }
        }

        private async Task<bool> CheckEcommerceConnections()
        {
            var projectSelected = _userProfileHandle.GetProjectSelected();

            if (projectSelected is null)
            {
                MessageBox.Show(
                    "No se ha seleccionado un proyecto. Por favor, selecciona un proyecto para continuar.",
                    "Proyecto no seleccionado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                var formConfig = _serviceProvider.GetService<FormConfig>();
                DialogResult configResult = formConfig.ShowDialog();
                return configResult == DialogResult.OK;
            }

            ContextResult result = await _ecommerceConnectionValidator.ValidateAllEndpointsAsync(projectSelected);

            if (!result.Success)
            {
                MessageBox.Show(
                    $"Error al validar las conexiones de comercio electrónico: {result.ErrorMessage}",
                    "Error de validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                var formConfig = _serviceProvider.GetService<FormConfig>();
                DialogResult configResult = formConfig.ShowDialog();
                return configResult == DialogResult.OK;
            }

            return true;
        }

        private void MostrarImportacion()
        {
            panelMain.Controls.Clear();
            var importControl = ActivatorUtilities.CreateInstance<MainUserControl>(_serviceProvider, ChangeUserControl);
            importControl.Dock = DockStyle.Fill;
            backPanels.Add(importControl);
            panelMain.Controls.Add(importControl);
            actualPanel = importControl;
            this.Text = "Gestor de E-commerces de " + _userProfileHandle!.GetUserName();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {

        }

        private void panelMain_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ChangeUserControl(UserControl userControlChange)
        {
            if (userControlChange is null)
                return;
            panelMain.Controls.Clear();
            panelMain.Controls.Add(userControlChange);
            int index = backPanels.IndexOf(actualPanel);
            // Al agregar un nuevo UserControl, quita los que están adelante (porque el usuario retrocedió antes)
            if (index < backPanels.Count - 1)
            {
                backPanels.RemoveRange(index + 1, backPanels.Count - 1);
            }

            backPanels.Add(userControlChange);
            actualPanel = userControlChange;

            btnUndo.Enabled = backPanels.IndexOf(actualPanel) > 0;
            btnRedo.Enabled = backPanels.IndexOf(actualPanel) < backPanels.Count - 1;
        }

        private void btnUndo_Click(object sender, EventArgs e)
        {
            int index = backPanels.IndexOf(actualPanel);
            //Comprueba si hay UserConrtol más atrás del actual para retroceder
            if (index > 0)
            {
                var previousPanel = backPanels[index - 1];
                ChangeUserControl(previousPanel);
                actualPanel = previousPanel;
            }
        }


        private void btnRedo_Click(object sender, EventArgs e)
        {
            int index = backPanels.IndexOf(actualPanel);
            //Comprueba si hay UserConrtol más adelante del actual para avanzar
            if (index < backPanels.Count - 1)
            {
                var previousPanel = backPanels[index + 1];
                ChangeUserControl(previousPanel);
                actualPanel = previousPanel;
            }
        }

        private void tableLayoutPanelMain_Paint(object sender, PaintEventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanelControlButtons_Paint(object sender, PaintEventArgs e)
        {

        }

        private async void buttonConfig_Click(object sender, EventArgs e)
        {
            var formConfig = _serviceProvider.GetService<FormConfig>();
            DialogResult configResult = formConfig.ShowDialog();
            if (configResult == DialogResult.OK)
            {
                string message = "Configuración guardada correctamente.";
                if (actualPanel is not MainUserControl)
                    message += "\nVuelve a cargar el módulo para aplicar los cambios.";
                MessageBox.Show(
                    message,
                    "Configuración correcta",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else
            {
                bool result = await CheckEcommerceConnections();

                if (!result)
                {
                    MessageBox.Show(
                        "No se pudieron validar las conexiones de comercio electrónico. La aplicación se cerrará.",
                        "Error crítico",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

                MessageBox.Show(
                    "No se han aplicado cambios.",
                    "Advertencia",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }
        }
    }
}
