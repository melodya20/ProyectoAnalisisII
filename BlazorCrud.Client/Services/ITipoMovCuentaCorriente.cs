using BlazorCrud.Shared;

namespace BlazorCrud.Client.Services
{
    public interface ITipoMovCuentaCorriente
    {
        Task<List<TipoMovCuentaCorrienteCC>> Lista();

        Task<TipoMovCuentaCorrienteCC> Buscar(int id);

        Task<int> Guardar(TipoMovCuentaCorrienteCC tipomovcuentacorriente);

        Task<int> Editar(TipoMovCuentaCorrienteCC tipomovcuentacorriente);

        Task<bool> Eliminar(int id);
    }
}
