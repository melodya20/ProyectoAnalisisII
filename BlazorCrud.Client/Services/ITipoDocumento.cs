using BlazorCrud.Shared;

namespace BlazorCrud.Client.Services
{
    public interface ITipoDocumento
    {
        Task<List<TipoDocumentoTD>> Lista();

        Task<TipoDocumentoTD> Buscar(int id);

        Task<int> Guardar(TipoDocumentoTD tipodocumento);

        Task<int> Editar(TipoDocumentoTD tipodocumento);

        Task<bool> Eliminar(int id);
    }
}
