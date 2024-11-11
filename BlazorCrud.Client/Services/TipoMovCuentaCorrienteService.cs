using BlazorCrud.Shared;
using System.Net.Http.Json;

namespace BlazorCrud.Client.Services
{
    public class TipoMovCuentaCorrienteService : ITipoMovCuentaCorriente
    {
        private readonly HttpClient _http;

        public TipoMovCuentaCorrienteService(HttpClient http)
        {
            _http = http;
        }

        public async Task<TipoMovCuentaCorrienteCC> Buscar(int id)
        {
            var result = await _http.GetFromJsonAsync<ResponseAPI<TipoMovCuentaCorrienteCC>>($"/api/TipoMovCuentaCorriente/Buscar/{id}");

            if (result!.EsCorrecto)
                return result.Valor!;
            else
            {
                throw new Exception(result.Mensajes);
            }
        }

        public async Task<int> Editar(TipoMovCuentaCorrienteCC tipomovcuentacorriente)
        {
            var result = await _http.PutAsJsonAsync($"/api/TipoMovCuentaCorriente/Editar/{tipomovcuentacorriente.IdTipoMovimientoCXC}", tipomovcuentacorriente);
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
            var result = await _http.DeleteAsync($"/api/TipoMovCuentaCorriente/Eliminar/{id}");
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.EsCorrecto)
                return response.EsCorrecto!;
            else
            {
                throw new Exception(response.Mensajes);
            }
        }

        public async Task<int> Guardar(TipoMovCuentaCorrienteCC tipomovcuentacorriente)
        {
            var result = await _http.PostAsJsonAsync("/api/TipoMovCuentaCorriente/Guardar", tipomovcuentacorriente);
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.EsCorrecto)
                return response.Valor!;
            else
            {
                throw new Exception(response.Mensajes);
            }
        }

        public async Task<List<TipoMovCuentaCorrienteCC>> Lista()
        {
            var result = await _http.GetFromJsonAsync<ResponseAPI<List<TipoMovCuentaCorrienteCC>>>("api/TipoMovCuentaCorriente/Lista");

            if (result!.EsCorrecto)
                return result.Valor!;
            else
            {
                throw new Exception(result.Mensajes);
            }
        }
    }
}

