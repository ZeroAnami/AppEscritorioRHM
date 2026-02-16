using AppEscritorioRHM.Core.Entities;
using AppEscritorioRHM.Core.Interfaces.Infrastructure.Woo;
using AppEscritorioRHM.Infrastructure.ExternalServices.Woo.DTOs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Security.Policy;
using System.Text;
using System.Windows.Forms;

namespace AppEscritorioRHM.Infrastructure.ExternalServices.Woo
{
    //TODO: Completar el servicio de imágenes
    public class WPService : IWPService
    {
        private readonly HttpClient _client;
        private const int WaitingTimeTest = 5000;
        private const int Mcr = 2; //MaxConcurrentRequests
        public static readonly SemaphoreSlim _semaphore = new SemaphoreSlim(Mcr, Mcr);
        private static readonly Endpoints Endpoints = WooCommerce.GetEndpoint(WooCommerce.EndpointOptions.Wordpress);

        public WPService(HttpClient client)
        {
            _client = client;
        }  

        public async Task<ImageProduct> GetImageByIdAsync(int id, CancellationToken ct = default)
        {
            var imageWp = await GetResourceAsync<AttachmentWpDTO>($"{Endpoints.ImagesMedia}/{id}", null, ct);
            return MapToDomain(imageWp);
        }

        public async Task<ImageProduct> DeleteImageByIdAsync(int id, bool force = true, CancellationToken ct = default)
        {
            var imageWp = await DeleteResourceAsync<AttachmentWpDTO>($"{Endpoints.ImagesMedia}/{id}?force={force}", ct);
            return MapToDomain(imageWp);
        }
        [Obsolete("Puede fallar porque Wordpress acepta autentificaciones anónimas. Utilizar CheckConnectionAsync en su lugar.")]
        public async Task<bool> OldCheckConnectionAsync()
        {
            try
            {
                var testConnectionTask = Task.Run(async () =>
                {
                    try
                    {
                        var parametros = new Dictionary<string, string> { { "per_page", "1" } };
                        var response = await GetResourceAsync<List<AttachmentWpDTO>>(Endpoints.ImagesMedia, parametros);
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                });

                var clockTask = Task.Delay(WaitingTimeTest);
                var taskCompleted = await Task.WhenAny(testConnectionTask, clockTask);

                if (taskCompleted == clockTask)
                {
                    return false;
                }

                return await testConnectionTask;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> CheckConnectionAsync()
        {
            try
            {
                var testConnectionTask = Task.Run(async () =>
                {
                    try
                    {
                        // Si la autenticación falla, lanzará excepción (401/403).
                        var response = await GetResourceAsync<dynamic>("users/me", null);
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                });

                var clockTask = Task.Delay(WaitingTimeTest);
                var taskCompleted = await Task.WhenAny(testConnectionTask, clockTask);

                if (taskCompleted == clockTask)
                {
                    return false;
                }

                return await testConnectionTask;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private async Task<T> GetResourceAsync<T>(
            string endpoint,
            Dictionary<string, string> parameters = null,
            CancellationToken ct = default)
        {
            try
            {
                string url = BuildUrlWithQueryString(endpoint, parameters);
                var response = await _client.GetAsync(url, ct);
                return await DeserializeResponseAsync<T>(response);
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error al conectar con WooCommerce ({endpoint}): {ex.Message}");
            }
            catch (JsonException ex)
            {
                throw new Exception($"Error al leer la respuesta de WooCommerce: {ex.Message}");
            }
            catch (OperationCanceledException ex)
            {
                throw;
            }
        }

        private async Task<T> DeleteResourceAsync<T>(
            string endpoint,
            CancellationToken ct = default)
        {
            try
            {
                var response = await _client.DeleteAsync(endpoint, ct);
                return await DeserializeResponseAsync<T>(response);
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error al eliminar con WooCommerce ({endpoint}): {ex.Message}");
            }
            catch (JsonException ex)
            {
                throw new Exception($"Error al leer la respuesta de eliminación de WooCommerce: {ex.Message}");
            }
            catch (OperationCanceledException)
            {
                throw new Exception("La operación de eliminación fue cancelada.");
            }
        }

        private static string BuildUrlWithQueryString(string endpoint, Dictionary<string, string> parameters)
        {
            string url = endpoint;
            if (parameters != null && parameters.Count > 0)
            {
                var queryString = string.Join("&", parameters.Select(p =>
                    $"{p.Key}={Uri.EscapeDataString(p.Value)}"));

                url += $"?{queryString}";
            }

            return url;
        }
        private static async Task<T> DeserializeResponseAsync<T>(HttpResponseMessage response)
        {
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<T>(responseString);

            return result;
        }
        private ImageProduct MapToDomain(AttachmentWpDTO dto)
        {
            // Intentamos obtener la miniatura de WooCommerce
            string urlImagen = dto.SourceUrl; // Por defecto, la original (full)

            if (dto.MediaDetails?.Sizes != null)
            {
                // Prioridad: WooCommerce Thumbnail -> Medium -> Original
                if (dto.MediaDetails.Sizes.ContainsKey("woocommerce_thumbnail"))
                {
                    urlImagen = dto.MediaDetails.Sizes["woocommerce_thumbnail"].SourceUrl;
                }
                else if (dto.MediaDetails.Sizes.ContainsKey("medium"))
                {
                    urlImagen = dto.MediaDetails.Sizes["medium"].SourceUrl;
                }
            }

            return new ImageProduct
            {
                Id = dto.Id.ToString(),
                Src = urlImagen,
                Alt = dto.AltText,
                Name = dto.Title?.Rendered,
                Date_created_gmt = dto.DateGmt,
                Date_modified_gmt = dto.ModifiedGmt
            };
        }
        public SemaphoreSlim getSemaphore()
        {
            return _semaphore;
        }
    }
}