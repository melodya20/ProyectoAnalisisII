using BlazorCrud.Shared;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace BlazorCrud.Client.Services
{
    public class MenuService : IMenu
    {
        private readonly HttpClient _http;

        public MenuService(HttpClient http)
        {
            _http = http;
        }

        public async Task<MenuMN> Buscar(int id)
        {
            var result = await _http.GetFromJsonAsync<ResponseAPI<MenuMN>>($"/api/Menu/Buscar/{id}");

            if (result!.EsCorrecto)
                return result.Valor!;
            else
            {
                throw new Exception(result.Mensajes);
            }
        }

        public async Task<int> Editar(MenuMN menu)
        {
            var result = await _http.PutAsJsonAsync($"/api/Menu/Editar/{menu.IdMenu}", menu);
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
            var result = await _http.DeleteAsync($"/api/Menu/Eliminar/{id}");
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.EsCorrecto)
                return response.EsCorrecto!;
            else
            {
                throw new Exception(response.Mensajes);
            }
        }

        public async Task<int> Guardar(MenuMN menu)
        {
            var result = await _http.PostAsJsonAsync("/api/Menu/Guardar", menu);
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.EsCorrecto)
                return response.Valor!;
            else
            {
                throw new Exception(response.Mensajes);
            }
        }

        public async Task<List<MenuMN>> Lista()
        {
            var result = await _http.GetFromJsonAsync<ResponseAPI<List<MenuMN>>>("api/Menu/Lista");

            if (result!.EsCorrecto)
                return result.Valor!;
            else
            {
                throw new Exception(result.Mensajes);
            }
        }
    }
}
