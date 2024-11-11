using BlazorCrud.Shared;
using System.Net.Http.Json;

namespace BlazorCrud.Client.Services
{
    public class UsuarioRoleService : IUsuarioRole
    {
        private readonly HttpClient _http;

        public UsuarioRoleService(HttpClient http)
        {
            _http = http;
        }

        public async Task<UsuarioRoleURL> Buscar(string idUsuario, int idRole)
        {
            var result = await _http.GetFromJsonAsync<ResponseAPI<UsuarioRoleURL>>($"/api/UsuarioRole/Buscar/{idUsuario}/{idRole}");

            if (result!.EsCorrecto)
                return result.Valor!;
            else
            {
                throw new Exception(result.Mensajes);
            }
        }

        public async Task<bool> Editar(UsuarioRoleURL usuarioR)
        {
            var result = await _http.PutAsJsonAsync($"/api/UsuarioRole/Editar/{usuarioR.IdUsuario}/{usuarioR.IdRole}", usuarioR);
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<bool>>();

            if (response!.EsCorrecto)
                return response.Valor!;
            else
            {
                throw new Exception(response.Mensajes);
            }
        }

        public async Task<bool> Eliminar(string idUsuario, int idRole)
        {
            var result = await _http.DeleteAsync($"/api/UsuarioRole/Eliminar/{idUsuario}/{idRole}");
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<bool>>();

            if (response!.EsCorrecto)
                return response.Valor!;
            else
            {
                throw new Exception(response.Mensajes);
            }
        }

        public async Task<bool> Guardar(UsuarioRoleURL usuarioR)
        {
            var result = await _http.PostAsJsonAsync("/api/UsuarioRole/Guardar", usuarioR);
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<bool>>();

            if (response!.EsCorrecto)
                return response.Valor!;
            else
            {
                throw new Exception(response.Mensajes);
            }
        }

        public async Task<List<UsuarioRoleURL>> Lista()
        {
            var result = await _http.GetFromJsonAsync<ResponseAPI<List<UsuarioRoleURL>>>("api/UsuarioRole/Lista");

            if (result!.EsCorrecto)
                return result.Valor!;
            else
            {
                throw new Exception(result.Mensajes);
            }
        }
    }
}
