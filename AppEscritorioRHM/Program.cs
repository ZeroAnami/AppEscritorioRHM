using AppEscritorioRHM.Controls;
using AppEscritorioRHM.Controls.Config;
using AppEscritorioRHM.Core.Application;
using AppEscritorioRHM.Core.Entities;
using AppEscritorioRHM.Core.Interfaces.Infrastructure;
using AppEscritorioRHM.Core.Interfaces.Services;
using AppEscritorioRHM.Core.Services;
using AppEscritorioRHM.Core.Utilities;
using AppEscritorioRHM.Forms;
using AppEscritorioRHM.Infrastructure.Data;
using AppEscritorioRHM.Infrastructure.ExternalServices.Woo;
using AppEscritorioRHM.Properties;
using AppEscritorioRHM.UI.Forms;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;

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

            DatabaseHelper.InitializeDatabase();
            var services = new ServiceCollection();
            ConfigureServices(services);
            var serviceProvider = services.BuildServiceProvider();

            using var scope = serviceProvider.CreateScope();
            try
            {
                // Login
                var loginForm = scope.ServiceProvider.GetRequiredService<FormLogin>();
                var loginResult = loginForm.ShowDialog();

                if (loginResult == DialogResult.OK)
                {
                    var mainForm = scope.ServiceProvider.GetRequiredService<MainForm>();
                    Application.Run(mainForm);
                }
                else
                {
                    // Usuario canceló o cerró el login.
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fatal en el inicio: {ex.Message}", "Error Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            // Infraestructura
            services.AddTransient<IUserConnecction, UserConnectionSqlite>();

            // Configuración
            services.AddSingleton<AppSettings>();
            services.AddSingleton<IUserProfileHandle, UserProfileHandle>();
            services.AddSingleton<IEcommerceServiceManager, EcommerceServiceManager>();

            // Servicios
            services.AddTransient<IBrandMapper, BrandMapperService>();
            services.AddTransient<IRedirectService, RedirectCsvService>();
            services.AddTransient<IEcommerceConnectionValidator, EcommerceConnectionValidator>();

            // Capa de presentación
            services.AddTransient<FormLogin>();
            services.AddTransient<MainForm>();
            services.AddTransient<FormConfig>();
            services.AddTransient<FormImageSelector>();
            services.AddTransient<FormPasswordPrompt>();

            services.AddTransient<TokenConfigUserControl>();
            services.AddTransient<RedirecctionUrlUserControl>();
            services.AddTransient<MainUserControl>();
        }
    }
}