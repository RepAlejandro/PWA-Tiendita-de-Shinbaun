using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace Proyectotiendita.Services
{
    public class Geolocalizacion
    {
        private readonly IJSRuntime _jsRuntime;

        public Geolocalizacion(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        // Este task se enfoca en encontrar el usuario desde Geolocation.js
        public async Task<UserLocation> GetUserLocationAsync()
        {
            try
            {
                //Obtiene la ubicacion del usuario
                var position = await _jsRuntime.InvokeAsync<UserLocation>("getUserLocation");
                return position;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la ubicación: " + ex.Message);
            }
        }
    }

    // Clase que representa la ubicación del usuario
    public class UserLocation
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
