using BlazorCrud.Shared;
using System.Net.Http.Json;

namespace BlazorCrud.Client.Services
{
    public class UsuarioService : IUsuarioServices
    {
            
        private readonly HttpClient _http;

        public UsuarioService(HttpClient http)
        {
            _http = http;
        }

        public async Task<UsuarioUSO> Buscar(string id)
        {
            var result = await _http.GetFromJsonAsync<ResponseAPI<UsuarioUSO>>($"/api/Usuario/Buscar/{id}");

            if (result!.EsCorrecto)
                return result.Valor!;
            else
            {
                throw new Exception(result.Mensajes);
            }
        }

        public async Task<string> Guardar(UsuarioUSO usuario) // Cambiar el tipo de retorno de int a string
        {
            var result = await _http.PostAsJsonAsync("/api/Usuario/Guardar", usuario);
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<string>>(); // Cambiar int a string

            if (response!.EsCorrecto)
                return response.Valor!;
            else
            {
                throw new Exception(response.Mensajes);
            }
        }

        public async Task<List<UsuarioUSO>> Lista()
        {
            var result = await _http.GetFromJsonAsync<ResponseAPI<List<UsuarioUSO>>>("api/Usuario/Lista");
            if (result!.EsCorrecto)
                return result.Valor!;
            else
            {
                throw new Exception(result.Mensajes);
            }
        }

        public async Task<string> Editar(UsuarioUSO usuario) // Cambié el tipo de retorno de int a string
        {
            var result = await _http.PutAsJsonAsync($"/api/Usuario/Editar/{usuario.IdUsuario}", usuario);
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<string>>(); // Cambié int a string

            if (response!.EsCorrecto)
                return response.Valor!;
            else
            {
                throw new Exception(response.Mensajes);
            }
        }

        public async Task<bool> Eliminar(string id)
        {
            var result = await _http.DeleteAsync($"/api/Usuario/Eliminar/{id}");
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<string>>();

            if (response!.EsCorrecto)
                return response.EsCorrecto!;
            else
            {
                throw new Exception(response.Mensajes);
            }
        }

    }
}
