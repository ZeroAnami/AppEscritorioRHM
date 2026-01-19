using AppEscritorioRHM.Controls;
using AppEscritorioRHM.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Windows.Forms;

namespace AppEscritorioRHM
{
    public partial class MainForm : Form
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IProductService _productService;
        private List<UserControl> backPanels = new List<UserControl>();
        private UserControl actualPanel;

        public MainForm(IServiceProvider serviceProvider, IProductService productService)
        {
            _serviceProvider = serviceProvider;
            _productService = productService;

            InitializeComponent();
            MostrarImportacion();
        }

        private void MostrarImportacion()
        {
            panelMain.Controls.Clear();
            var importControl = ActivatorUtilities.CreateInstance<MainUserControl>(_serviceProvider, ChangeUserControl);
            importControl.Dock = DockStyle.Fill;
            backPanels.Add(importControl);
            panelMain.Controls.Add(importControl);
        }

        private void Form1_Load(object sender, EventArgs e)
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

        private void buttonConfig_Click(object sender, EventArgs e)
        {
            var formConfig = new FormConfig();
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

            } else
                MessageBox.Show(
                    "No se han aplicado cambios.",
                    "Configuración fallida",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
        }
    }
}
