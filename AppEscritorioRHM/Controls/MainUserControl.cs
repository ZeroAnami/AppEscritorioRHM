using AppEscritorioRHM.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using static AppEscritorioRHM.Controls.RedirecctionUrlUserControl;

namespace AppEscritorioRHM.Controls
{
    public partial class MainUserControl : UserControl
    {
        Action<UserControl> changeUserControl;
        private readonly IServiceProvider _serviceProvider;

        public MainUserControl(Action<UserControl> changeUserControl, IServiceProvider serviceProvider)
        {
            InitializeComponent();
            this.changeUserControl = changeUserControl;
            this.Dock = DockStyle.Fill;
            _serviceProvider = serviceProvider;
        }

        private void MainUserControl_Load(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void buttonChangePanel_Click(object sender, EventArgs e)
        {
            var importControl = ActivatorUtilities.CreateInstance<RedirecctionUrlUserControl>(_serviceProvider);
            changeUserControl(importControl);
        }

        private void panelMain_Paint(object sender, PaintEventArgs e)
        {
            
        }
    }
}
