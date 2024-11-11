using BlazorCrud.Shared;
using System.Net.Http.Json;

namespace BlazorCrud.Client.Services
{
    public class ModuloService : IModulo
    {
        private readonly HttpClient _http;

        public ModuloService(HttpClient http)
        {
            _http = http;
        }

        public async Task<ModuloMD> Buscar(int id)
        {
            var result = await _http.GetFromJsonAsync<ResponseAPI<ModuloMD>>($"/api/Modulo/Buscar/{id}");

            if (result!.EsCorrecto)
                return result.Valor!;
            else
            {
                throw new Exception(result.Mensajes);
            }
        }

        public async Task<int> Editar(ModuloMD modulo)
        {
            var result = await _http.PutAsJsonAsync($"/api/Modulo/Editar/{modulo.IdModulo}", modulo);
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.EsCorrecto)
                return response.Valor!;
            else
            {
                throw new Exception(response.Mensajes);
            }
        }

        public async  Task<bool> Eliminar(int id)
        {
            var result = await _http.DeleteAsync($"/api/Modulo/Eliminar/{id}");
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.EsCorrecto)
                return response.EsCorrecto!;
            else
            {
                throw new Exception(response.Mensajes);
            }
        }

        public async Task<int> Guardar(ModuloMD modulo)
        {
            var result = await _http.PostAsJsonAsync("/api/Modulo/Guardar", modulo);
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.EsCorrecto)
                return response.Valor!;
            else
            {
                throw new Exception(response.Mensajes);
            }
        }

        public async Task<List<ModuloMD>> Lista()
        {
            var result = await _http.GetFromJsonAsync<ResponseAPI<List<ModuloMD>>>("api/Modulo/Lista");

            if (result!.EsCorrecto)
                return result.Valor!;
            else
            {
                throw new Exception(result.Mensajes);
            }
        }
    }
}
