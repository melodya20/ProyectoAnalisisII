using BlazorCrud.Shared;
using System.Net.Http.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlazorCrud.Client.Services
{
    public class CierreMensualService
    {
        private readonly HttpClient _http;
        public CierreMensualService(HttpClient http)
        {
            _http = http;
        }

        public async Task<string> RealizarCierreMensual()
        {
            var response = await _http.PostAsync("api/CierreMensual/RealizarCierre", null);
            if (!response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync(); // Obtén el mensaje de error
            }
            return "Cierre mensual completado"; // Mensaje de éxito
        }

    }
}
