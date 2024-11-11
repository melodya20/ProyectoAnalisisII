using BlazorCrud.Shared;

namespace BlazorCrud.Client.Services
{
    public interface IMenu
    {
        Task<List<MenuMN>> Lista();

        Task<MenuMN> Buscar(int id);

        Task<int> Guardar(MenuMN menu);

        Task<int> Editar(MenuMN menu);

        Task<bool> Eliminar(int id);
    }
}
