using BlazorCrud.Shared;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace BlazorCrud.Client.Services
{
    public class OpcionService : IOpcion
    {
        private readonly HttpClient _http;

        public OpcionService(HttpClient http)
        {
            _http = http;
        }

        public async Task<OpcionOP> Buscar(int id)
        {
            var result = await _http.GetFromJsonAsync<ResponseAPI<OpcionOP>>($"/api/Opcion/Buscar/{id}");

            if (result!.EsCorrecto)
                return result.Valor!;
            else
            {
                throw new Exception(result.Mensajes);
            }
        }

        public async Task<int> Editar(OpcionOP opcion)
        {
            var result = await _http.PutAsJsonAsync($"/api/Opcion/Editar/{opcion.IdOpcion}", opcion);
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
            var result = await _http.DeleteAsync($"/api/Opcion/Eliminar/{id}");
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.EsCorrecto)
                return response.EsCorrecto!;
            else
            {
                throw new Exception(response.Mensajes);
            }
        }

        public async Task<int> Guardar(OpcionOP opcion)
        {
            var result = await _http.PostAsJsonAsync("/api/Opcion/Guardar", opcion);
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.EsCorrecto)
                return response.Valor!;
            else
            {
                throw new Exception(response.Mensajes);
            }
        }

        public async Task<List<OpcionOP>> Lista()
        {
            var result = await _http.GetFromJsonAsync<ResponseAPI<List<OpcionOP>>>("api/Opcion/Lista");

            if (result!.EsCorrecto)
                return result.Valor!;
            else
            {
                throw new Exception(result.Mensajes);
            }
        }
    }
}
