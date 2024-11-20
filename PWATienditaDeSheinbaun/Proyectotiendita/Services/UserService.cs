using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using TiendaSModels;

namespace TiendaS.Client.Services
{
    public class UserService : IUsersService
    {
        private readonly HttpClient _httpClient;
        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<Users> AuthenticateUser(UsersDTO userAuthDTO)
        {
            var response = await _httpClient.PostAsJsonAsync("api/users/Login", userAuthDTO);

            try
            {
                if (response.IsSuccessStatusCode)
                {

                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var user = JsonSerializer.Deserialize<Users>(jsonResponse, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    return user;
                }
                else if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new UnauthorizedAccessException("Usuario o contraseña incorrectos");
                }
                else
                {
                    var errorResponse = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error inesperado: {response.ReasonPhrase}. Detalles: {errorResponse}");
                }
            }
            catch (HttpRequestException ex)
            {
                // Manejo de errores de red
                throw new Exception("Error en la solicitud HTTP: " + ex.Message);
            }
            catch (JsonException ex)
            {
                // Manejo de errores de deserialización
                throw new Exception("Error al deserializar la respuesta: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Error en la solicitud HTTP: " + ex.Message);
            }
        }

        public async Task DeleteUser(string id)
        {
            await _httpClient.DeleteAsync($"api/users/{id}");
        }

        public async Task<IEnumerable<Users>> GetAllUsers()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<Users>>
                   (await _httpClient.GetStreamAsync($"api/users"),
                   new JsonSerializerOptions()
                   {
                       PropertyNameCaseInsensitive
         = true
                   });
        }

        public async Task<Users> GetUserDetail(string id)
        {
            return await JsonSerializer.DeserializeAsync<Users>
            (await _httpClient.GetStreamAsync($"api/users/{id}"),
             new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task SaveUser(Users user)
        {
            var userJson = new StringContent(JsonSerializer.Serialize(user),
                Encoding.UTF8, "application/json");

            HttpResponseMessage response;

            if (string.IsNullOrEmpty(user.Id))
            {
                response = await _httpClient.PostAsync($"api/users", userJson);
            }
            else
            {
                response = await _httpClient.PutAsync($"api/users/{user.Id}", userJson);
            }


            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Usuario guardado con éxito.");
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error al guardar el usuario: {response.StatusCode}, Detalles: {errorContent}");
            }
        }
    }
}
