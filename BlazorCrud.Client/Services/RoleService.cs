using BlazorCrud.Shared;
using System.Net.Http.Json;

namespace BlazorCrud.Client.Services
{
    public class RoleService : IRole
    {
        private readonly HttpClient _http;

        public RoleService(HttpClient http)
        {
            _http = http;
        }

        public async Task<RoleRL> Buscar(int id)
        {
            var result = await _http.GetFromJsonAsync<ResponseAPI<RoleRL>>($"/api/Role/Buscar/{id}");

            if (result!.EsCorrecto)
                return result.Valor!;
            else
            {
                throw new Exception(result.Mensajes);
            }
        }

        public async Task<int> Editar(RoleRL role)
        {
            var result = await _http.PutAsJsonAsync($"/api/Role/Editar/{role.IdRole}", role);
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
            var result = await _http.DeleteAsync($"/api/Role/Eliminar/{id}");
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.EsCorrecto)
                return response.EsCorrecto!;
            else
            {
                throw new Exception(response.Mensajes);
            }
        }

        public async Task<int> Guardar(RoleRL role)
        {
            var result = await _http.PostAsJsonAsync("/api/Role/Guardar", role);
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.EsCorrecto)
                return response.Valor!;
            else
            {
                throw new Exception(response.Mensajes);
            }
        }

        public async Task<List<RoleRL>> Lista()
        {
            var result = await _http.GetFromJsonAsync<ResponseAPI<List<RoleRL>>>("api/Role/Lista");

            if (result!.EsCorrecto)
                return result.Valor!;
            else
            {
                throw new Exception(result.Mensajes);
            }
        }
    }
}
