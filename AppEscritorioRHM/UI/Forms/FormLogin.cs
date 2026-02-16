using AppEscritorioRHM.Core.Application;
using AppEscritorioRHM.Core.Entities;
using AppEscritorioRHM.Core.Interfaces.Infrastructure;
using AppEscritorioRHM.Core.Interfaces.Services;
using AppEscritorioRHM.Core.Utilities;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AppEscritorioRHM.UI.Forms
{
    public partial class FormLogin : Form
    {
        private readonly IUserProfileHandle _userProfileHandle;
        private readonly IServiceProvider _serviceProvider;

        public FormLogin(IUserProfileHandle userProfileHandle, IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _userProfileHandle = userProfileHandle;
            txtUser.Text = Properties.Settings.Default.userSelected;
            txtUser.Focus();
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            string user = txtUser.Text.Trim();
            string pass = txtPass.Text.Trim();

            if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(pass))
            {
                MessageBox.Show("Por favor, introduce usuario y contraseña.", "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Deshabilitar UI para evitar doble clic
            ToggleControls(false);
            lblStatus.Text = "Verificando...";
            lblStatus.Visible = true;

            try
            {
                var result = await _userProfileHandle.LoginAsync(user, pass);

                if (result.Success)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show(result.ErrorMessage ?? "Credenciales incorrectas.", "Error de acceso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPass.Clear();
                    txtPass.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error inesperado: {ex.Message}", "Error crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Rehabilitar UI
                ToggleControls(true);
                lblStatus.Visible = false;
            }
        }

        private async void btnRegister_Click(object sender, EventArgs e)
        {
            string user = txtUser.Text.Trim();
            string pass = txtPass.Text;

            if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(pass))
            {
                MessageBox.Show("Para registrarte, escribe el usuario y contraseña que deseas usar.", "Registro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var confirm = MessageBox.Show($"¿Deseas crear un nuevo usuario '{user}'?", "Confirmar registro", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            ToggleControls(false);

            try
            {
                var result = await _userProfileHandle.RegisterUserAsync(user, pass);

                if (result.Success)
                    MessageBox.Show("Usuario registrado correctamente. Ahora puedes iniciar sesión.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show($"No se pudo registrar: {result.ErrorMessage}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al registrar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ToggleControls(true);
            }
        }

        private void chkShowPass_CheckedChanged(object sender, EventArgs e) =>
            txtPass.UseSystemPasswordChar = !chkShowPass.Checked;

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void ToggleControls(bool enable)
        {
            btnLogin.Enabled = enable;
            btnRegister.Enabled = enable;
            txtUser.Enabled = enable;
            txtPass.Enabled = enable;
        }
    }
}
