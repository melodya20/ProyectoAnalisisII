using BlazorCrud.Shared;

namespace BlazorCrud.Client.Services
{
    public interface ITipoSaldoCuenta
    {
        Task<List<TipoSaldoCuentaTSC>> Lista();

        Task<TipoSaldoCuentaTSC> Buscar(int id);

        Task<int> Guardar(TipoSaldoCuentaTSC tscuenta);

        Task<int> Editar(TipoSaldoCuentaTSC tscuenta);

        Task<bool> Eliminar(int id);
    }
}
