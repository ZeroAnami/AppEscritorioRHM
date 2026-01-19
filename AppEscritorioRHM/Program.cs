using AppEscritorioRHM.Controls;
using AppEscritorioRHM.Core.Interfaces;
using AppEscritorioRHM.Core.Models;
using AppEscritorioRHM.Core.Services;
using AppEscritorioRHM.Core.Utilities;
using AppEscritorioRHM.Properties;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http.Headers;
using System.Text;

namespace AppEscritorioRHM
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            Application.SetDefaultFont(new Font(new FontFamily("Microsoft Sans Serif"), 9f));
            var services = new ServiceCollection();
            ConfigureServices(services);
            var serviceProvider = services.BuildServiceProvider();

            bool loginOk = checkLoging(serviceProvider).GetAwaiter().GetResult();
            if (!loginOk) return; //Verifica si la configuración básica es correcta

            var settings = serviceProvider.GetRequiredService<AppSettings>();
            settings.DominioWeb = SecurityService.Decrypt(Settings.Default.Dominio);
            Uri myUri = new Uri(settings.DominioWeb);
            var mainForm = serviceProvider.GetRequiredService<MainForm>();
            mainForm.Text = $"Gestión de productos {myUri.Host}";
           
            Application.Run(mainForm);
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            // Configuración
            services.AddSingleton<AppSettings>();
            services.AddTransient<RedirecctionUrlUserControl>();
            services.AddTransient<MainForm>();

            // Servicios
            services.AddTransient<IBrandMapper, BrandMapperService>();
            services.AddTransient<IRedirectService, RedirectCsvService>();
            services.AddTransient<IProductService, ProductWooService>();

            services.AddHttpClient<IWCService, WCService>(client =>
            {
                client.BaseAddress = new Uri($"{SecurityService.Decrypt(Settings.Default.Dominio)}{WCEndpoints.EndpointWC}");
                client.Timeout = TimeSpan.FromMinutes(10);

                string key = SecurityService.Decrypt(Settings.Default.yourConsumerKeyWC);
                string secret = SecurityService.Decrypt(Settings.Default.yourConsumerSecretWC);

                if (!string.IsNullOrEmpty(key))
                {
                    var authBytes = Encoding.ASCII.GetBytes($"{key}:{secret}");
                    client.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authBytes));
                }
            });

            services.AddHttpClient<IWPService, WPService>(client =>
            {
                client.BaseAddress = new Uri($"{SecurityService.Decrypt(Settings.Default.Dominio)}{WCEndpoints.EndpointWP}");
                client.Timeout = TimeSpan.FromMinutes(10);

                string key = SecurityService.Decrypt(Settings.Default.yourConsumerKeyWP);
                string secret = SecurityService.Decrypt(Settings.Default.yourConsumerSecretWP);

                if (!string.IsNullOrEmpty(key))
                {
                    var authBytes = Encoding.ASCII.GetBytes($"{key}:{secret}");
                    client.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authBytes));
                }
            });
        }

        private async static Task<bool> checkLoging(ServiceProvider serviceProvider)
        {
            if (String.IsNullOrEmpty(Settings.Default.Dominio) ||
                String.IsNullOrEmpty(Settings.Default.yourConsumerKeyWC) ||
                String.IsNullOrEmpty(Settings.Default.yourConsumerSecretWC) ||
                String.IsNullOrEmpty(Settings.Default.yourConsumerKeyWP) ||
                String.IsNullOrEmpty(Settings.Default.yourConsumerSecretWP)
                )
            {
                var formConfig = new FormConfig();
                DialogResult result = formConfig.ShowDialog();
                if (result != DialogResult.OK) return false;
            }

            bool connectionCheck = false;
            while (!connectionCheck)
            {
                try
                {
                    if (string.IsNullOrEmpty(Settings.Default.Dominio))
                    {
                        connectionCheck = false;
                    }
                    else
                    {
                        var wcService = serviceProvider.GetRequiredService<IWCService>();
                        var wpService = serviceProvider.GetRequiredService<IWPService>();

                        bool connectionCheckWC = await wcService.CheckConnectionAsync();
                        bool connectionCheckWP = await wpService.CheckConnectionAsync();
                        connectionCheck = connectionCheckWC && connectionCheckWP;
                    }
                }
                catch
                {
                    connectionCheck = false;
                }

                if (connectionCheck) return true;

                DialogResult messageBoxResult = MessageBox.Show(
                    "No se puede conectar con WooCommerce.\n¿Quieres abrir la configuración?",
                    "Error de Conexión",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                    );
                if (messageBoxResult == DialogResult.No) return false;

                var formConfig = new FormConfig();
                DialogResult configResult = formConfig.ShowDialog();
                if (configResult != DialogResult.OK) return false;
            }
            return false;
        }
    }
}