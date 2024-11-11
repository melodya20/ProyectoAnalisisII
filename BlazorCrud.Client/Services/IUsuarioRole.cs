using BlazorCrud.Shared;

namespace BlazorCrud.Client.Services
{
    public interface IUsuarioRole
    {
        Task<List<UsuarioRoleURL>> Lista();

        Task<UsuarioRoleURL> Buscar(string idUsuario, int idRole);

        Task<bool> Guardar(UsuarioRoleURL usuarioR);

        Task<bool> Editar(UsuarioRoleURL usuarioR);

        Task<bool> Eliminar(string idUsuario, int idRole);
    }
}

