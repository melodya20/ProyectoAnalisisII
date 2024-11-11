using BlazorCrud.Shared;
using System.Net.Http.Json;

namespace BlazorCrud.Client.Services
{
    public class RoleOpService : IRoleOP
    {
        private readonly HttpClient _http;

        public RoleOpService(HttpClient http)
        {
            _http = http;
        }

        public async Task<RoleOP> Buscar(int idRole, int idOpcion)
        {
            var result = await _http.GetFromJsonAsync<ResponseAPI<RoleOP>>($"/api/RoleOpcion/Buscar/{idRole}/{idOpcion}");

            if (result!.EsCorrecto)
                return result.Valor!;
            else
            {
                throw new Exception(result.Mensajes);
            }
        }

        public async Task<bool> Editar(RoleOP roleOpcion)
        {
            var result = await _http.PutAsJsonAsync($"/api/RoleOpcion/Editar/{roleOpcion.IdRole}/{roleOpcion.IdOpcion}", roleOpcion);
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<bool>>();

            if (response!.EsCorrecto)
                return response.Valor!;
            else
            {
                throw new Exception(response.Mensajes);
            }
        }

        public async Task<bool> Eliminar(int idRole, int idOpcion)
        {
            var result = await _http.DeleteAsync($"/api/RoleOpcion/Eliminar/{idRole}/{idOpcion}");
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<bool>>();

            if (response!.EsCorrecto)
                return response.Valor!;
            else
            {
                throw new Exception(response.Mensajes);
            }
        }


        public async Task<bool> Guardar(RoleOP roleOpcion)
        {
            try
            {
                var result = await _http.PostAsJsonAsync($"/api/RoleOpcion/Guardar", roleOpcion);
                var response = await result.Content.ReadFromJsonAsync<ResponseAPI<bool>>();

                if (response!.EsCorrecto)
                    return response.Valor!;
                else
                {
                    // Imprime detalles del error para depuración
                    Console.WriteLine($"Error: {response.Mensajes}");
                    throw new Exception(response.Mensajes);
                }
            }
            catch (Exception ex)
            {
                // Maneja o imprime la excepción para depuración
                Console.WriteLine($"Exception: {ex.Message}, StackTrace: {ex.StackTrace}");
                throw;
            }
        }


        public async Task<List<RoleOP>> Lista()
        {
            var result = await _http.GetFromJsonAsync<ResponseAPI<List<RoleOP>>>("api/RoleOpcion/Lista");

            if (result!.EsCorrecto)
                return result.Valor!;
            else
            {
                throw new Exception(result.Mensajes);
            }
        }
    }
}
