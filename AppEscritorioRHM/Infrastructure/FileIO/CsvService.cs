using AppEscritorioRHM.Core.Interfaces.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppEscritorioRHM.Infrastructure.FileIO
{
    [Obsolete]
    public class CsvService : ICsvService
    {
        public async Task<List<string>> ImportProductsAsync(string filePath)
        {
            var lines = new List<string>();

            if (string.IsNullOrWhiteSpace(filePath))
            {
                Console.WriteLine("importarProducts: la ruta está vacía.");
                return lines;
            }

            try
            {
                if (filePath.StartsWith("./") || filePath.StartsWith("/") ||
                    Path.IsPathRooted(filePath) || File.Exists(filePath))
                {
                    var path = filePath.StartsWith("./") ? Path.GetFullPath(filePath) : filePath;
                    if (!File.Exists(path))
                    {
                        Console.WriteLine($"importarProducts: el fichero no existe: {path}");
                        return lines;
                    }

                    lines.AddRange(File.ReadAllLines(path, Encoding.UTF8));
                    return lines;
                }

                Console.WriteLine($"importarProducts: formato de ruta no reconocido: '{filePath}'");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"importarProducts: error al importar CSV: {ex.Message}");
            }

            return lines;
        }
    }
}
