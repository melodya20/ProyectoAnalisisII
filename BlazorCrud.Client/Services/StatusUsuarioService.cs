using BlazorCrud.Shared;
using System.Net.Http.Json;

namespace BlazorCrud.Client.Services
{
    public class StatusUsuarioService : IStatusUsuario
    {
        private readonly HttpClient _http;

        public StatusUsuarioService(HttpClient http)
        {
            _http = http;
        }

        public async Task<StatusUsuarioST> Buscar(int id)
        {
            var result = await _http.GetFromJsonAsync<ResponseAPI<StatusUsuarioST>>($"/api/StatusUsuario/Buscar/{id}");

            if (result!.EsCorrecto)
                return result.Valor!;
            else
            {
                throw new Exception(result.Mensajes);
            }
        }

        public async Task<int> Editar(StatusUsuarioST statususuario)
        {
            var result = await _http.PutAsJsonAsync($"/api/StatusUsuario/Editar/{statususuario.IdStatusUsuario}", statususuario);
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
            var result = await _http.DeleteAsync($"/api/StatusUsuario/Eliminar/{id}");
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.EsCorrecto)
                return response.EsCorrecto!;
            else
            {
                throw new Exception(response.Mensajes);
            }
        }

        public async Task<int> Guardar(StatusUsuarioST statususuario)
        {
            var result = await _http.PostAsJsonAsync("/api/StatusUsuario/Guardar", statususuario);
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.EsCorrecto)
                return response.Valor!;
            else
            {
                throw new Exception(response.Mensajes);
            }
        }

        public async Task<List<StatusUsuarioST>> Lista()
        {
            var result = await _http.GetFromJsonAsync<ResponseAPI<List<StatusUsuarioST>>>("api/StatusUsuario/Lista");

            if (result!.EsCorrecto)
                return result.Valor!;
            else
            {
                throw new Exception(result.Mensajes);
            }
        }
    }
}

/* StatusUsuario */
