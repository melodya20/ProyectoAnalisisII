using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using BlazorCrud.Server.Models;
using BlazorCrud.Shared;
using Microsoft.EntityFrameworkCore;

namespace BlazorCrud.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OpcionController : ControllerBase
    {
        private readonly ProyectoAnalisisContext _dbcontent;

        public OpcionController(ProyectoAnalisisContext dbcontent)
        {
            _dbcontent = dbcontent;
        }


        [HttpGet]
        [Route("Lista")]

        public async Task<IActionResult> Lista()
        {
            var responseApi = new ResponseAPI<List<OpcionOP>>();
            var listaOpcionOP = new List<OpcionOP>();
            try
            {
                foreach (var item in await _dbcontent.Opcions.Include(d => d.IdMenuNavigation).ToListAsync())
                {
                    listaOpcionOP.Add(new OpcionOP
                    {
                        IdOpcion = item.IdOpcion,
                        IdMenu = item.IdMenu,
                        Menu = new MenuMN
                        {
                            IdMenu = item.IdMenuNavigation.IdMenu,
                            IdModulo = item.IdMenuNavigation.IdModulo,
                            Nombre = item.IdMenuNavigation.Nombre,
                            OrdenMenu = item.IdMenuNavigation.OrdenMenu,
                            FechaCreacion = item.IdMenuNavigation.FechaCreacion,
                            UsuarioCreacion = item.IdMenuNavigation.UsuarioCreacion,
                            FechaModificacion = item.IdMenuNavigation.FechaModificacion,
                            UsuarioModificacion = item.IdMenuNavigation.UsuarioModificacion,
                        },
                        Nombre = item.Nombre,
                        OrdenMenu = item.OrdenMenu,
                        Pagina = item.Pagina,   
                        FechaCreacion = item.FechaCreacion,
                        UsuarioCreacion = item.UsuarioCreacion,
                        FechaModificacion = item.FechaModificacion,
                        UsuarioModificacion = item.UsuarioModificacion,
                    });

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = listaOpcionOP;
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
            var responseApi = new ResponseAPI<OpcionOP>();
            var OpcionOPL = new OpcionOP();

            try
            {
                var dbOpcionOP = await _dbcontent.Opcions.FirstOrDefaultAsync(X => X.IdOpcion == id);
                if (dbOpcionOP != null)
                {
                    OpcionOPL.IdOpcion = dbOpcionOP.IdOpcion;
                    OpcionOPL.IdMenu = dbOpcionOP.IdMenu;
                    OpcionOPL.Nombre = dbOpcionOP.Nombre;
                    OpcionOPL.OrdenMenu = dbOpcionOP.OrdenMenu;
                    OpcionOPL.Pagina = dbOpcionOP.Pagina;
                    OpcionOPL.FechaCreacion = dbOpcionOP.FechaCreacion;
                    OpcionOPL.UsuarioCreacion = dbOpcionOP.UsuarioCreacion;
                    OpcionOPL.FechaModificacion = dbOpcionOP.FechaModificacion;
                    OpcionOPL.UsuarioModificacion = dbOpcionOP?.UsuarioModificacion;


                    responseApi.EsCorrecto = true;
                    responseApi.Valor = OpcionOPL;

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

        public async Task<IActionResult> Guardar(OpcionOP opcion)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbOpcion = new Opcion
                {
                    IdOpcion = opcion.IdOpcion,
                    IdMenu = opcion.IdMenu,
                    Nombre = opcion.Nombre,
                    OrdenMenu = opcion.OrdenMenu,
                    Pagina = opcion.Pagina,
                    FechaCreacion = opcion.FechaCreacion,
                    UsuarioCreacion = opcion.UsuarioCreacion,
                    FechaModificacion = opcion.FechaModificacion,
                    UsuarioModificacion = opcion.UsuarioModificacion,
                
                };

                _dbcontent.Opcions.Add(dbOpcion);
                await _dbcontent.SaveChangesAsync();

                if (dbOpcion.IdOpcion != 0)
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = dbOpcion.IdOpcion;
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

        public async Task<IActionResult> Editar(OpcionOP opcion, int id)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbOpcionOP = await _dbcontent.Opcions.FirstOrDefaultAsync(e => e.IdOpcion == id);


                if (dbOpcionOP != null)
                {
                    dbOpcionOP.IdOpcion = opcion.IdOpcion;
                    dbOpcionOP.IdMenu = opcion.IdMenu;
                    dbOpcionOP.Nombre = opcion.Nombre;
                    dbOpcionOP.OrdenMenu = opcion.OrdenMenu;
                    dbOpcionOP.Pagina = opcion.Pagina;
                    dbOpcionOP.FechaCreacion = opcion.FechaCreacion;
                    dbOpcionOP.UsuarioCreacion = opcion.UsuarioCreacion;
                    dbOpcionOP.FechaModificacion = opcion.FechaModificacion;
                    dbOpcionOP.UsuarioModificacion = opcion.UsuarioModificacion;


                    _dbcontent.Opcions.Update(dbOpcionOP);
                    await _dbcontent.SaveChangesAsync();

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = dbOpcionOP.IdOpcion;


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
                var dbOpcion = await _dbcontent.Opcions.FirstOrDefaultAsync(e => e.IdOpcion== id);


                if (dbOpcion != null)
                {

                    _dbcontent.Opcions.Remove(dbOpcion);
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
