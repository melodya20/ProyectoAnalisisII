using BlazorCrud.Shared;

namespace BlazorCrud.Client.Services
{
    public interface IUsuarioServices
    {
            
        Task<List<UsuarioUSO>> Lista();

        Task<UsuarioUSO> Buscar(string id);

        Task<string> Guardar(UsuarioUSO usuario);

        Task<string> Editar(UsuarioUSO usuario);

        Task<bool> Eliminar(string id);

    }

}

