using BlazorCrud.Shared;

namespace BlazorCrud.Client.Services
{
    public interface ITipoAcceso
    {
        Task<List<TipoAccesoTA>> Lista();

        Task<TipoAccesoTA> Buscar(int id);

        Task<int> Guardar(TipoAccesoTA tipoacceso);

        Task<int> Editar(TipoAccesoTA tipoacceso);

        Task<bool> Eliminar(int id);
    }
}
