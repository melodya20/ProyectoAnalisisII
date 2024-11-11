using BlazorCrud.Shared;

namespace BlazorCrud.Client.Services
{
    public interface IOpcion
    {
        Task<List<OpcionOP>> Lista();

        Task<OpcionOP> Buscar(int id);

        Task<int> Guardar(OpcionOP opcion);

        Task<int> Editar(OpcionOP opcion);

        Task<bool> Eliminar(int id);
    }
}
