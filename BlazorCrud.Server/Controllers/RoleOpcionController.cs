using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using BlazorCrud.Server.Models;
using BlazorCrud.Shared;
using Microsoft.EntityFrameworkCore;

namespace BlazorCrud.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleOpcionController : ControllerBase
    {
        private readonly ProyectoAnalisisContext _dbcontext;

        public RoleOpcionController(ProyectoAnalisisContext dbcontext)
        {
            _dbcontext = dbcontext;
        }


        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var responseApi = new ResponseAPI<List<RoleOP>>();
            var listaRoleOpciones = new List<RoleOP>();
            try
            {
                var opciones = await _dbcontext.RoleOpcions
                    .Include(d => d.IdRoleNavigation)
                    .Include(d => d.IdOpcionNavigation)
                    .ToListAsync();

                foreach (var item in opciones)
                {
                    listaRoleOpciones.Add(new RoleOP
                    {
                        IdRole = item.IdRole,
                        IdOpcion = item.IdOpcion,
                        Alta = item.Alta,
                        Baja = item.Baja,
                        Cambio = item.Cambio,
                        Imprimir = item.Imprimir,
                        Exportar = item.Exportar,
                        FechaCreacion = item.FechaCreacion,
                        UsuarioCreacion = item.UsuarioCreacion,
                        FechaModificacion = item.FechaModificacion,
                        UsuarioModificacion = item.UsuarioModificacion
                    });
                }

                responseApi.EsCorrecto = true;
                responseApi.Valor = listaRoleOpciones;
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensajes = ex.Message;
            }

            return Ok(responseApi);
        }


        [HttpGet]
        [Route("Buscar/{idRole}/{idOpcion}")]
        public async Task<IActionResult> Buscar(int idRole, int idOpcion)
        {
            var responseApi = new ResponseAPI<RoleOP>();

            try
            {
                var dbOpcion = await _dbcontext.RoleOpcions.FirstOrDefaultAsync(x => x.IdRole == idRole && x.IdOpcion == idOpcion);
                if (dbOpcion != null)
                {
                    var roleOpcion = new RoleOP
                    {
                        IdRole = dbOpcion.IdRole,
                        IdOpcion = dbOpcion.IdOpcion,
                        Alta = dbOpcion.Alta,
                        Baja = dbOpcion.Baja,
                        Cambio = dbOpcion.Cambio,
                        Imprimir = dbOpcion.Imprimir,
                        Exportar = dbOpcion.Exportar,
                        FechaCreacion = dbOpcion.FechaCreacion,
                        UsuarioCreacion = dbOpcion.UsuarioCreacion,
                        FechaModificacion = dbOpcion.FechaModificacion,
                        UsuarioModificacion = dbOpcion.UsuarioModificacion
                    };

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = roleOpcion;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensajes = "Role-Opción no encontrado";
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
        public async Task<IActionResult> Guardar(RoleOP roleOpcion)
        {
            var responseApi = new ResponseAPI<bool>();

            try
            {
                var dbOpcion = new RoleOpcion
                {
                    IdRole = roleOpcion.IdRole,
                    IdOpcion = roleOpcion.IdOpcion,
                    Alta = roleOpcion.Alta,
                    Baja = roleOpcion.Baja,
                    Cambio = roleOpcion.Cambio,
                    Imprimir = roleOpcion.Imprimir,
                    Exportar = roleOpcion.Exportar,
                    FechaCreacion = roleOpcion.FechaCreacion,
                    UsuarioCreacion = roleOpcion.UsuarioCreacion,
                    FechaModificacion = roleOpcion.FechaModificacion,
                    UsuarioModificacion = roleOpcion.UsuarioModificacion
                };

                _dbcontext.RoleOpcions.Add(dbOpcion);
                await _dbcontext.SaveChangesAsync();

                responseApi.EsCorrecto = true;
                responseApi.Valor = true;
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensajes = ex.Message;
            }

            return Ok(responseApi);
        }



        [HttpPut]
        [Route("Editar/{idRole}/{idOpcion}")]
        public async Task<IActionResult> Editar(RoleOP roleOpcion, int idRole, int idOpcion)
        {
            var responseApi = new ResponseAPI<bool>();

            try
            {
                var dbOpcion = await _dbcontext.RoleOpcions.FirstOrDefaultAsync(e => e.IdRole == idRole && e.IdOpcion == idOpcion);

                if (dbOpcion != null)
                {
                    dbOpcion.IdRole = roleOpcion.IdRole;
                    dbOpcion.IdOpcion = roleOpcion.IdOpcion;
                    dbOpcion.Alta = roleOpcion.Alta;
                    dbOpcion.Baja = roleOpcion.Baja;
                    dbOpcion.Cambio = roleOpcion.Cambio;
                    dbOpcion.Imprimir = roleOpcion.Imprimir;
                    dbOpcion.Exportar = roleOpcion.Exportar;
                    dbOpcion.FechaCreacion = roleOpcion.FechaCreacion;
                    dbOpcion.UsuarioCreacion = roleOpcion.UsuarioCreacion;
                    dbOpcion.FechaModificacion = roleOpcion.FechaModificacion;
                    dbOpcion.UsuarioModificacion = roleOpcion.UsuarioModificacion;

                    _dbcontext.RoleOpcions.Update(dbOpcion);
                    await _dbcontext.SaveChangesAsync();

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = true;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensajes = "Role-Opción no encontrado";
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
        [Route("Eliminar/{idRole}/{idOpcion}")]
        public async Task<IActionResult> Eliminar(int idRole, int idOpcion)
        {
            var responseApi = new ResponseAPI<bool>();

            try
            {
                var dbOpcion = await _dbcontext.RoleOpcions.FirstOrDefaultAsync(e => e.IdRole == idRole && e.IdOpcion == idOpcion);

                if (dbOpcion != null)
                {
                    _dbcontext.RoleOpcions.Remove(dbOpcion);
                    await _dbcontext.SaveChangesAsync();

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = true;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensajes = "Role-Opción no encontrado";
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
