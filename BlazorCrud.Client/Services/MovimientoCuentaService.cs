using BlazorCrud.Shared;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace BlazorCrud.Client.Services
{
    public class MovimientoCuentaService : IMovCuenta
    {
        private readonly HttpClient _http;

        public MovimientoCuentaService(HttpClient http)
        {
            _http = http;
        }

        public async  Task<MovimientoCuentaMC> Buscar(int id)
        {
            var result = await _http.GetFromJsonAsync<ResponseAPI<MovimientoCuentaMC>>($"/api/MovimientoCuenta/Buscar/{id}");

            if (result!.EsCorrecto)
                return result.Valor!;
            else
            {
                throw new Exception(result.Mensajes);
            }
        }

        public  async Task<int> Guardar(MovimientoCuentaMC MovCuenta)
        {
            var result = await _http.PostAsJsonAsync("/api/MovimientoCuenta/Guardar", MovCuenta);
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.EsCorrecto)
                return response.Valor!;
            else
            {
                throw new Exception(response.Mensajes);
            }
        }

        public async Task<List<MovimientoCuentaMC>> Lista()
        {
            var result = await _http.GetFromJsonAsync<ResponseAPI<List<MovimientoCuentaMC>>>("api/MovimientoCuenta/Lista");

            if (result!.EsCorrecto)
                return result.Valor!;
            else
            {
                throw new Exception(result.Mensajes);
            }
        }

        public async Task<List<MovimientoCuentaMC>> ObtenerMovimientosPorPersonaMes(int idPersona, int mes)
        {
            return await _http.GetFromJsonAsync<List<MovimientoCuentaMC>>($"api/MovimientoCuenta/Persona/{idPersona}/mes/{mes}")
                   ?? new List<MovimientoCuentaMC>();
        }
    }
}
