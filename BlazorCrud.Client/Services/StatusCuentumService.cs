using BlazorCrud.Shared;
using System.Net.Http.Json;

namespace BlazorCrud.Client.Services
{
    public class StatusCuentumService : IStatusCuentum
    {
        private readonly HttpClient _http;

        public StatusCuentumService(HttpClient http)
        {
            _http = http;
        }

        public async Task<StatusCuentumST> Buscar(int id)
        {
            var result = await _http.GetFromJsonAsync<ResponseAPI<StatusCuentumST>>($"/api/StatusCuentum/Buscar/{id}");

            if (result!.EsCorrecto)
                return result.Valor!;
            else
            {
                throw new Exception(result.Mensajes);
            }
        }

        public async Task<int> Editar(StatusCuentumST statuscuentum)
        {
            var result = await _http.PutAsJsonAsync($"/api/StatusCuentum/Editar/{statuscuentum.IdStatusCuentum}", statuscuentum);
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
            var result = await _http.DeleteAsync($"/api/StatusCuentum/Eliminar/{id}");
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.EsCorrecto)
                return response.EsCorrecto!;
            else
            {
                throw new Exception(response.Mensajes);
            }
        }

        public async Task<int> Guardar(StatusCuentumST statuscuentum)
        {
            var result = await _http.PostAsJsonAsync("/api/StatusCuentum/Guardar", statuscuentum);
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.EsCorrecto)
                return response.Valor!;
            else
            {
                throw new Exception(response.Mensajes);
            }
        }

        public async Task<List<StatusCuentumST>> Lista()
        {
            var result = await _http.GetFromJsonAsync<ResponseAPI<List<StatusCuentumST>>>("api/StatusCuentum/Lista");

            if (result!.EsCorrecto)
                return result.Valor!;
            else
            {
                throw new Exception(result.Mensajes);
            }
        }
    }
}
