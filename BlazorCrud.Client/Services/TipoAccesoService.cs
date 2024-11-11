using BlazorCrud.Shared;
using System.Net.Http.Json;

namespace BlazorCrud.Client.Services
{
    public class TipoAccesoService : ITipoAcceso
    {
        private readonly HttpClient _http;

        public TipoAccesoService(HttpClient http)
        {
            _http = http;
        }

        public async Task<TipoAccesoTA> Buscar(int id)
        {
            var result = await _http.GetFromJsonAsync<ResponseAPI<TipoAccesoTA>>($"/api/TipoAcceso/Buscar/{id}");

            if (result!.EsCorrecto)
                return result.Valor!;
            else
            {
                throw new Exception(result.Mensajes);
            }
        }

        public async Task<int> Editar(TipoAccesoTA tipoacceso)
        {
            var result = await _http.PutAsJsonAsync($"/api/TipoAcceso/Editar/{tipoacceso.IdTipoAcceso}", tipoacceso);
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
            var result = await _http.DeleteAsync($"/api/TipoAcceso/Eliminar/{id}");
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.EsCorrecto)
                return response.EsCorrecto!;
            else
            {
                throw new Exception(response.Mensajes);
            };
        }

        public async Task<int> Guardar(TipoAccesoTA tipoacceso)
        {
            var result = await _http.PostAsJsonAsync("/api/TipoAcceso/Guardar", tipoacceso);
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.EsCorrecto)
                return response.Valor!;
            else
            {
                throw new Exception(response.Mensajes);
            }
        }

        public async Task<List<TipoAccesoTA>> Lista()
        {
            var result = await _http.GetFromJsonAsync<ResponseAPI<List<TipoAccesoTA>>>("api/TipoAcceso/Lista");

            if (result!.EsCorrecto)
                return result.Valor!;
            else
            {
                throw new Exception(result.Mensajes);
            }
        }
    }
}

