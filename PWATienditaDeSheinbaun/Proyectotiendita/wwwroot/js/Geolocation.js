let map;
let polyline;
let marker1;
let marker2;
let marker3;
let currentLatLng;
let thirdPoint;

function initializeMap() {
    map = L.map('map').setView([25.6718, -100.3120], 13); // Macroplaza coordinates

    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
        attribution: '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
    }).addTo(map);

    // Agregar marcador en la Macroplaza
    marker1 = L.marker([25.6718, -100.3120]).addTo(map).bindPopup("Macroplaza");

    // Agregar marcador en el destino (cuando obtengas la ubicación)
    marker2 = L.marker([25.6718, -100.3120]).addTo(map).bindPopup("Destino");

    // Tercer marcador en movimiento
    marker3 = L.marker([25.6718, -100.3120]).addTo(map).bindPopup("Tercer punto");

    polyline = L.polyline([], { color: 'blue' }).addTo(map); // Inicializar la línea poligonal
}

async function getRouteFromORS(latA, lngA, latB, lngB) {
    const apiKey = '5b3ce3597851110001cf62484d2a089a15be40b3a39a07eea1a753a3'; // Reemplaza con tu API Key de OpenRouteService
    const url = `https://api.openrouteservice.org/v2/directions/driving-car?api_key=${apiKey}&start=${lngA},${latA}&end=${lngB},${latB}`;

    try {
        const response = await fetch(url);
        if (!response.ok) {
            throw new Error(`Error en la solicitud: ${response.status}`);
        }
        const data = await response.json();
        const coordinates = data.features[0].geometry.coordinates.map(coord => [coord[1], coord[0]]);

        // Actualizar la línea poligonal en el mapa con las coordenadas de la ruta
        polyline.setLatLngs(coordinates);

        // Mover el marcador a lo largo de la ruta
        animateMarker(coordinates);

    } catch (error) {
        console.error('Error al obtener la ruta:', error);
    }
}

function animateMarker(coordinates) {
    let i = 0;
    const interval = setInterval(() => {
        if (i < coordinates.length) {
            marker3.setLatLng(coordinates[i]);
            i++;
        } else {
            clearInterval(interval); // Detener la animación cuando llegue al destino

            // Mostrar el modal de llegada
            showArrivalModal();
        }
    }, 100); // Intervalo de 100ms para el movimiento del marcador
}

function showArrivalModal() {
    // Mostrar el modal de llegada
    const modal = new bootstrap.Modal(document.getElementById('arrivalModal'));
    modal.show();

    // Lógica para el botón "Ya recibí mi paquete"
    document.getElementById('confirmArrival').addEventListener('click', function () {
        modal.hide(); // Cerrar el modal
        // Aquí puedes agregar lógica adicional si es necesario, como actualizar el estado en la interfaz
    });
}

function updateDestination(lat, lng) {
    // Actualiza la ubicación del segundo marcador (Destino)
    marker2.setLatLng([lat, lng]);
}

function simulateRoute(latA, lngA, latB, lngB) {
    // Obtener ruta simulada entre dos puntos
    getRouteFromORS(latA, lngA, latB, lngB);
}

function getUserLocation() {
    return new Promise((resolve, reject) => {
        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition(
                (position) => {
                    const coords = {
                        Latitude: position.coords.latitude,
                        Longitude: position.coords.longitude
                    };
                    resolve(coords);
                },
                (error) => {
                    reject(`Error al obtener la ubicación: ${error.message}`);
                }
            );
        } else {
            reject("La geolocalización no es compatible con este navegador.");
        }
    });
}
