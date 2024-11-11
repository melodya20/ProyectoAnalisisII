using BlazorCrud.Shared;
using System.Net.Http.Json;

namespace BlazorCrud.Client.Services
{
    public class BitacoraService : IBitacora
    {
        private readonly HttpClient _http;

        public BitacoraService(HttpClient http)
        {
            _http = http;
        }

        public async Task<BitacoraBT> Buscar(int id)
        {
            var result = await _http.GetFromJsonAsync<ResponseAPI<BitacoraBT>>($"/api/bitacora_acceso/Buscar/{id}");

            if (result!.EsCorrecto)
                return result.Valor!;
            else
            {
                throw new Exception(result.Mensajes);
            }
        }

        public async Task<int> Editar(BitacoraBT bitacora)
        {
            var result = await _http.PutAsJsonAsync($"/api/bitacora_acceso/Editar/{bitacora.IdBitacoraAcceso}", bitacora);
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
            var result = await _http.DeleteAsync($"/api/bitacora_acceso/Eliminar/{id}");
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.EsCorrecto)
                return response.EsCorrecto!;
            else
            {
                throw new Exception(response.Mensajes);
            }
        }

        public async Task<int> Guardar(BitacoraBT bitacora)
        {
            var result = await _http.PostAsJsonAsync("/api/bitacora_acceso/Guardar", bitacora);
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.EsCorrecto)
                return response.Valor!;
            else
            {
                throw new Exception(response.Mensajes);
            }
        }

        public async Task<List<BitacoraBT>> Lista()
        {
            var result = await _http.GetFromJsonAsync<ResponseAPI<List<BitacoraBT>>>("api/bitacora_acceso/Lista");

            if (result!.EsCorrecto)
                return result.Valor!;
            else
            {
                throw new Exception(result.Mensajes);
            }
        }
    }
}

