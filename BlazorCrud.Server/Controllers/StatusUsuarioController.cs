using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using BlazorCrud.Server.Models;
using BlazorCrud.Shared;
using Microsoft.EntityFrameworkCore;
namespace BlazorCrud.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusUsuarioController : ControllerBase
    {
        private readonly ProyectoAnalisisContext _dbcontent;

        public StatusUsuarioController(ProyectoAnalisisContext dbcontent)
        {
            _dbcontent = dbcontent;
        }


        [HttpGet]
        [Route("Lista")]

        public async Task<IActionResult> Lista()
        {
            var responseApi = new ResponseAPI<List<StatusUsuarioST>>();
            var listaStatusUsuarioST = new List<StatusUsuarioST>();
            try
            {
                foreach (var item in await _dbcontent.StatusUsuarios.ToListAsync())
                {
                    listaStatusUsuarioST.Add(new StatusUsuarioST
                    {
                        IdStatusUsuario = item.IdStatusUsuario,
                        Nombre = item.Nombre,
                        FechaCreacion = item.FechaCreacion,
                        UsuarioCreacion = item.UsuarioCreacion,
                        FechaModificacion = item.FechaModificacion,
                        UsuarioModificacion = item.UsuarioModificacion,

                    });
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = listaStatusUsuarioST;
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
            var responseApi = new ResponseAPI<StatusUsuarioST>();
            var StatusUsuarioST = new StatusUsuarioST();



            try
            {
                var dbStatusUsuarioST = await _dbcontent.StatusUsuarios.FirstOrDefaultAsync(X => X.IdStatusUsuario == id);
                if (dbStatusUsuarioST != null)
                {

                    StatusUsuarioST.IdStatusUsuario = dbStatusUsuarioST.IdStatusUsuario;
                    StatusUsuarioST.Nombre = dbStatusUsuarioST.Nombre;
                    StatusUsuarioST.FechaCreacion = dbStatusUsuarioST.FechaCreacion;
                    StatusUsuarioST.UsuarioCreacion = dbStatusUsuarioST.UsuarioCreacion;
                    StatusUsuarioST.FechaModificacion = dbStatusUsuarioST.FechaModificacion;
                    StatusUsuarioST.UsuarioModificacion = dbStatusUsuarioST.UsuarioModificacion;



                    responseApi.EsCorrecto = true;
                    responseApi.Valor = StatusUsuarioST;

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

        public async Task<IActionResult> Guardar(StatusUsuarioST status)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbStatusUsuarioST = new StatusUsuario
                {
                    Nombre = status.Nombre,
                    FechaCreacion = status.FechaCreacion,
                    UsuarioCreacion = status.UsuarioCreacion,
                    FechaModificacion = status.FechaModificacion,
                    UsuarioModificacion = status.UsuarioModificacion,
                };

                _dbcontent.StatusUsuarios.Add(dbStatusUsuarioST);
                await _dbcontent.SaveChangesAsync();

                if (dbStatusUsuarioST.IdStatusUsuario != 0)
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = dbStatusUsuarioST.IdStatusUsuario;
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

        public async Task<IActionResult> Editar(StatusUsuarioST status, int id)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbStatusUsuario = await _dbcontent.StatusUsuarios.FirstOrDefaultAsync(e => e.IdStatusUsuario == id);


                if (dbStatusUsuario != null)
                {

                    dbStatusUsuario.Nombre = status.Nombre;
                    dbStatusUsuario.FechaCreacion = status.FechaCreacion;
                    dbStatusUsuario.UsuarioCreacion = status.UsuarioCreacion;
                    dbStatusUsuario.FechaModificacion = status.FechaModificacion;
                    dbStatusUsuario.UsuarioModificacion = status.UsuarioModificacion;


                    _dbcontent.StatusUsuarios.Update(dbStatusUsuario);
                    await _dbcontent.SaveChangesAsync();

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = dbStatusUsuario.IdStatusUsuario;


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
                var dbStatusUsuario = await _dbcontent.StatusUsuarios.FirstOrDefaultAsync(e => e.IdStatusUsuario == id);


                if (dbStatusUsuario != null)
                {



                    _dbcontent.StatusUsuarios.Remove(dbStatusUsuario);
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
