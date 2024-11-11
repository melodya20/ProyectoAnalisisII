using BlazorCrud.Shared;

namespace BlazorCrud.Client.Services
{
    public interface IUsuarioPregunta
    {

        Task<List<UsuarioPreguntumUP>> Lista();

        Task<UsuarioPreguntumUP> Buscar(int id);

        Task<int> Guardar(UsuarioPreguntumUP usuarioPreguntum);

        Task<int> Editar(UsuarioPreguntumUP usuarioPreguntum);

        Task<bool> Eliminar(int id);
    }
}
