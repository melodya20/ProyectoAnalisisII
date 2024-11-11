using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using BlazorCrud.Server.Models;
using BlazorCrud.Shared;
using Microsoft.EntityFrameworkCore;

namespace BlazorCrud.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly ProyectoAnalisisContext _dbcontent;

        public RoleController(ProyectoAnalisisContext dbcontent)
        {
            _dbcontent = dbcontent;
        }

        [HttpGet]
        [Route("Lista")]

        public async Task<IActionResult> Lista()
        {
            var responseApi = new ResponseAPI<List<RoleRL>>();
            var listaRoleRL = new List<RoleRL>();
            try
            {
                foreach (var item in await _dbcontent.Roles.ToListAsync())
                {
                    listaRoleRL.Add(new RoleRL
                    {

                        IdRole = item.IdRole,
                        Nombre = item.Nombre,
                        FechaCreacion = item.FechaCreacion,
                        UsuarioCreacion = item.UsuarioCreacion,
                        FechaModificacion = item.FechaModificacion, 
                        UsuarioModificacion = item.UsuarioModificacion,
                    });
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = listaRoleRL;
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
            var responseApi = new ResponseAPI<RoleRL>();
            var RoleRL = new RoleRL();

            try
            {
                var dbRoleRL = await _dbcontent.Roles.FirstOrDefaultAsync(X => X.IdRole == id);
                if (dbRoleRL != null)
                {
                    RoleRL.IdRole = dbRoleRL.IdRole;
                    RoleRL.Nombre = dbRoleRL.Nombre;
                    RoleRL.FechaCreacion = dbRoleRL.FechaCreacion;
                    RoleRL.UsuarioCreacion = dbRoleRL.UsuarioCreacion;
                    RoleRL.FechaModificacion = dbRoleRL.FechaModificacion;
                    RoleRL.UsuarioModificacion = dbRoleRL.UsuarioModificacion;

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = RoleRL;

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

        public async Task<IActionResult> Guardar(RoleRL role)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbrole = new Role
                {

                    IdRole = role.IdRole,
                    Nombre = role.Nombre,
                    FechaCreacion = role.FechaCreacion,
                    UsuarioCreacion = role.UsuarioCreacion,
                    FechaModificacion = role.FechaModificacion,
                    UsuarioModificacion = role.UsuarioModificacion,
                };

                _dbcontent.Roles.Add(dbrole);
                await _dbcontent.SaveChangesAsync();

                if (dbrole.IdRole != 0)
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = dbrole.IdRole;
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

        public async Task<IActionResult> Editar(RoleRL role, int id)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbRoleRL = await _dbcontent.Roles.FirstOrDefaultAsync(e => e.IdRole == id);


                if (dbRoleRL != null)
                {
                    dbRoleRL.IdRole = role.IdRole;
                    dbRoleRL.Nombre = role.Nombre;
                    dbRoleRL.FechaCreacion = role.FechaCreacion;
                    dbRoleRL.UsuarioCreacion = role.UsuarioCreacion;
                    dbRoleRL.FechaModificacion = role.FechaModificacion;
                    dbRoleRL.UsuarioModificacion = role.UsuarioModificacion;


                    _dbcontent.Roles.Update(dbRoleRL);
                    await _dbcontent.SaveChangesAsync();

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = dbRoleRL.IdRole;


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
                var dbRole = await _dbcontent.Roles.FirstOrDefaultAsync(e => e.IdRole == id);


                if (dbRole != null)
                {

                    _dbcontent.Roles.Remove(dbRole);
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
