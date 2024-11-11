using BlazorCrud.Shared;
using System.Net.Http.Json;

namespace BlazorCrud.Client.Services
{
    public class EstadoCivilService : IEstadoCivil
    {
        private readonly HttpClient _http;

        public EstadoCivilService(HttpClient http)
        {
            _http = http;
        }

        public async Task<EstadoCivilEC> Buscar(int id)
        {
            var result = await _http.GetFromJsonAsync<ResponseAPI<EstadoCivilEC>>($"/api/EstadoCivil/Buscar/{id}");

            if (result!.EsCorrecto)
                return result.Valor!;
            else
            {
                throw new Exception(result.Mensajes);
            }
        }

        public async Task<int> Editar(EstadoCivilEC estadoCivil)
        {
            var result = await _http.PutAsJsonAsync($"/api/EstadoCivil/Editar/{estadoCivil.IdEstadoCivil}", estadoCivil);
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
            var result = await _http.DeleteAsync($"/api/EstadoCivil/Eliminar/{id}");
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.EsCorrecto)
                return response.EsCorrecto!;
            else
            {
                throw new Exception(response.Mensajes);
            }
        }

        public async Task<int> Guardar(EstadoCivilEC estadoCivil)
        {
            var result = await _http.PostAsJsonAsync("/api/EstadoCivil/Guardar", estadoCivil);
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.EsCorrecto)
                return response.Valor!;
            else
            {
                throw new Exception(response.Mensajes);
            }
        }

        public async Task<List<EstadoCivilEC>> Lista()
        {
            var result = await _http.GetFromJsonAsync<ResponseAPI<List<EstadoCivilEC>>>("api/EstadoCivil/Lista");

            if (result!.EsCorrecto)
                return result.Valor!;
            else
            {
                throw new Exception(result.Mensajes);
            }
        }
    }
}
