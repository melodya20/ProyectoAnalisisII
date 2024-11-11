using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using BlazorCrud.Server.Models;
using BlazorCrud.Shared;
using Microsoft.EntityFrameworkCore;


namespace BlazorCrud.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModuloController : ControllerBase
    {
        private readonly ProyectoAnalisisContext _dbcontent;

        public ModuloController(ProyectoAnalisisContext dbcontent)
        {
            _dbcontent = dbcontent;
        }


        [HttpGet]
        [Route("Lista")]

        public async Task<IActionResult> Lista()
        {
            var responseApi = new ResponseAPI<List<ModuloMD>>();
            var listaModuloMD = new List<ModuloMD>();
            try
            {
                foreach (var item in await _dbcontent.Modulos.ToListAsync())
                {
                    listaModuloMD.Add(new ModuloMD
                    {
                        IdModulo = item.IdModulo,
                        Nombre = item.Nombre,
                        OrdenMenu = item.OrdenMenu,
                        FechaCreacion = item.FechaCreacion,
                        UsuarioCreacion = item.UsuarioCreacion,
                        FechaModificacion = item.FechaModificacion,
                        UsuarioModificacion = item.UsuarioModificacion,

                    });
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = listaModuloMD;
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
            var responseApi = new ResponseAPI<ModuloMD>();
            var ModuloMD = new ModuloMD();

            try
            {
                var dbModuloMD = await _dbcontent.Modulos.FirstOrDefaultAsync(X => X.IdModulo == id);
                if (dbModuloMD != null)
                {
                    ModuloMD.IdModulo = dbModuloMD.IdModulo;
                    ModuloMD.Nombre = dbModuloMD.Nombre;
                    ModuloMD.OrdenMenu = dbModuloMD.OrdenMenu;
                    ModuloMD.FechaCreacion = dbModuloMD.FechaCreacion;
                    ModuloMD.UsuarioCreacion = dbModuloMD.UsuarioCreacion;
                    ModuloMD.FechaModificacion = dbModuloMD.FechaModificacion;
                    ModuloMD.UsuarioModificacion = dbModuloMD.UsuarioModificacion;


                    responseApi.EsCorrecto = true;
                    responseApi.Valor = ModuloMD;

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

        public async Task<IActionResult> Guardar(ModuloMD modulo)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbModulo = new Modulo
                {

                    Nombre = modulo.Nombre,
                    OrdenMenu = modulo.OrdenMenu,
                    FechaCreacion = modulo.FechaCreacion,
                    UsuarioCreacion = modulo.UsuarioCreacion,
                    FechaModificacion  = modulo.FechaModificacion,
                    UsuarioModificacion = modulo.UsuarioModificacion,

                };

                _dbcontent.Modulos.Add(dbModulo);
                await _dbcontent.SaveChangesAsync();

                if (dbModulo.IdModulo != 0)
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = dbModulo.IdModulo;
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

        public async Task<IActionResult> Editar(ModuloMD modulo, int id)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbModulo = await _dbcontent.Modulos.FirstOrDefaultAsync(e => e.IdModulo == id);


                if (dbModulo != null)
                {

                    dbModulo.Nombre = modulo.Nombre;
                    dbModulo.OrdenMenu = modulo.OrdenMenu;
                    dbModulo.FechaCreacion = modulo.FechaCreacion;
                    dbModulo.UsuarioCreacion = modulo.UsuarioCreacion;
                    dbModulo.FechaModificacion = modulo.FechaModificacion;
                    dbModulo.UsuarioModificacion = modulo.UsuarioModificacion;



                    _dbcontent.Modulos.Update(dbModulo);
                    await _dbcontent.SaveChangesAsync();

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = dbModulo.IdModulo;


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
                var dbModulo = await _dbcontent.Modulos.FirstOrDefaultAsync(e => e.IdModulo == id);


                if (dbModulo != null)
                {



                    _dbcontent.Modulos.Remove(dbModulo);
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
