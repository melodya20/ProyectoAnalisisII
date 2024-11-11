using BlazorCrud.Shared;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Net.Http.Json;

namespace BlazorCrud.Client.Services
{
    public class EmpresaService : IEmpresaService
    {
        private readonly HttpClient _http;
        private readonly IJSRuntime _js;
        private readonly AuthenticationStateProvider _authenticationStateProvider;


        public EmpresaService(HttpClient http, IJSRuntime js, AuthenticationStateProvider authenticationStateProvider)
        {
            _http = http;
            _js = js;
            _authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<EmpresaEM> Buscar(int id)
        {
            var result = await _http.GetFromJsonAsync<ResponseAPI<EmpresaEM>>($"/api/Empresa/Buscar/{id}");

            if (result!.EsCorrecto)
                return result.Valor!;
            else
            {
                throw new Exception(result.Mensajes);
            }
        }

        public async Task<int> Editar(EmpresaEM empresa)
        {

            empresa.FechaModificacion = DateTime.Now;

            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var userName = authState.User.Identity?.Name;


            empresa.UsuarioModificacion = userName ?? "Desconocido";


            var result = await _http.PutAsJsonAsync($"/api/Empresa/Editar/{empresa.IdEmpresa}", empresa);
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
            var result = await _http.DeleteAsync($"/api/Empresa/Eliminar/{id}");
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.EsCorrecto)
                return response.EsCorrecto!;
            else
            {
                throw new Exception(response.Mensajes);
            }
        }

        public Task<EmpresaEM> GetEmpresaById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<EmpresaEM>> GetEmpresas()
        {
            throw new NotImplementedException();
        }

        public async Task<int> Guardar(EmpresaEM empresa)
        {

            empresa.FechaCreacion = DateTime.Now;


            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var userName = authState.User.Identity?.Name;


            empresa.UsuarioCreacion = userName ?? "Desconocido";

            var result = await _http.PostAsJsonAsync($"/api/Empresa/Guardar", empresa);
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.EsCorrecto)
                return response.Valor!;
            else
            {
                throw new Exception(response.Mensajes);
            }
        }


        public async Task<List<EmpresaEM>> Lista()
        {
            var result = await _http.GetFromJsonAsync<ResponseAPI<List<EmpresaEM>>>("api/Empresa/Lista");

            if (result!.EsCorrecto)
                return result.Valor!;
            else
            {
                throw new Exception(result.Mensajes);
            }
        }
    }
}
