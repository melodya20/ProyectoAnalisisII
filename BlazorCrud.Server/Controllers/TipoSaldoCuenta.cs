using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


using BlazorCrud.Server.Models;
using BlazorCrud.Shared;
using Microsoft.EntityFrameworkCore;


namespace BlazorCrud.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoSaldoCuentaController : ControllerBase
    {
        private readonly ProyectoAnalisisContext _dbcontent;

        public TipoSaldoCuentaController(ProyectoAnalisisContext dbcontent)
        {
            _dbcontent = dbcontent;
        }


        [HttpGet]
        [Route("Lista")]

        public async Task<IActionResult> Lista()
        {
            var responseApi = new ResponseAPI<List<TipoSaldoCuentaTSC>>();
            var listaTipoSaldoCuentaTSC = new List<TipoSaldoCuentaTSC>();
            try
            {
                foreach (var item in await _dbcontent.TipoSaldoCuenta.ToListAsync())
                {
                    listaTipoSaldoCuentaTSC.Add(new TipoSaldoCuentaTSC
                    {
                        IdTipoSaldoCuenta = item.IdTipoSaldoCuenta,
                        Nombre = item.Nombre,
                        FechaCreacion = item.FechaCreacion,
                        UsuarioCreacion = item.UsuarioCreacion,
                        FechaModificacion = item.FechaModificacion,
                        UsuarioModificacion = item.UsuarioModificacion,

                    });
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = listaTipoSaldoCuentaTSC;
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
            var responseApi = new ResponseAPI<TipoSaldoCuentaTSC>();
            var TipoSaldoCuentaTSC = new TipoSaldoCuentaTSC();



            try
            {
                var dbTipoSaldoCuentaTSC = await _dbcontent.TipoSaldoCuenta.FirstOrDefaultAsync(X => X.IdTipoSaldoCuenta == id);
                if (dbTipoSaldoCuentaTSC != null)
                {

                    TipoSaldoCuentaTSC.IdTipoSaldoCuenta = dbTipoSaldoCuentaTSC.IdTipoSaldoCuenta;
                    TipoSaldoCuentaTSC.Nombre = dbTipoSaldoCuentaTSC.Nombre;
                    TipoSaldoCuentaTSC.FechaCreacion = dbTipoSaldoCuentaTSC.FechaCreacion;
                    TipoSaldoCuentaTSC.UsuarioCreacion = dbTipoSaldoCuentaTSC.UsuarioCreacion;
                    TipoSaldoCuentaTSC.FechaModificacion = dbTipoSaldoCuentaTSC.FechaModificacion;
                    TipoSaldoCuentaTSC.UsuarioModificacion = dbTipoSaldoCuentaTSC.UsuarioModificacion;


                    responseApi.EsCorrecto = true;
                    responseApi.Valor = TipoSaldoCuentaTSC;

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

        public async Task<IActionResult> Guardar(TipoSaldoCuentaTSC tscuenta)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbTipoSaldoCuenta = new TipoSaldoCuentum
                {
                    IdTipoSaldoCuenta = tscuenta.IdTipoSaldoCuenta,
                    Nombre = tscuenta.Nombre,
                    FechaCreacion = tscuenta.FechaCreacion,
                    UsuarioCreacion = tscuenta.UsuarioCreacion,
                    FechaModificacion = tscuenta.FechaModificacion,
                    UsuarioModificacion = tscuenta.UsuarioModificacion,
                };

                _dbcontent.TipoSaldoCuenta.Add(dbTipoSaldoCuenta);
                await _dbcontent.SaveChangesAsync();

                if (dbTipoSaldoCuenta.IdTipoSaldoCuenta != 0)
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = dbTipoSaldoCuenta.IdTipoSaldoCuenta;
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

        public async Task<IActionResult> Editar(TipoSaldoCuentaTSC tscuenta, int id)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbTipoSaldoCuenta = await _dbcontent.TipoSaldoCuenta.FirstOrDefaultAsync(e => e.IdTipoSaldoCuenta == id);


                if (dbTipoSaldoCuenta != null)
                {

                    dbTipoSaldoCuenta.Nombre = tscuenta.Nombre;
                    dbTipoSaldoCuenta.FechaCreacion = tscuenta.FechaCreacion;
                    dbTipoSaldoCuenta.UsuarioCreacion = tscuenta.UsuarioCreacion;
                    dbTipoSaldoCuenta.FechaModificacion = tscuenta.FechaModificacion;
                    dbTipoSaldoCuenta.UsuarioModificacion = tscuenta.UsuarioModificacion;


                    _dbcontent.TipoSaldoCuenta.Update(dbTipoSaldoCuenta);
                    await _dbcontent.SaveChangesAsync();

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = dbTipoSaldoCuenta.IdTipoSaldoCuenta;


                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensajes = "No  encontrado";
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
                var dbTipoSaldoCuenta = await _dbcontent.TipoSaldoCuenta.FirstOrDefaultAsync(e => e.IdTipoSaldoCuenta == id);


                if (dbTipoSaldoCuenta != null)
                {



                    _dbcontent.TipoSaldoCuenta.Remove(dbTipoSaldoCuenta);
                    await _dbcontent.SaveChangesAsync();

                    responseApi.EsCorrecto = true;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensajes = "No  encontrado";
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
