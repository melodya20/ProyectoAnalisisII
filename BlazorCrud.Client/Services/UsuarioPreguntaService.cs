using BlazorCrud.Shared;
using System.Net.Http.Json;

namespace BlazorCrud.Client.Services
{
    public class UsuarioPreguntaService : IUsuarioPregunta
    {
        private readonly HttpClient _http;

        public UsuarioPreguntaService(HttpClient http)
        {
            _http = http;
        }

        public async Task<UsuarioPreguntumUP> Buscar(int id)
        {
            var result = await _http.GetFromJsonAsync<ResponseAPI<UsuarioPreguntumUP>>($"/api/UsuarioPregunta/Buscar/{id}");

            if (result!.EsCorrecto)
                return result.Valor!;
            else
            {
                throw new Exception(result.Mensajes);
            }
        }

        public async Task<int> Editar(UsuarioPreguntumUP usuarioPregunta)
        {
            var result = await _http.PutAsJsonAsync($"/api/UsuarioPregunta/Editar/{usuarioPregunta.IdPregunta}", usuarioPregunta);
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
            var result = await _http.DeleteAsync($"/api/UsuarioPregunta/Eliminar/{id}");
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.EsCorrecto)
                return response.EsCorrecto!;
            else
            {
                throw new Exception(response.Mensajes);
            }
        }

        public async Task<int> Guardar(UsuarioPreguntumUP usuariopregunta)
        {
            var result = await _http.PostAsJsonAsync("/api/UsuarioPregunta/Guardar", usuariopregunta);
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.EsCorrecto)
                return response.Valor!;
            else
            {
                throw new Exception(response.Mensajes);
            }
        }

        public async Task<List<UsuarioPreguntumUP>> Lista()
        {
            var result = await _http.GetFromJsonAsync<ResponseAPI<List<UsuarioPreguntumUP>>>("api/UsuarioPregunta/Lista");

            if (result!.EsCorrecto)
                return result.Valor!;
            else
            {
                throw new Exception(result.Mensajes);
            }
        }
    }
}
