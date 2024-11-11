using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using BlazorCrud.Server.Models;
using BlazorCrud.Shared;
using Microsoft.EntityFrameworkCore;

namespace BlazorCrud.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpresaController : ControllerBase
    {
        private readonly ProyectoAnalisisContext _dbcontent;

        public EmpresaController(ProyectoAnalisisContext dbcontent)
        {
            _dbcontent = dbcontent;
        }

        [HttpGet]
        [Route("Lista")]

        public async Task<IActionResult> Lista()
        {
            var responseApi = new ResponseAPI<List<EmpresaEM>>();
            var listaEmpresaEM = new List<EmpresaEM>();
            try
            {
                foreach (var item in await _dbcontent.Empresas.ToListAsync())
                {
                    listaEmpresaEM.Add(new EmpresaEM
                    {
                        IdEmpresa = item.IdEmpresa,
                        Nombre = item.Nombre,
                        Direccion = item.Direccion,
                        Nit = item.Nit,
                        PasswordCantidadMayusculas = item.PasswordCantidadMayusculas,
                        PasswordCantidadMinusculas = item.PasswordCantidadMinusculas,
                        PasswordCantidadCaracteresEspeciales = item.PasswordCantidadCaracteresEspeciales,
                        PasswordCantidadCaducidadDias = item.PasswordCantidadCaducidadDias,
                        PasswordLargo = item.PasswordLargo,
                        PasswordIntentosAntesDeBloquear = item.PasswordIntentosAntesDeBloquear,
                        PasswordCantidadNumeros = item.PasswordCantidadNumeros,
                        PasswordCantidadPreguntasValidar = item.PasswordCantidadPreguntasValidar,
                        FechaCreacion = item.FechaCreacion,
                        UsuarioCreacion = item.UsuarioCreacion,
                        FechaModificacion = item.FechaModificacion,
                        UsuarioModificacion = item.UsuarioModificacion,
                    });
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = listaEmpresaEM;
                }
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensajes = ex.Message;
            }

            return Ok(responseApi);
        }

        [HttpGet]
        [Route("Buscar/{id}")]

        public async Task<IActionResult> Buscar(int id)
        {
            var responseApi = new ResponseAPI<EmpresaEM>();
            var EmpresaEM = new EmpresaEM();

            try
            {
                var dbEmpresaEM = await _dbcontent.Empresas.FirstOrDefaultAsync(X => X.IdEmpresa == id);
                if (dbEmpresaEM != null)
                {


                    EmpresaEM.IdEmpresa = dbEmpresaEM.IdEmpresa;
                    EmpresaEM.Nombre = dbEmpresaEM.Nombre;
                    EmpresaEM.Direccion = dbEmpresaEM.Direccion;
                    EmpresaEM.Nit = dbEmpresaEM.Nit;
                    EmpresaEM.PasswordCantidadMayusculas = dbEmpresaEM.PasswordCantidadMayusculas;
                    EmpresaEM.PasswordCantidadMinusculas = dbEmpresaEM.PasswordCantidadMinusculas;
                    EmpresaEM.PasswordCantidadCaracteresEspeciales = dbEmpresaEM.PasswordCantidadCaracteresEspeciales;
                    EmpresaEM.PasswordCantidadCaducidadDias = dbEmpresaEM.PasswordCantidadCaducidadDias;
                    EmpresaEM.PasswordLargo = dbEmpresaEM.PasswordLargo;
                    EmpresaEM.PasswordIntentosAntesDeBloquear = dbEmpresaEM.PasswordIntentosAntesDeBloquear;
                    EmpresaEM.PasswordCantidadNumeros = dbEmpresaEM.PasswordCantidadNumeros;
                    EmpresaEM.PasswordCantidadPreguntasValidar = dbEmpresaEM.PasswordCantidadPreguntasValidar;
                    EmpresaEM.FechaCreacion = dbEmpresaEM.FechaCreacion;
                    EmpresaEM.UsuarioCreacion = dbEmpresaEM.UsuarioCreacion;
                    EmpresaEM.FechaModificacion = dbEmpresaEM.FechaModificacion;
                    EmpresaEM.UsuarioModificacion = dbEmpresaEM.UsuarioModificacion;

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = EmpresaEM;

                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensajes = "No Encontrado";
                }


            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensajes = ex.Message;
            }

            return Ok(responseApi);
        }

        [HttpPost]
        [Route("Guardar")]

        public async Task<IActionResult> Guardar(EmpresaEM empresa)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbEmpresa = new Empresa
                {

                    IdEmpresa = empresa.IdEmpresa,
                    Nombre = empresa.Nombre,
                    Direccion = empresa.Direccion,
                    Nit = empresa.Nit,
                    PasswordCantidadMayusculas = empresa.PasswordCantidadMayusculas,
                    PasswordCantidadMinusculas = empresa.PasswordCantidadMinusculas,
                    PasswordCantidadCaracteresEspeciales = empresa.PasswordCantidadCaracteresEspeciales,
                    PasswordCantidadCaducidadDias = empresa.PasswordCantidadCaducidadDias,
                    PasswordLargo = empresa.PasswordLargo,
                    PasswordIntentosAntesDeBloquear = empresa.PasswordIntentosAntesDeBloquear,
                    PasswordCantidadNumeros = empresa.PasswordCantidadNumeros,
                    PasswordCantidadPreguntasValidar = empresa.PasswordCantidadPreguntasValidar,
                    FechaCreacion = empresa.FechaCreacion,
                    UsuarioCreacion = empresa.UsuarioCreacion,
                    FechaModificacion = empresa.FechaModificacion,
                    UsuarioModificacion = empresa.UsuarioModificacion,
                };

                _dbcontent.Empresas.Add(dbEmpresa);
                await _dbcontent.SaveChangesAsync();

                if (dbEmpresa.IdEmpresa != 0)
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = dbEmpresa.IdEmpresa;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensajes = "No Guardado";
                }

            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensajes = ex.Message;
            }

            return Ok(responseApi);
        }


        [HttpPut]
        [Route("Editar/{id}")]

        public async Task<IActionResult> Editar(EmpresaEM empresa, int id)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbEmpresaEM = await _dbcontent.Empresas.FirstOrDefaultAsync(e => e.IdEmpresa== id);


                if (dbEmpresaEM != null)
                {

                    dbEmpresaEM.IdEmpresa = empresa.IdEmpresa;
                    dbEmpresaEM.Nombre = empresa.Nombre;
                    dbEmpresaEM.Direccion = empresa.Direccion;
                    dbEmpresaEM.Nit = empresa.Nit;
                    dbEmpresaEM.PasswordCantidadMayusculas = empresa.PasswordCantidadMayusculas;
                    dbEmpresaEM.PasswordCantidadMinusculas = empresa.PasswordCantidadMinusculas;
                    dbEmpresaEM.PasswordCantidadCaracteresEspeciales = empresa.PasswordCantidadCaracteresEspeciales;
                    dbEmpresaEM.PasswordCantidadCaducidadDias = empresa.PasswordCantidadCaducidadDias;
                    dbEmpresaEM.PasswordLargo = empresa.PasswordLargo;
                    dbEmpresaEM.PasswordIntentosAntesDeBloquear = empresa.PasswordIntentosAntesDeBloquear;
                    dbEmpresaEM.PasswordCantidadNumeros = empresa.PasswordCantidadNumeros;
                    dbEmpresaEM.PasswordCantidadPreguntasValidar = empresa.PasswordCantidadPreguntasValidar;
                    dbEmpresaEM.FechaCreacion = empresa.FechaCreacion;
                    dbEmpresaEM.UsuarioCreacion = empresa.UsuarioCreacion;
                    dbEmpresaEM.FechaModificacion = empresa.FechaModificacion;
                    dbEmpresaEM.UsuarioModificacion = empresa.UsuarioModificacion;


                    _dbcontent.Empresas.Update(dbEmpresaEM);
                    await _dbcontent.SaveChangesAsync();

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = dbEmpresaEM.IdEmpresa;


                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensajes = "No  encotrado";
                }

            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensajes = ex.Message;
            }

            return Ok(responseApi);
        }

        [HttpDelete]
        [Route("Eliminar/{id}")]

        public async Task<IActionResult> Eliminar(int id)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbEmpresa = await _dbcontent.Empresas.FirstOrDefaultAsync(e => e.IdEmpresa == id);


                if (dbEmpresa != null)
                {

                    _dbcontent.Empresas.Remove(dbEmpresa);
                    await _dbcontent.SaveChangesAsync();

                    responseApi.EsCorrecto = true;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensajes = "No  encotrado";
                }

            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensajes = ex.Message;
            }

            return Ok(responseApi);
        }
    }
}
