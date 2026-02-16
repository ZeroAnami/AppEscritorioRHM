using AppEscritorioRHM.Core.Entities;
using AppEscritorioRHM.Core.Interfaces.Infrastructure;
using AppEscritorioRHM.Core.Interfaces.Services;
using AppEscritorioRHM.Core.Utilities;
using AppEscritorioRHM.Forms;
using AppEscritorioRHM.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AppEscritorioRHM.Controls
{
    public partial class RedirecctionUrlUserControl : UserControl
    {
        IRedirectService _redirectService;
        IEcommerceServiceManager _ecommerceServiceManager;
        string? _rutaArchivoEntrada = null;
        string? _rutaArchivoSalida = null;
        string? _archivoGenerado = null;
        private CancellationTokenSource? _cts = null;

        public RedirecctionUrlUserControl(IEcommerceServiceManager ecommerceServiceManager, IRedirectService redirectService)
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            _redirectService = redirectService;
            _ecommerceServiceManager = ecommerceServiceManager;
        }

        private IProductService? TryGetProductService()
        {
            if (!_ecommerceServiceManager.IsConfigured)
            {
                MessageBox.Show(
                    "No se ha configurado ningún servicio de ecommerce.\nVe a Configuración y selecciona una tienda verificada.",
                    "Error de configuración",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return null;
            }
            return _ecommerceServiceManager.GetProductService();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private async void buttonStartProgress_Click(object sender, EventArgs e)
        {
            //No hacer nada si no hay ninguna acción seleccionada
            if (!checkBoxRedirections.Checked && !checkBoxDeleteImages.Checked && !checkBoxDeleteProducts.Checked)
                return;

            var productService = TryGetProductService();
            if (productService is null) return;

            buttonStartProgress.Enabled = false;
            buttonDownload.Enabled = false;
            pictureBoxCheckRedirections.Visible = false;
            pictureBoxCheckDeleteImages.Visible = false;
            pictureBoxCheckDeleteProducts.Visible = false;
            richTextBoxReport.Clear();

            _cts = new CancellationTokenSource(); //Importante que exista el token antes de que pueda ser cancelado
            buttonCancel.Enabled = true;
            try
            {
                List<int> ids = productService.getIdsFromCsvAsync(_rutaArchivoEntrada);
                List<Product>? products = null;
                if (checkBoxRedirections.Checked)
                {
                    products = await getProducts(productService, ids, progressBarRedirections);
                    await redirection(products);
                    progressBarRedirections.Visible = false;
                    pictureBoxCheckRedirections.Visible = true;
                    LogUpdate("--- Redirecciones generadas con éxito ---");
                    buttonDownload.Enabled = true;
                }
                    
                if (checkBoxDeleteImages.Checked)
                {
                    if (!checkBoxRedirections.Checked)
                    {
                        DialogResult messageBoxResult = MessageBox.Show(
                            "¿Estás seguro de eliminar las imágenes de los productos sin generar redirecciones?",
                            "Advertencia",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Warning
                        );
                        if (messageBoxResult == DialogResult.No) return;
                    }
                    if(products is null) {
                        products = await getProducts(productService, ids, progressBarDeleteImages, 0, 50);
                        await ProcessImageDeletion(productService, products, 50, 50);
                    } else {
                        await ProcessImageDeletion(productService, products);
                    }

                }
                if (checkBoxDeleteProducts.Checked)
                {
                    if (!checkBoxRedirections.Checked || !checkBoxDeleteImages.Checked)
                    {
                        DialogResult messageBoxResult = MessageBox.Show(
                            $"¿Estás seguro de eliminar los productos sin realizar estas acciones?" +
                            $"{(!checkBoxRedirections.Checked ? "\n- Generar las redirecciones" : string.Empty)}" +
                            $"{(!checkBoxDeleteImages.Checked ? "\n- Eliminar las imágenes" : string.Empty)}",
                            "Advertencia",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Warning
                        );
                        if (messageBoxResult == DialogResult.No) return;
                        //TODO: Implementar eliminación de productos
                    }
                    await ProcessProductsDeletion(productService, ids);
                }
            }
            catch (OperationCanceledException)
            {
                handleCancellation();
            }
            catch (Exception ex)
            {
                richTextBoxReport.AppendText($"ERROR: {ex.Message}");
                buttonDownload.Enabled = false;
            }
            finally
            {
                _cts?.Dispose();
                _cts = null;
                buttonStartProgress.Enabled = true;
                buttonCancel.Enabled = false;
            }
        }

        private async Task redirection(List<Product> products)
        {
            _archivoGenerado = _redirectService.GenerateRedirectsFromJsonToCsvAsync(products);
        }

        private async Task<List<Product>> getProducts(IProductService productService, List<int> ids, ProgressBar prob, int offset = 0, int scale = 100)
        {
            if (_rutaArchivoEntrada is null)
                return new List<Product>();

            prob.Value = 0;
            prob.Visible = true;
            //TODO: Crear un factory de Progress para evitar repetir código
            var prgs = CreateProgressReporter(prob, "procesado", "importados", offset, scale);
            var products = await productService.GetProductsFromIdsAsync(ids, progress: prgs, _cts.Token);
            return products;
        }

        private void LogUpdate(string message)
        {
            richTextBoxReport.AppendText(message + Environment.NewLine);
            richTextBoxReport.ScrollToCaret();
        }
        private void handleCancellation()
        {
            LogUpdate(">>> PROCESO CANCELADO <<<");
            MessageBox.Show("Operación detenida.\nAlgunos elementos pueden haberse procesado ya.", "Cancelado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        [GeneratedRegex(@"wp-image-(\d+)", RegexOptions.Compiled)]
        private static partial Regex WpImageRegex();

        private async Task ProcessImageDeletion(IProductService productService, List<Product> products, int offset = 0, int scale = 100)
        {
            var idsInDescriptions = products
                .SelectMany(p => WpImageRegex().Matches(p.Description ?? string.Empty)
                    .Select(m => int.Parse(m.Groups[1].Value)))
                .Distinct()
                .ToList();

            var imagesDescription = idsInDescriptions.Any()
                ? await productService.GetImagesFromIdsAsync(idsInDescriptions, null, _cts.Token)
                : [];

            var allImagesFiltered = products.SelectMany(p => p.Images)
                .Concat(imagesDescription)
                .DistinctBy(img => img.Id)
                .ToList();

            if (!allImagesFiltered.Any()) return;

            // Mostrar diálogo de selección
            using var selector = new FormImageSelector(allImagesFiltered);
            if (selector.ShowDialog() != DialogResult.OK) return;

            var imagesToDelete = selector.SelectedImages;

            //Comienza la eliminación
            progressBarDeleteImages.Value = 0;
            progressBarDeleteImages.Visible = true;

            var idsImage = imagesToDelete.Select(c => int.Parse(c.Id)).ToList();
            var prgs = CreateProgressReporter(progressBarDeleteImages, "eliminado", "imágenes", offset, scale);
            await productService.DeleteImagesFromIdsAsync(idsImage, progress: prgs, _cts.Token);
            progressBarDeleteImages.Visible = false;
            pictureBoxCheckDeleteImages.Visible = true;
            LogUpdate("--- Proceso de eliminación de imágenes finalizado ---");
        }

        private async Task ProcessProductsDeletion(IProductService productService, List<int> ids, int offset = 0, int scale = 100)
        {
            progressBarDeleteProducts.Value = 0;
            progressBarDeleteProducts.Visible = true;

            var prgs = CreateProgressReporter(progressBarDeleteProducts, "eliminado", "productos", offset, scale);
            await productService.DeleteProductsFromIdsAsync(ids, progress: prgs, _cts.Token);
            progressBarDeleteProducts.Visible = false;
            pictureBoxCheckDeleteProducts.Visible = true;
            LogUpdate("--- Proceso de eliminación de productos finalizado ---");
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void tableLayoutPanelUpload_Paint(object sender, PaintEventArgs e)
        {

        }

        private void buttonUpload_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Seleccionar archivo CSV";
                openFileDialog.Filter = "Archivos CSV (*.csv)|*.csv|Todos los archivos (*.*)|*.*";
                openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    _rutaArchivoEntrada = openFileDialog.FileName;
                    labelUploadFileName.Text = _rutaArchivoEntrada;
                    buttonStartProgress.Enabled = true;
                }
            }
        }

        private void richTextBoxReport_TextChanged(object sender, EventArgs e)
        {

        }

        private async void buttonDownload_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                string extension = _redirectService.GetExtension();
                saveFileDialog.Title = "Guardar archivo de redirecciones";
                saveFileDialog.Filter = $"Archivos {extension.ToUpper()} (*.{extension})|*.{extension}|Todos los archivos (*.*)|*.*";

                var timestamp = DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss");
                var fileName = $"redirections_{timestamp}.{extension}";
                saveFileDialog.FileName = fileName;

                //Ruta por defecto, download si no ha establecido una anterior
                string directoryDefault = Settings.Default.directoryDownloadRedirections;
                if (String.IsNullOrEmpty(directoryDefault))
                    directoryDefault = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
                if (!Directory.Exists(directoryDefault))
                    saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    _rutaArchivoSalida = saveFileDialog.FileName;
                    try
                    {
                        await File.WriteAllTextAsync(_rutaArchivoSalida, _archivoGenerado);
                        string? pathSave = Path.GetDirectoryName(_rutaArchivoSalida);
                        if (pathSave is not null)
                        {
                            Settings.Default.directoryDownloadRedirections = pathSave;
                            Settings.Default.Save();
                        }
                        string argumento = $"/select, \"{_rutaArchivoSalida}\"";
                        System.Diagnostics.Process.Start("explorer.exe", argumento);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("No se ha podido guardar el archivo en la ubicación seleccionada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private IProgress<ProgressInfo> CreateProgressReporter(ProgressBar prob, string actionLabel, string unitLabel = "", int offset = 0, int scale = 100)
        {
            // Reset inicial solo si empezamos desde 0
            if (offset == 0)
            {
                prob.Value = 0;
                prob.Visible = true;
            }

            return new SynchronousProgress<ProgressInfo>(info =>
            {
                string progressMessage = $"{info.id} {actionLabel}: {info.current}";

                if (info.max is null)
                {
                    prob.Style = ProgressBarStyle.Marquee;
                }
                else
                {
                    prob.Style = ProgressBarStyle.Blocks;
                    progressMessage += $" de {info.max} {unitLabel}".TrimEnd();

                    // Cálculo: Desplazamiento + (PorcentajeActual * Escala)
                    double percentage = (double)info.current / info.max.Value;
                    int calculatedValue = offset + (int)Math.Round(percentage * scale);

                    prob.Value = Math.Clamp(calculatedValue, prob.Minimum, prob.Maximum);
                }

                LogUpdate(progressMessage);
            });
        }
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            if (_cts != null && !_cts.IsCancellationRequested)
            {
                buttonCancel.Enabled = false;
                _cts.Cancel();
            }
        }

        private void checkBoxDeleteImages_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void progressBarDeleteImages_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanelUpload_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void checkBoxDeleteProducts_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxRedirections_CheckedChanged_1(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanelImagesDelete_Paint(object sender, PaintEventArgs e)
        {

        }

        private void progressBarDeleteProducts_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanelImagesDelete_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void progressBarRedirections_Click(object sender, EventArgs e)
        {

        }        

        private void tableLayoutPanel1_Paint_1(object sender, PaintEventArgs e)
        {

        }
    }
}
