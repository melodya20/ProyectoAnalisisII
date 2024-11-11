using BlazorCrud.Shared;
using System.Net.Http.Json;

namespace BlazorCrud.Client.Services
{
    public class TipoDocumentoService : ITipoDocumento
    {
        private readonly HttpClient _http;

        public TipoDocumentoService(HttpClient http)
        {
            _http = http;
        }

        public async Task<TipoDocumentoTD> Buscar(int id)
        {
            var result = await _http.GetFromJsonAsync<ResponseAPI<TipoDocumentoTD>>($"/api/TipoDocumento/Buscar/{id}");

            if (result!.EsCorrecto)
                return result.Valor!;
            else
            {
                throw new Exception(result.Mensajes);
            }
        }

        public async Task<int> Editar(TipoDocumentoTD tipodocumento)
        {
            var result = await _http.PutAsJsonAsync($"/api/TipoDocumento/Editar/{tipodocumento.IdTipoDocumento}", tipodocumento);
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
            var result = await _http.DeleteAsync($"/api/TipoDocumento/Eliminar/{id}");
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.EsCorrecto)
                return response.EsCorrecto!;
            else
            {
                throw new Exception(response.Mensajes);
            }
        }

        public async Task<int> Guardar(TipoDocumentoTD tipodocumento)
        {
            var result = await _http.PostAsJsonAsync("/api/TipoDocumento/Guardar", tipodocumento);
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.EsCorrecto)
                return response.Valor!;
            else
            {
                throw new Exception(response.Mensajes);
            }
        }

        public async Task<List<TipoDocumentoTD>> Lista()
        {
            var result = await _http.GetFromJsonAsync<ResponseAPI<List<TipoDocumentoTD>>>("api/TipoDocumento/Lista");

            if (result!.EsCorrecto)
                return result.Valor!;
            else
            {
                throw new Exception(result.Mensajes);
            }
        }
    }
}

