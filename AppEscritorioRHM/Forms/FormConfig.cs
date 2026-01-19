using AppEscritorioRHM.Core.Services;
using AppEscritorioRHM.Core.Utilities;
using AppEscritorioRHM.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net.Http.Headers;
using System.Text;
using System.Windows.Forms;

namespace AppEscritorioRHM
{
    public partial class FormConfig : Form
    {
        public FormConfig()
        {
            InitializeComponent();
            LoadSettings();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private async void buttonCheck_Click(object sender, EventArgs e)
        {
            buttonCheck.Enabled = false;
            errorProvider1.Clear();
            progressBarLoading.Visible = true;

            try
            {
                string dominioWeb = NormalizeDomain(textBoxDomain.Text);

                // Validar WooCommerce
                bool wcOk = await ValidateServiceConnection(
                    dominioWeb + WCEndpoints.EndpointWC,
                    textBoxTokenPublicWC.Text,
                    textBoxTokenSecretWC.Text,
                    client => new WCService(client).CheckConnectionAsync(),
                    "WooCommerce");

                if (!wcOk) return;

                // Validar WordPress
                bool wpOk = await ValidateServiceConnection(
                    dominioWeb + WCEndpoints.EndpointWP,
                    textBoxTokenPublicWP.Text,
                    textBoxTokenSecretWP.Text,
                    client => new WPService(client).CheckConnectionAsync(),
                    "WordPress");

                if (!wpOk) return;

                // Guardado de configuración (Permanecer igual...)
                SaveSettings(dominioWeb);

                MessageBox.Show("Conexión exitosa", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                var correlationId = Guid.NewGuid().ToString();
                errorProvider1.SetError(buttonCheck, $"Error inesperado. ID: {correlationId}");
                MessageBox.Show($"Error: {ex.Message}\nID: {correlationId}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                buttonCheck.Enabled = true;
                progressBarLoading.Visible = false;
            }
        }

        private string NormalizeDomain(string domain)
        {
            domain = domain.Trim().TrimEnd('/');
            if (!domain.StartsWith("http", StringComparison.OrdinalIgnoreCase))
            {
                domain = "https://" + domain;
            }
            return domain;
        }

        private async Task<bool> ValidateServiceConnection(
            string url,
            string key,
            string secret,
            Func<HttpClient, Task<bool>> checkAction,
            string serviceName)
        {
            try
            {
                using var client = new HttpClient { BaseAddress = new Uri(url), Timeout = TimeSpan.FromMinutes(3) };

                if (!string.IsNullOrEmpty(key))
                {
                    var authBytes = Encoding.ASCII.GetBytes($"{key}:{secret}");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authBytes));
                }

                if (!await checkAction(client))
                {
                    errorProvider1.SetError(buttonCheck, $"Error al conectar con {serviceName}.");
                    MessageBox.Show($"Error al conectar con {serviceName}.\nVerifique los datos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                errorProvider1.SetError(buttonCheck, $"Error de red en {serviceName}: {ex.Message}");
                return false;
            }
        }

        private void LoadSettings()
        {
            textBoxDomain.Text = SecurityService.Decrypt(Settings.Default.Dominio);
            textBoxTokenPublicWC.Text = SecurityService.Decrypt(Settings.Default.yourConsumerKeyWC);
            textBoxTokenSecretWC.Text = SecurityService.Decrypt(Settings.Default.yourConsumerSecretWC);
            textBoxTokenPublicWP.Text = SecurityService.Decrypt(Settings.Default.yourConsumerKeyWP);
            textBoxTokenSecretWP.Text = SecurityService.Decrypt(Settings.Default.yourConsumerSecretWP);
        }
        
        private void SaveSettings(string dominio)
        {
            Settings.Default.Dominio = SecurityService.Encrypt(dominio);
            Settings.Default.yourConsumerKeyWC = SecurityService.Encrypt(textBoxTokenPublicWC.Text);
            Settings.Default.yourConsumerSecretWC = SecurityService.Encrypt(textBoxTokenSecretWC.Text);
            Settings.Default.yourConsumerKeyWP = SecurityService.Encrypt(textBoxTokenPublicWP.Text);
            Settings.Default.yourConsumerSecretWP = SecurityService.Encrypt(textBoxTokenSecretWP.Text);
            Settings.Default.Save();
        }

        private void labelTokenSecretWC_Click(object sender, EventArgs e)
        {

        }

        private void textBoxDomain_TextChanged(object sender, EventArgs e)
        {
            textChanged();
        }

        private void textBoxTokenPublicWC_TextChanged(object sender, EventArgs e)
        {
            textChanged();
        }

        private void textBoxTokenSecretWC_TextChanged(object sender, EventArgs e)
        {
            textChanged();
        }

        private void textBoxTokenPublicWP_TextChanged(object sender, EventArgs e)
        {
            textChanged();
        }

        private void textBoxTokenSecretWP_TextChanged(object sender, EventArgs e)
        {
            textChanged();
        }

        private void textChanged()
        {
            buttonCheck.Enabled = !String.IsNullOrEmpty(textBoxDomain.Text) &&
                !String.IsNullOrEmpty(textBoxTokenPublicWC.Text) &&
                !String.IsNullOrEmpty(textBoxTokenSecretWC.Text) &&
                !String.IsNullOrEmpty(textBoxTokenPublicWP.Text) &&
                !String.IsNullOrEmpty(textBoxTokenSecretWP.Text);

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void labelTokenSecretWP_Click(object sender, EventArgs e)
        {

        }
    }
    // Interfaz auxiliar interna para unificar la validación en el formulario
    interface IConnectionChecker { Task<bool> CheckConnectionAsync(); }
}
