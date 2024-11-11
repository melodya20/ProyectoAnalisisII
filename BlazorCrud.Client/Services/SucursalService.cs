using BlazorCrud.Client.Services;
using BlazorCrud.Shared;
using System.Net.Http.Json;

namespace BlazorCrud.Client.Services
{

    public class SucursalService : ISucursal
{
    private readonly HttpClient _http;

    public SucursalService(HttpClient http)
    {
        _http = http;
    }

    public async Task<SucursalSC> Buscar(int id)
    {
        var result = await _http.GetFromJsonAsync<ResponseAPI<SucursalSC>>($"/api/Sucursal/Buscar/{id}");

        if (result!.EsCorrecto)
            return result.Valor!;
        else
        {
            throw new Exception(result.Mensajes);
        }
    }

    public async Task<int> Editar(SucursalSC sucursal)
    {
        var result = await _http.PutAsJsonAsync($"/api/Sucursal/Editar/{sucursal.IdSucursal}", sucursal);
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
        var result = await _http.DeleteAsync($"/api/Sucursal/Eliminar/{id}");
        var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

        if (response!.EsCorrecto)
            return response.EsCorrecto!;
        else
        {
            throw new Exception(response.Mensajes);
        }
    }

    public async Task<int> Guardar(SucursalSC sucursal)
    {
        var result = await _http.PostAsJsonAsync("/api/Sucursal/Guardar", sucursal);
        var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

        if (response!.EsCorrecto)
            return response.Valor!;
        else
        {
            throw new Exception(response.Mensajes);
        }
    }

    public async Task<List<SucursalSC>> Lista()
    {
        var result = await _http.GetFromJsonAsync<ResponseAPI<List<SucursalSC>>>("api/Sucursal/Lista");

        if (result!.EsCorrecto)
            return result.Valor!;
        else
        {
            throw new Exception(result.Mensajes);
        }
    }
}
}