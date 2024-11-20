using System.Text.Json;
using System.Text;
using MongoAPIWeb.Model;
using MongoDB.Bson;
using System.Net.Http;

namespace Proyectotiendita.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;
        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task DeleteProduct(string id)
        {
            await _httpClient.DeleteAsync($"api/product/{id}");
        }

        public async Task<IEnumerable<Products>> GetAllProducts()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<Products>>
                (await _httpClient.GetStreamAsync($"api/product"),
                new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<Products> GetProductDetails(string id)
        {
            return await JsonSerializer.DeserializeAsync<Products>
                (await _httpClient.GetStreamAsync($"api/product/{id}"),
                new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task SaveProduct(Products product)
        {
            try
            {
                var productJson = new StringContent(JsonSerializer.Serialize(product),
                    Encoding.UTF8, "application/json");
                HttpResponseMessage response;
                if (string.IsNullOrEmpty(product.ID_Productos))
                {
                    product.ID_Productos = ObjectId.GenerateNewId().ToString();
                    productJson = new StringContent(JsonSerializer.Serialize(product),
                        Encoding.UTF8, "application/json");

                    response = await _httpClient.PostAsync("api/product", productJson);
                }
                else
                {
                    response = await _httpClient.PutAsync($"api/product/{product.ID_Productos}", productJson);
                }

                response.EnsureSuccessStatusCode();
                var responseData = await response.Content.ReadAsStringAsync();

            }
            catch (Exception ex)
            {
                // Manejar el error
                var errorMessage = ex.Message; // Aquí puedes loguear el error o mostrarlo
            }
        }

    }

}
