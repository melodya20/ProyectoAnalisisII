using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


using BlazorCrud.Server.Models;
using BlazorCrud.Shared;
using Microsoft.EntityFrameworkCore;


namespace BlazorCrud.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoDocumentoController : ControllerBase
    {
        private readonly ProyectoAnalisisContext _dbcontent;

        public TipoDocumentoController(ProyectoAnalisisContext dbcontent)
        {
            _dbcontent = dbcontent;
        }


        [HttpGet]
        [Route("Lista")]

        public async Task<IActionResult> Lista()
        {
            var responseApi = new ResponseAPI<List<TipoDocumentoTD>>();
            var listaTipoDocumentoTD = new List<TipoDocumentoTD>();
            try
            {
                foreach (var item in await _dbcontent.TipoDocumentos.ToListAsync())
                {
                    listaTipoDocumentoTD.Add(new TipoDocumentoTD
                    {
                        IdTipoDocumento = item.IdTipoDocumento,
                        Nombre = item.Nombre,
                        FechaCreacion = item.FechaCreacion,
                        UsuarioCreacion = item.UsuarioCreacion,
                        FechaModificacion = item.FechaModificacion,
                        UsuarioModificacion = item.UsuarioModificacion,

                    });
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = listaTipoDocumentoTD;
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
            var responseApi = new ResponseAPI<TipoDocumentoTD>();
            var TipoDocumentoTD = new TipoDocumentoTD();



            try
            {
                var dbTipoDocumentoTD = await _dbcontent.TipoDocumentos.FirstOrDefaultAsync(X => X.IdTipoDocumento == id);
                if (dbTipoDocumentoTD != null)
                {

                    TipoDocumentoTD.IdTipoDocumento = dbTipoDocumentoTD.IdTipoDocumento;
                    TipoDocumentoTD.Nombre = dbTipoDocumentoTD.Nombre;
                    TipoDocumentoTD.FechaCreacion = dbTipoDocumentoTD.FechaCreacion;
                    TipoDocumentoTD.UsuarioCreacion = dbTipoDocumentoTD.UsuarioCreacion;
                    TipoDocumentoTD.FechaModificacion = dbTipoDocumentoTD.FechaModificacion;
                    TipoDocumentoTD.UsuarioModificacion = dbTipoDocumentoTD.UsuarioModificacion;


                    responseApi.EsCorrecto = true;
                    responseApi.Valor = TipoDocumentoTD;

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

        public async Task<IActionResult> Guardar(TipoDocumentoTD tipodocumento)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbTipoDocumentoTD = new TipoDocumento
                {
                    IdTipoDocumento = tipodocumento.IdTipoDocumento,
                    Nombre = tipodocumento.Nombre,
                    FechaCreacion = tipodocumento.FechaCreacion,
                    UsuarioCreacion = tipodocumento.UsuarioCreacion,
                    FechaModificacion = tipodocumento.FechaModificacion,
                    UsuarioModificacion = tipodocumento.UsuarioModificacion,
                };

                _dbcontent.TipoDocumentos.Add(dbTipoDocumentoTD);
                await _dbcontent.SaveChangesAsync();

                if (dbTipoDocumentoTD.IdTipoDocumento != 0)
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = dbTipoDocumentoTD.IdTipoDocumento;
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

        public async Task<IActionResult> Editar(TipoDocumentoTD tipodocumento, int id)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbTipoDocumentosTD = await _dbcontent.TipoDocumentos.FirstOrDefaultAsync(e => e.IdTipoDocumento == id);


                if (dbTipoDocumentosTD != null)
                {

                    dbTipoDocumentosTD.Nombre = tipodocumento.Nombre;
                    dbTipoDocumentosTD.FechaCreacion = tipodocumento.FechaCreacion;
                    dbTipoDocumentosTD.UsuarioCreacion = tipodocumento.UsuarioCreacion;
                    dbTipoDocumentosTD.FechaModificacion = tipodocumento.FechaModificacion;
                    dbTipoDocumentosTD.UsuarioModificacion = tipodocumento.UsuarioModificacion;


                    _dbcontent.TipoDocumentos.Update(dbTipoDocumentosTD);
                    await _dbcontent.SaveChangesAsync();

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = dbTipoDocumentosTD.IdTipoDocumento;


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
                var dbTipoDocumentosTD = await _dbcontent.TipoDocumentos.FirstOrDefaultAsync(e => e.IdTipoDocumento == id);


                if (dbTipoDocumentosTD != null)
                {



                    _dbcontent.TipoDocumentos.Remove(dbTipoDocumentosTD);
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
