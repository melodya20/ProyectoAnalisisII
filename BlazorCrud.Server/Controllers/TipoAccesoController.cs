using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using BlazorCrud.Server.Models;
using BlazorCrud.Shared;
using Microsoft.EntityFrameworkCore;

namespace BlazorCrud.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoAccesoController : ControllerBase
    {
        private readonly ProyectoAnalisisContext _dbcontent;

        public TipoAccesoController(ProyectoAnalisisContext dbcontent)
        {
            _dbcontent = dbcontent;
        }

        [HttpGet]
        [Route("Lista")]

        public async Task<IActionResult> Lista()
        {
            var responseApi = new ResponseAPI<List<TipoAccesoTA>>();
            var listaTipoAccesoTA = new List<TipoAccesoTA>();
            try
            {
                foreach (var item in await _dbcontent.TipoAccesos.ToListAsync())
                {
                    listaTipoAccesoTA.Add(new TipoAccesoTA
                    {
                        IdTipoAcceso = item.IdTipoAcceso,
                        Nombre = item.Nombre,
                        FechaCreacion = item.FechaCreacion,
                        UsuarioCreacion = item.UsuarioCreacion,
                        FechaModificacion = item.FechaModificacion,
                        UsuarioModificacion = item.UsuarioModificacion,

                    });
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = listaTipoAccesoTA;
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
            var responseApi = new ResponseAPI<TipoAccesoTA>();
            var TipoAccesoTA = new TipoAccesoTA();
            try
            {
                var dbTipoAccesoTA = await _dbcontent.TipoAccesos.FirstOrDefaultAsync(X => X.IdTipoAcceso == id);
                if (dbTipoAccesoTA != null)
                {

                    TipoAccesoTA.IdTipoAcceso = dbTipoAccesoTA.IdTipoAcceso;
                    TipoAccesoTA.Nombre = dbTipoAccesoTA.Nombre;
                    TipoAccesoTA.FechaCreacion = dbTipoAccesoTA.FechaCreacion;
                    TipoAccesoTA.UsuarioCreacion = dbTipoAccesoTA.UsuarioCreacion;
                    TipoAccesoTA.FechaModificacion = dbTipoAccesoTA.FechaModificacion;
                    TipoAccesoTA.UsuarioModificacion = dbTipoAccesoTA.UsuarioModificacion;


                    responseApi.EsCorrecto = true;
                    responseApi.Valor = TipoAccesoTA;

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

        public async Task<IActionResult> Guardar(TipoAccesoTA tipo)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbTipoAccesoTA = new TipoAcceso
                {
                    IdTipoAcceso = tipo.IdTipoAcceso,
                    Nombre = tipo.Nombre,
                    FechaCreacion = tipo.FechaCreacion,
                    UsuarioCreacion = tipo.UsuarioCreacion,
                    FechaModificacion = tipo.FechaModificacion,
                    UsuarioModificacion = tipo.UsuarioModificacion,
                };

                _dbcontent.TipoAccesos.Add(dbTipoAccesoTA);
                await _dbcontent.SaveChangesAsync();

                if (dbTipoAccesoTA.IdTipoAcceso != 0)
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = dbTipoAccesoTA.IdTipoAcceso;
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

        public async Task<IActionResult> Editar(TipoAccesoTA tipo, int id)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbTipoAcceso = await _dbcontent.TipoAccesos.FirstOrDefaultAsync(e => e.IdTipoAcceso == id);


                if (dbTipoAcceso != null)
                {

                    dbTipoAcceso.Nombre = tipo.Nombre;
                    dbTipoAcceso.FechaCreacion = tipo.FechaCreacion;
                    dbTipoAcceso.UsuarioCreacion = tipo.UsuarioCreacion;
                    dbTipoAcceso.FechaModificacion = tipo.FechaModificacion;
                    dbTipoAcceso.UsuarioModificacion = tipo.UsuarioModificacion;


                    _dbcontent.TipoAccesos.Update(dbTipoAcceso);
                    await _dbcontent.SaveChangesAsync();

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = dbTipoAcceso.IdTipoAcceso;


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
                var dbTipoAcceso = await _dbcontent.TipoAccesos.FirstOrDefaultAsync(e => e.IdTipoAcceso == id);


                if (dbTipoAcceso != null)
                {



                    _dbcontent.TipoAccesos.Remove(dbTipoAcceso);
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
