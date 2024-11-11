using BlazorCrud.Shared;

namespace BlazorCrud.Client.Services
{
    public interface IBitacora
    {
        Task<List<BitacoraBT>> Lista();

        Task<BitacoraBT> Buscar(int id);

        Task<int> Guardar(BitacoraBT bitacora);

        Task<int> Editar(BitacoraBT bitacora);

        Task<bool> Eliminar(int id);
    }
}
