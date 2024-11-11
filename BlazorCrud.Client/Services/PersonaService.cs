using BlazorCrud.Shared;
using System.Net.Http.Json;

namespace BlazorCrud.Client.Services
{
    public class PersonaService : IPersona
    {
        private readonly HttpClient _http;

        public PersonaService(HttpClient http)
        {
            _http = http;
        }

        public async Task<PersonaP> Buscar(int id)
        {
            var result = await _http.GetFromJsonAsync<ResponseAPI<PersonaP>>($"/api/Persona/Buscar/{id}");

            if (result!.EsCorrecto)
                return result.Valor!;
            else
            {
                throw new Exception(result.Mensajes);
            }
        }

        public async Task<int> Editar(PersonaP persona)
        {
            var result = await _http.PutAsJsonAsync($"/api/Persona/Editar/{persona.IdPersona}", persona);
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
            var result = await _http.DeleteAsync($"/api/Persona/Eliminar/{id}");
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.EsCorrecto)
                return response.EsCorrecto!;
            else
            {
                throw new Exception(response.Mensajes);
            }
        }

        public async Task<int> Guardar(PersonaP persona)
        {
            var result = await _http.PostAsJsonAsync("/api/Persona/Guardar", persona);
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.EsCorrecto)
                return response.Valor!;
            else
            {
                throw new Exception(response.Mensajes);
            }
        }

        public async Task<List<PersonaP>> Lista()
        {
            var result = await _http.GetFromJsonAsync<ResponseAPI<List<PersonaP>>>("api/Persona/Lista");

            if (result!.EsCorrecto)
                return result.Valor!;
            else
            {
                throw new Exception(result.Mensajes);
            }
        }
    }
}
