using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using BlazorCrud.Server.Models;
using BlazorCrud.Shared;
using Microsoft.EntityFrameworkCore;

namespace BlazorCrud.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SucursalController : ControllerBase
    {
        private readonly ProyectoAnalisisContext _dbcontent;

        public SucursalController(ProyectoAnalisisContext dbcontent)
        {
            _dbcontent = dbcontent;
        }

        [HttpGet]
        [Route("Lista")]

        public async Task<IActionResult> Lista()
        {
            var responseApi = new ResponseAPI<List<SucursalSC>>();
            var listaSucursalSC = new List<SucursalSC>();
            try
            {
                foreach (var item in await _dbcontent.Sucursals.Include(d => d.IdEmpresaNavigation).ToListAsync())
                {
                    listaSucursalSC.Add(new SucursalSC
                    {
                        IdSucursal = item.IdSucursal,
                        Nombre = item.Nombre,
                        Direccion = item.Direccion,
                        IdEmpresa = item.IdEmpresa,
                        Empresa = new EmpresaEM
                        {
                            IdEmpresa = item.IdEmpresaNavigation.IdEmpresa,
                            Nombre = item.IdEmpresaNavigation.Nombre,
                            Direccion = item.IdEmpresaNavigation.Direccion,
                            Nit = item.IdEmpresaNavigation.Nit,
                            PasswordCantidadMayusculas = item.IdEmpresaNavigation.PasswordCantidadMayusculas,
                            PasswordCantidadMinusculas = item.IdEmpresaNavigation.PasswordCantidadMinusculas,
                            PasswordCantidadCaracteresEspeciales = item.IdEmpresaNavigation.PasswordCantidadCaracteresEspeciales,
                            PasswordCantidadCaducidadDias = item.IdEmpresaNavigation.PasswordCantidadCaducidadDias,
                            PasswordLargo = item.IdEmpresaNavigation.PasswordLargo,
                            PasswordIntentosAntesDeBloquear = item.IdEmpresaNavigation.PasswordIntentosAntesDeBloquear,
                            PasswordCantidadNumeros = item.IdEmpresaNavigation.PasswordCantidadNumeros,
                            PasswordCantidadPreguntasValidar = item.IdEmpresaNavigation.PasswordCantidadPreguntasValidar,
                            FechaCreacion = item.IdEmpresaNavigation.FechaCreacion,
                            UsuarioCreacion = item.IdEmpresaNavigation.UsuarioCreacion,
                            FechaModificacion = item.IdEmpresaNavigation.FechaModificacion,
                            UsuarioModificacion = item.IdEmpresaNavigation.UsuarioModificacion,
                        },
                        FechaCreacion = item.FechaCreacion,
                        UsuarioCreacion = item.UsuarioCreacion,
                        FechaModificacion = item.FechaModificacion,
                        UsuarioModificacion = item.UsuarioModificacion,

                    });
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = listaSucursalSC;
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
            var responseApi = new ResponseAPI<SucursalSC>();
            var SucursalSC = new SucursalSC();



            try
            {
                var dbSucursalSC = await _dbcontent.Sucursals.FirstOrDefaultAsync(X => X.IdSucursal == id);
                if (dbSucursalSC != null)
                {

                    SucursalSC.IdSucursal = dbSucursalSC.IdSucursal;
                    SucursalSC.Nombre = dbSucursalSC.Nombre;
                    SucursalSC.Direccion = dbSucursalSC.Direccion;
                    SucursalSC.IdEmpresa = dbSucursalSC.IdEmpresa;
                    SucursalSC.FechaCreacion = dbSucursalSC.FechaCreacion;
                    SucursalSC.UsuarioCreacion = dbSucursalSC.UsuarioCreacion;
                    SucursalSC.FechaModificacion =    dbSucursalSC.FechaModificacion;
                    SucursalSC.UsuarioModificacion = dbSucursalSC.UsuarioModificacion;


                    responseApi.EsCorrecto = true;
                    responseApi.Valor = SucursalSC;

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

        public async Task<IActionResult> Guardar(SucursalSC sucursal)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbSucursal = new Sucursal
                {

                    IdSucursal = sucursal.IdSucursal,
                    Nombre = sucursal.Nombre,
                    Direccion = sucursal.Direccion,
                    IdEmpresa = sucursal.IdEmpresa,
                    FechaCreacion = sucursal.FechaCreacion,
                    UsuarioCreacion = sucursal.UsuarioCreacion,
                    FechaModificacion = sucursal.FechaModificacion,
                    UsuarioModificacion = sucursal.UsuarioModificacion,
                };

                _dbcontent.Sucursals.Add(dbSucursal);
                await _dbcontent.SaveChangesAsync();

                if (dbSucursal.IdSucursal != 0)
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = dbSucursal.IdSucursal;
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

        public async Task<IActionResult> Editar(SucursalSC sucursal, int id)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbsucursal = await _dbcontent.Sucursals.FirstOrDefaultAsync(e => e.IdSucursal == id);


                if (dbsucursal != null)
                {

                    dbsucursal.IdSucursal = sucursal.IdSucursal;
                    dbsucursal.Nombre = sucursal.Nombre;
                    dbsucursal.Direccion = sucursal.Direccion;
                    dbsucursal.IdEmpresa = sucursal.IdEmpresa;
                    dbsucursal.FechaCreacion = sucursal.FechaCreacion;
                    dbsucursal.UsuarioCreacion = sucursal.UsuarioCreacion;
                    dbsucursal.FechaModificacion = sucursal.FechaModificacion;
                    dbsucursal.UsuarioModificacion = sucursal.UsuarioModificacion;


                    _dbcontent.Sucursals.Update(dbsucursal);
                    await _dbcontent.SaveChangesAsync();

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = dbsucursal.IdSucursal;


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
                var dbSucursal = await _dbcontent.Sucursals.FirstOrDefaultAsync(e => e.IdSucursal == id);


                if (dbSucursal != null)
                {

                    _dbcontent.Sucursals.Remove(dbSucursal);
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
