using BlazorCrud.Shared;

namespace BlazorCrud.Client.Services
{
    public interface IStatusCuentum
    {
        Task<List<StatusCuentumST>> Lista();

        Task<StatusCuentumST> Buscar(int id);

        Task<int> Guardar(StatusCuentumST status);

        Task<int> Editar(StatusCuentumST status);

        Task<bool> Eliminar(int id);

    }
}


