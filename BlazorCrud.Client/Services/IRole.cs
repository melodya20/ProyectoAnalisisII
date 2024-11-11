using BlazorCrud.Shared;

namespace BlazorCrud.Client.Services
{
    public interface IRole
    {
        Task<List<RoleRL>> Lista();

        Task<RoleRL> Buscar(int id);

        Task<int> Guardar(RoleRL role);

        Task<int> Editar(RoleRL role);

        Task<bool> Eliminar(int id);
    }
}
