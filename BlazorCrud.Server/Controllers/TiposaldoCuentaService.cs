using BlazorCrud.Shared;
using System.Net.Http.Json;

namespace BlazorCrud.Client.Services
{
    public class TipoSaldoCuentaService : ITipoSaldoCuenta
    {
        private readonly HttpClient _http;

        public TipoSaldoCuentaService(HttpClient http)
        {
            _http = http;
        }

        public async Task<TipoSaldoCuentaTSC> Buscar(int id)
        {
            var result = await _http.GetFromJsonAsync<ResponseAPI<TipoSaldoCuentaTSC>>($"/api/TipoSaldoCuenta/Buscar/{id}");

            if (result!.EsCorrecto)
                return result.Valor!;
            else
            {
                throw new Exception(result.Mensajes);
            }
        }

        public async Task<int> Editar(TipoSaldoCuentaTSC tscuenta)
        {
            var result = await _http.PutAsJsonAsync($"/api/TipoSaldoCuenta/Editar/{tscuenta.IdTipoSaldoCuenta}", tscuenta);
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.EsCorrecto)
                return response.Valor!;
            else
            {
                throw new Exception(response.Mensajes);
            }
        }

        public async Task<bool> Eliminar(int id)
        {
            var result = await _http.DeleteAsync($"/api/TipoSaldoCuenta/Eliminar/{id}");
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.EsCorrecto)
                return response.EsCorrecto!;
            else
            {
                throw new Exception(response.Mensajes);
            }
        }

        public async Task<int> Guardar(TipoSaldoCuentaTSC tscuenta)
        {
            var result = await _http.PostAsJsonAsync("/api/TipoSaldoCuenta/Guardar", tscuenta);
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.EsCorrecto)
                return response.Valor!;
            else
            {
                throw new Exception(response.Mensajes);
            }
        }

        public async Task<List<TipoSaldoCuentaTSC>> Lista()
        {
            var result = await _http.GetFromJsonAsync<ResponseAPI<List<TipoSaldoCuentaTSC>>>("api/TipoSaldoCuenta/Lista");

            if (result!.EsCorrecto)
                return result.Valor!;
            else
            {
                throw new Exception(result.Mensajes);
            }
        }
    }
}
