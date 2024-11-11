using BlazorCrud.Shared;

namespace BlazorCrud.Client.Services
{
    public interface IRoleOP
    {
        Task<List<RoleOP>> Lista();

        Task<RoleOP> Buscar(int idRole, int idOpcion);

        Task<bool> Guardar(RoleOP roleOpcion);

        Task<bool> Editar(RoleOP roleOpcion);

        Task<bool> Eliminar(int idRole, int idOpcion);
    }
}
