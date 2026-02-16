using AppEscritorioRHM.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AppEscritorioRHM.UI.Forms
{
    public partial class FormPasswordPrompt : Form
    {
        private readonly IUserProfileHandle _userProfileHandle;

        // Propiedad pública para recuperar la contraseña si es válida
        public string? Password { get; private set; }

        public FormPasswordPrompt(IUserProfileHandle userProfileHandle)
        {
            InitializeComponent();
            _userProfileHandle = userProfileHandle;

            // Configuraciones visuales rápidas
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.AcceptButton = btnOk;
            this.CancelButton = btnCancel;
        }

        private async void btnOk_Click(object sender, EventArgs e)
        {
            string pass = txtPass.Text;

            if (string.IsNullOrEmpty(pass))
            {
                MessageBox.Show("Introduce tu contraseña.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 1. Validamos contra el usuario actual
            var result = await _userProfileHandle.ValidatePassword(pass);
            if (result.Success)
            {
                // Si es correcta, la guardamos en la propiedad pública y cerramos con OK
                this.Password = pass;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Contraseña incorrecta.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPass.SelectAll();
                txtPass.Focus();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.Password = null;
                txtPass?.Text = string.Empty;
                components?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
