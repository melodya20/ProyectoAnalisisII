using BlazorCrud.Shared;

namespace BlazorCrud.Client.Services
{
    public interface IModulo
    {
        Task<List<ModuloMD>> Lista();

        Task<ModuloMD> Buscar(int id);

        Task<int> Guardar(ModuloMD modulo);

        Task<int> Editar(ModuloMD modulo);

        Task<bool> Eliminar(int id);
    }
}
