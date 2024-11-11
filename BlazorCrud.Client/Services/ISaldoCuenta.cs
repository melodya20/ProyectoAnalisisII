using BlazorCrud.Shared;

namespace BlazorCrud.Client.Services
{
    public interface ISaldoCuenta
    {

        Task<List<SaldoCuentumSC>> Lista();

        Task<SaldoCuentumSC> Buscar(int id);

        Task<int> Guardar(SaldoCuentumSC saldo);

        Task<int> AgregarSaldo(SaldoCuentumSC saldo);

        Task<SaldoCuentumSC?> ObtenerPorPersona(int idPersona);
    }
}
