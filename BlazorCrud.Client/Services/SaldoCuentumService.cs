using BlazorCrud.Shared;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace BlazorCrud.Client.Services
{
    public class SaldoCuentumService : ISaldoCuenta
    {
        private readonly HttpClient _http;

        public SaldoCuentumService(HttpClient http)
        {
            _http = http;
        }

        public async Task<SaldoCuentumSC> Buscar(int id)
        {
            var result = await _http.GetFromJsonAsync<ResponseAPI<SaldoCuentumSC>>($"/api/SaldoCuentum/Buscar/{id}");
            return result?.Valor ?? new SaldoCuentumSC();
        }

        public async Task<int> Guardar(SaldoCuentumSC saldo)
        {
            var result = await _http.PostAsJsonAsync("/api/SaldoCuentum/Guardar", saldo);
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();
            return response?.Valor ?? 0;
        }

        public async Task<List<SaldoCuentumSC>> Lista()
        {
            var result = await _http.GetFromJsonAsync<ResponseAPI<List<SaldoCuentumSC>>>("api/SaldoCuentum/Lista");
            return result?.Valor ?? new List<SaldoCuentumSC>();
        }

        // Nuevo método para agregar un saldo
        public async Task<int> AgregarSaldo(SaldoCuentumSC saldoCuenta)
        {
            var result = await _http.PostAsJsonAsync("api/SaldoCuenta/Agregar", saldoCuenta);
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();
            return response?.Valor ?? 0;
        }

        public async Task<SaldoCuentumSC?> ObtenerPorPersona(int idPersona)
        {
            try
            {
                return await _http.GetFromJsonAsync<SaldoCuentumSC>($"api/SaldoCuentum/Persona/{idPersona}");
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Request error: {e.Message}");
                return null;
            }
        }

    }
}
