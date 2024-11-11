using BlazorCrud.Shared;

namespace BlazorCrud.Client.Services
{
    public interface IStatusUsuario
    {
        Task<List<StatusUsuarioST>> Lista();

        Task<StatusUsuarioST> Buscar(int id);

        Task<int> Guardar(StatusUsuarioST statususuario);

        Task<int> Editar(StatusUsuarioST statususuario);

        Task<bool> Eliminar(int id);
    }
}
