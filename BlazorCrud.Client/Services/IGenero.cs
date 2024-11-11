using BlazorCrud.Shared;

namespace BlazorCrud.Client.Services
{
    public interface IGenero
    {
        Task<List<GeneroGR>> Lista();

        Task<GeneroGR> Buscar(int id);

        Task<int> Guardar(GeneroGR genero);

        Task<int> Editar(GeneroGR genero);

        Task<bool> Eliminar(int id);
    }
}
