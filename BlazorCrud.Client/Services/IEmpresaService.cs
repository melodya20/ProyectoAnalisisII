using BlazorCrud.Shared;

namespace BlazorCrud.Client.Services
{
    public interface IEmpresaService
    {
        Task<List<EmpresaEM>> Lista();

        Task<EmpresaEM> Buscar(int id);

        Task<int> Guardar(EmpresaEM empresa);

        Task<int> Editar(EmpresaEM empresa);

        Task<bool> Eliminar(int id);

    }
}
