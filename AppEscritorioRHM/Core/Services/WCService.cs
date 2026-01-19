using AppEscritorioRHM.Core.Interfaces;
using AppEscritorioRHM.Core.Models.Domain;
using AppEscritorioRHM.Core.Models.DTOs.WooCommerce;
using AppEscritorioRHM.Core.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Security.Policy;
using System.Text;

namespace AppEscritorioRHM.Core.Services
{
    public class WCService : IWCService
    {
        private readonly HttpClient _client;
        private const int WaitingTimeTest = 5000;
        private const int Mcr = 2; //MaxConcurrentRequests
        public static readonly SemaphoreSlim _semaphore = new SemaphoreSlim(Mcr, Mcr);
        public WCService(HttpClient client)
        {
            _client = client;
        }
        public async Task<Product> GetProductByIdAsync(int id, CancellationToken ct = default)
        {
            var productWoo = await GetResourceAsync<ProductWooDTO>($"{WCEndpoints.Products}/{id}", null, ct);
            return MapProductWooToDomain(productWoo);
        }
        public async Task<Product> DeleteProductByIdAsync(int id, bool force, CancellationToken ct = default)
        {
            var productWoo = await DeleteResourceAsync<ProductWooDTO>($"{WCEndpoints.Products}/{id}?force={force}", ct);
            return MapProductWooToDomain(productWoo);
        }

        public async Task<List<Product>> GetProductsByPageAsync(int page, int perPage = 100, CancellationToken ct = default)
        {
            var parameters = new Dictionary<string, string>
            {
                { "page", page.ToString() },
                { "per_page", perPage.ToString() }
            };
            var productsWoo = await GetResourceAsync<List<ProductWooDTO>>(WCEndpoints.Products, parameters, ct);
            return productsWoo.Select(c => MapProductWooToDomain(c)).ToList();
        }

        public async Task<List<Product>> GetAllProductsAsync(IProgress<ProgressInfo> progress = null, CancellationToken ct = default)
        {
            var masterList = new List<Product>();
            int page = 1;
            int perPage = 100;
            bool isMoreData = true;

            while (isMoreData)
            {
                var productList = await GetProductsByPageAsync(page, perPage, ct);

                if (productList != null && productList.Count > 0)
                {
                    masterList.AddRange(productList);

                    if (productList.Count < perPage)
                    {
                        isMoreData = false;
                    }
                    else
                    {
                        progress?.Report(new ProgressInfo(masterList.Count));
                        page++;
                    }
                }
                else
                {
                    isMoreData = false;
                }
            }
            progress?.Report(new ProgressInfo(masterList.Count));
            return masterList;
        }

        public async Task<Category> GetCategoryByIdAsync(int id, CancellationToken ct = default)
        {
            var categoryWoo = await GetResourceAsync<CategoryWooDTO>($"{WCEndpoints.Categories}/{id}", null, ct);
            return MapCategoryWooToDTO(categoryWoo);
        }

        public async Task<List<Category>> GetCategoriesAsync(CancellationToken ct = default)
        {
            var categories = await GetResourceAsync<List<CategoryWooDTO>>(WCEndpoints.Categories, null, ct);
            return categories.Select(c => MapCategoryWooToDTO(c)).ToList();
        }

        public async Task<List<ProductVariationsWooDTO>> GetProductVariationsAsync(int parentId, CancellationToken ct = default)
        {
            string endpoint = $"{WCEndpoints.Products}/{parentId}/{WCEndpoints.Variations}";
            return await GetResourceAsync<List<ProductVariationsWooDTO>>(endpoint, null, ct);
        }

        public async Task<ProductVariationsWooDTO> GetProductVariationAsync(int parentId, int id, CancellationToken ct = default)
        {
            string endpoint = $"{WCEndpoints.Products}/{parentId}/{WCEndpoints.Variations}/{id}";
            return await GetResourceAsync<ProductVariationsWooDTO>(endpoint, null, ct);
        }
        
        public async Task<bool> CheckConnectionAsync()
        {
            try
            {
                var testConnectionTask = Task.Run(async () =>
                {
                    try
                    {
                        var parametros = new Dictionary<string, string> { { "per_page", "1" } };
                        var result = await GetResourceAsync<List<ProductWooDTO>>(WCEndpoints.Products, parametros);
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

        private Product MapProductWooToDomain(ProductWooDTO dto)
        {
            if (dto == null) return null;

            return new Product
            {
                Id = dto.id?.ToString(),
                Sku = dto.sku,
                Name = dto.name,
                RegularPrice = decimal.TryParse(dto.regular_price, out var regularPrice) ? regularPrice : (decimal?)null,
                Price = decimal.TryParse(dto.price, out var price) ? price : (decimal?)null,
                SalePrice = decimal.TryParse(dto.sale_price, out var salePrice) ? salePrice : (decimal?)null,
                Date_created_gmt = dto.date_created_gmt,
                Date_modified_gmt = dto.date_modified_gmt,
                Stock = dto.stock_quantity,
                Url = dto.permalink,
                ShortDescription = dto.short_description,
                Description = dto.description,
                IsVisible = dto.catalog_visibility == "visible",
                Categories = dto.categories?.Select(c => new Category
                {
                    Id = c.id.ToString(),
                    Name = c.name,
                    Slug = c.slug
                }).ToList(),
                Images = dto.images?.Select(c => new ImageProduct
                {
                    Id = c.id.ToString(),
                    Date_created_gmt = c.date_created_gmt,
                    Date_modified_gmt = c.date_modified_gmt,
                    Src = c.src,
                    Name = c.name,
                    Alt = c.alt
                }).ToList()
            }; 
        }

        private static ProductWooDTO MapToDTO(Product p)
        {
            if (p == null) return null;
            return new ProductWooDTO
            {
                id = int.TryParse(p.Id, out var idValue) ? idValue : (int?)null,
                sku = p.Sku,
                name = p.Name,
                regular_price = p.RegularPrice?.ToString(),
                price = p.Price?.ToString(),
                sale_price = p.SalePrice?.ToString(),
                stock_quantity = p.Stock,
                permalink = p.Url,
                short_description = p.ShortDescription,
                description = p.Description,
                catalog_visibility = p.IsVisible == true ? "visible" : "hidden",
                categories = p.Categories?.Select(c => new CategoryWooDTO
                {
                    id = int.TryParse(c.Id, out var catId) ? catId : 0,
                    name = c.Name,
                    slug = c.Slug
                }).ToList()
            };
        }

        private static Category MapCategoryWooToDTO(CategoryWooDTO c)
        {
            if (c == null) return null;
            return new Category
            {
                Id = c.id.ToString(),
                Name = c.name,
                Slug = c.slug
            };
        }

        public SemaphoreSlim getSemaphore()
        {
            return _semaphore;
        }
    }
}