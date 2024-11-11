using BlazorCrud.Shared;
using System.Net.Http.Json;

namespace BlazorCrud.Client.Services
{
    public class GeneroService : IGenero
    {
        private readonly HttpClient _http;

        public GeneroService(HttpClient http)
        {
            _http = http;
        }

        public async Task<GeneroGR> Buscar(int id)
        {
            var result = await _http.GetFromJsonAsync<ResponseAPI<GeneroGR>>($"/api/Genero/Buscar/{id}");

            if (result!.EsCorrecto)
                return result.Valor!;
            else
            {
                throw new Exception(result.Mensajes);
            }
        }

        public async Task<int> Editar(GeneroGR genero)
        {
            var result = await _http.PutAsJsonAsync($"/api/Genero/Editar/{genero.IdGenero}", genero);
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
            var result = await _http.DeleteAsync($"/api/Genero/Eliminar/{id}");
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.EsCorrecto)
                return response.EsCorrecto!;
            else
            {
                throw new Exception(response.Mensajes);
            }
        }

        public async Task<int> Guardar(GeneroGR genero)
        {
            var result = await _http.PostAsJsonAsync("/api/Genero/Guardar", genero);
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.EsCorrecto)
                return response.Valor!;
            else
            {
                throw new Exception(response.Mensajes);
            }
        }

        public async Task<List<GeneroGR>> Lista()
        {
            var result = await _http.GetFromJsonAsync<ResponseAPI<List<GeneroGR>>>("api/Genero/Lista");

            if (result!.EsCorrecto)
                return result.Valor!;
            else
            {
                throw new Exception(result.Mensajes);
            }
        }
    }
}
