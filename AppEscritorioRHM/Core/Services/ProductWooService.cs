using AppEscritorioRHM.Core.Interfaces;
using AppEscritorioRHM.Core.Models.Domain;
using AppEscritorioRHM.Core.Utilities;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

namespace AppEscritorioRHM.Core.Services
{
    public class ProductWooService : IProductService
    {
        private readonly AppSettings _settings;
        private readonly IWCService _WCService;
        private readonly IWPService _WPService;

        public ProductWooService(IWCService wCService, IWPService wPService, AppSettings settings)
        {
            _settings = settings;
            _WCService = wCService;
            _WPService = wPService;
        }
        public async Task<List<Product>> GetProductsFromIdsAsync(
            List<int> ids,
            IProgress<ProgressInfo> progress = null,
            CancellationToken ct = default)
        {
            return await ids.ProcessWithSemaphoreAsync(
                semaphore: _WCService.getSemaphore(),
                body: (id, token) => _WCService.GetProductByIdAsync(id, token),
                progress: progress,
                ct: ct,
                itemIdSelector: id => id
            );
        }
        public async Task<List<ImageProduct>> GetImagesFromIdsAsync(
            List<int> ids,
            IProgress<ProgressInfo> progress = null,
            CancellationToken ct = default)
        { 
            return await ids.ProcessWithSemaphoreAsync(
                semaphore: _WPService.getSemaphore(),
                body: (id, token) => _WPService.GetImageByIdAsync(id, token),
                progress: progress,
                ct: ct,
                itemIdSelector: id => id
            );
        }
        public async Task<List<Product>> DeleteProductsFromIdsAsync(
            List<int> ids,
            IProgress<ProgressInfo> progress = null,
            CancellationToken ct = default)
        {
            return await ids.ProcessWithSemaphoreAsync(
                semaphore: _WCService.getSemaphore(),
                body: (id, token) => _WCService.DeleteProductByIdAsync(id, true, token),
                progress: progress,
                ct: ct,
                itemIdSelector: id => id
            );
        }

        public async Task<List<ImageProduct>> DeleteImagesFromIdsAsync(
            List<int> ids,
            IProgress<ProgressInfo> progress = null,
            CancellationToken ct = default)
        {
            return await ids.ProcessWithSemaphoreAsync(
                semaphore: _WPService.getSemaphore(),
                body: (id, token) => _WPService.DeleteImageByIdAsync(id, true, token),
                progress: progress,
                ct: ct,
                itemIdSelector: id => id
            );
        }

        public List<int> getIdsFromCsvAsync(string pathToFile)
        {
            // Detectar si el delimitador es coma o punto y coma leyendo la cabecera
            string delimiter = ",";
            using (var streamReader = new StreamReader(pathToFile))
            {
                string headerLine = streamReader.ReadLine();
                if (headerLine != null && headerLine.Contains(';'))
                {
                    delimiter = ";";
                }
            }

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = delimiter,
                HasHeaderRecord = true,
                PrepareHeaderForMatch = args => args.Header.ToLower(),
            };

            var ids = new List<int>();

            using (var reader = new StreamReader(pathToFile))
            using (var csv = new CsvReader(reader, config))
            {
                csv.Read();
                csv.ReadHeader();

                while (csv.Read())
                {
                    string tipo = csv.GetField("Tipo");
                    string idStr = csv.GetField("ID");

                    if (tipo == "variable" && int.TryParse(idStr, out int id))
                    {
                        ids.Add(id);
                    }
                }
            }
            return ids;
        }
    }
}
