using BlazorCrud.Shared;

namespace BlazorCrud.Client.Services
{
    public interface IPersona
    {
        Task<List<PersonaP>> Lista();

        Task<PersonaP> Buscar(int id);

        Task<int> Guardar(PersonaP persona);

        Task<int> Editar(PersonaP persona);

        Task<bool> Eliminar(int id);
    }
}
