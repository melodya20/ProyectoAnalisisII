using BlazorCrud.Shared;

namespace BlazorCrud.Client.Services
{
    public interface ISucursal
    {
        Task<List<SucursalSC>> Lista();

        Task<SucursalSC> Buscar(int id);

        Task<int> Guardar(SucursalSC sucursal);

        Task<int> Editar(SucursalSC sucursal);

        Task<bool> Eliminar(int id);
    }
}
