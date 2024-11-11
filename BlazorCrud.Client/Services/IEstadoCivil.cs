using BlazorCrud.Shared;

namespace BlazorCrud.Client.Services
{
    public interface IEstadoCivil
    {
        Task<List<EstadoCivilEC>> Lista();

        Task<EstadoCivilEC> Buscar(int id);

        Task<int> Guardar(EstadoCivilEC estadocivil);

        Task<int> Editar(EstadoCivilEC estadocivil);

        Task<bool> Eliminar(int id);
    }
}
