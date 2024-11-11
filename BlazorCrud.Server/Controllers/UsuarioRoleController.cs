using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BlazorCrud.Server.Models;
using BlazorCrud.Shared;
using Microsoft.EntityFrameworkCore;
using System.Transactions;

namespace BlazorCrud.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioRoleController : ControllerBase
    {
        private readonly ProyectoAnalisisContext _dbcontext;

        public UsuarioRoleController(ProyectoAnalisisContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var responseApi = new ResponseAPI<List<UsuarioRoleURL>>();
            var listaUsuarioRoles = new List<UsuarioRoleURL>();
            try
            {
                var roles = await _dbcontext.UsuarioRoles
                    .Include(d => d.IdUsuarioNavigation)
                    .Include(d => d.IdRoleNavigation)
                    .ToListAsync();

                foreach (var item in roles)
                {
                    listaUsuarioRoles.Add(new UsuarioRoleURL
                    {
                        IdUsuario = item.IdUsuario,
                        IdRole = item.IdRole,
                        FechaCreacion = item.FechaCreacion,
                        UsuarioCreacion = item.UsuarioCreacion,
                        FechaModificacion = item.FechaModificacion,
                        UsuarioModificacion = item.UsuarioModificacion
                    });
                }

                responseApi.EsCorrecto = true;
                responseApi.Valor = listaUsuarioRoles;
            }
            catch (Exception ex)
            {
                // Log exception here
                responseApi.EsCorrecto = false;
                responseApi.Mensajes = ex.Message;
            }

            return Ok(responseApi);
        }

        [HttpGet]
        [Route("Buscar/{idUsuario}/{idRole}")]
        public async Task<IActionResult> Buscar(string idUsuario, int idRole)
        {
            var dbRole = await _dbcontext.UsuarioRoles.FirstOrDefaultAsync(x => x.IdUsuario == idUsuario && x.IdRole == idRole);
            if (dbRole == null)
            {
                return NotFound(new { Mensaje = $"Usuario-Role con IdUsuario = {idUsuario} e IdRole = {idRole} no encontrado." });
            }

            var usuarioRole = new UsuarioRoleURL
            {
                IdUsuario = dbRole.IdUsuario,
                IdRole = dbRole.IdRole,
                FechaCreacion = dbRole.FechaCreacion,
                UsuarioCreacion = dbRole.UsuarioCreacion,
                FechaModificacion = dbRole.FechaModificacion,
                UsuarioModificacion = dbRole.UsuarioModificacion
            };

            return Ok(new ResponseAPI<UsuarioRoleURL> { EsCorrecto = true, Valor = usuarioRole });
        }

        [HttpPost]
        [Route("Guardar")]
        public async Task<IActionResult> Guardar(UsuarioRoleURL usuarioRole)
        {
            var responseApi = new ResponseAPI<bool>();

            try
            {
                // Check if the combination of IdUsuario and IdRole already exists
                var exists = await _dbcontext.UsuarioRoles.AnyAsync(e => e.IdUsuario == usuarioRole.IdUsuario && e.IdRole == usuarioRole.IdRole);
                if (exists)
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensajes = "La combinación de IdUsuario e IdRole ya existe.";
                    return BadRequest(responseApi);
                }

                var dbRole = new UsuarioRole
                {
                    IdUsuario = usuarioRole.IdUsuario,
                    IdRole = usuarioRole.IdRole,
                    FechaCreacion = usuarioRole.FechaCreacion,
                    UsuarioCreacion = usuarioRole.UsuarioCreacion,
                    FechaModificacion = usuarioRole.FechaModificacion,
                    UsuarioModificacion = usuarioRole.UsuarioModificacion
                };

                _dbcontext.UsuarioRoles.Add(dbRole);
                await _dbcontext.SaveChangesAsync();

                responseApi.EsCorrecto = true;
                responseApi.Valor = true;
            }
            catch (Exception ex)
            {
                // Log exception here
                responseApi.EsCorrecto = false;
                responseApi.Mensajes = ex.Message;
            }

            return Ok(responseApi);
        }

        [HttpPut]
        [Route("Editar/{idUsuario}/{idRole}")]
        public async Task<IActionResult> Editar([FromBody] UsuarioRoleURL usuarioRole, string idUsuario, int idRole)
        {
            var responseApi = new ResponseAPI<bool>();

            try
            {
                // Busca el rol de usuario en la base de datos
                var dbRole = await _dbcontext.UsuarioRoles
                    .FirstOrDefaultAsync(e => e.IdUsuario == idUsuario && e.IdRole == idRole);

                if (dbRole != null)
                {
                    // Actualiza las propiedades del rol de usuario
                    dbRole.IdUsuario = usuarioRole.IdUsuario;
                    dbRole.IdRole = usuarioRole.IdRole;
                    dbRole.FechaCreacion = usuarioRole.FechaCreacion;
                    dbRole.UsuarioCreacion = usuarioRole.UsuarioCreacion;
                    dbRole.FechaModificacion = usuarioRole.FechaModificacion;
                    dbRole.UsuarioModificacion = usuarioRole.UsuarioModificacion;

                    // Marca el rol de usuario como modificado en el contexto
                    _dbcontext.UsuarioRoles.Update(dbRole);
                    await _dbcontext.SaveChangesAsync();

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = true;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensajes = "Usuario-Role no encontrado";
                }
            }
            catch (Exception ex)
            {
                // Log exception here
                responseApi.EsCorrecto = false;
                responseApi.Mensajes = ex.Message;
            }

            return Ok(responseApi);
        }


        [HttpDelete]
        [Route("Eliminar/{idUsuario}/{idRole}")]
        public async Task<IActionResult> Eliminar(string idUsuario, int idRole)
        {
            var dbRole = await _dbcontext.UsuarioRoles.FirstOrDefaultAsync(e => e.IdUsuario == idUsuario && e.IdRole == idRole);
            if (dbRole == null)
            {
                return NotFound(new { Mensaje = $"Usuario-Role con IdUsuario = {idUsuario} e IdRole = {idRole} no encontrado." });
            }

            try
            {
                _dbcontext.UsuarioRoles.Remove(dbRole);
                await _dbcontext.SaveChangesAsync();

                return Ok(new ResponseAPI<bool> { EsCorrecto = true, Valor = true });
            }
            catch (Exception ex)
            {
                // Log exception here
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseAPI<bool> { EsCorrecto = false, Mensajes = ex.Message });
            }
        }
    }
}
