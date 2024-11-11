using BlazorCrud.Shared;

namespace BlazorCrud.Client.Services
{
    public interface IMovCuenta
    {
        Task<List<MovimientoCuentaMC>> Lista();

        Task<MovimientoCuentaMC> Buscar(int id);

        Task<int> Guardar(MovimientoCuentaMC MovCuenta);

        Task<List<MovimientoCuentaMC>> ObtenerMovimientosPorPersonaMes(int idPersona, int mes);

    }
}
