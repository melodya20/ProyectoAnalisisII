using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


using BlazorCrud.Server.Models;
using BlazorCrud.Shared;
using Microsoft.EntityFrameworkCore;


namespace BlazorCrud.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoMovCuentaCorrienteController : ControllerBase
    {
        private readonly ProyectoAnalisisContext _dbcontent;

        public TipoMovCuentaCorrienteController(ProyectoAnalisisContext dbcontent)
        {
            _dbcontent = dbcontent;
        }


        [HttpGet]
        [Route("Lista")]

        public async Task<IActionResult> Lista()
        {
            var responseApi = new ResponseAPI<List<TipoMovCuentaCorrienteCC>>();
            var listaTipoMovCuentaCorrienteCC = new List<TipoMovCuentaCorrienteCC>();
            try
            {
                foreach (var item in await _dbcontent.TipoMovimientoCxcs.ToListAsync())
                {
                    listaTipoMovCuentaCorrienteCC.Add(new TipoMovCuentaCorrienteCC
                    {
                        IdTipoMovimientoCXC = item.IdTipoMovimientoCxc,
                        OperacionCuentaCorriente = item.OperacionCuentaCorriente,
                        Nombre = item.Nombre,
                        FechaCreacion = item.FechaCreacion,
                        UsuarioCreacion = item.UsuarioCreacion,
                        FechaModificacion = item.FechaModificacion,
                        UsuarioModificacion = item.UsuarioModificacion,

                    });
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = listaTipoMovCuentaCorrienteCC;
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
            var responseApi = new ResponseAPI<TipoMovCuentaCorrienteCC>();
            var TipoMovCuentaCorrienteCC = new TipoMovCuentaCorrienteCC();



            try
            {
                var dbTipoMovCuentaCorrienteCC = await _dbcontent.TipoMovimientoCxcs.FirstOrDefaultAsync(X => X.IdTipoMovimientoCxc == id);
                if (dbTipoMovCuentaCorrienteCC != null)
                {

                    TipoMovCuentaCorrienteCC.IdTipoMovimientoCXC = dbTipoMovCuentaCorrienteCC.IdTipoMovimientoCxc;
                    TipoMovCuentaCorrienteCC.OperacionCuentaCorriente = dbTipoMovCuentaCorrienteCC.OperacionCuentaCorriente;
                    TipoMovCuentaCorrienteCC.Nombre = dbTipoMovCuentaCorrienteCC.Nombre;
                    TipoMovCuentaCorrienteCC.FechaCreacion = dbTipoMovCuentaCorrienteCC.FechaCreacion;
                    TipoMovCuentaCorrienteCC.UsuarioCreacion = dbTipoMovCuentaCorrienteCC.UsuarioCreacion;
                    TipoMovCuentaCorrienteCC.FechaModificacion = dbTipoMovCuentaCorrienteCC.FechaModificacion;
                    TipoMovCuentaCorrienteCC.UsuarioModificacion = dbTipoMovCuentaCorrienteCC.UsuarioModificacion;


                    responseApi.EsCorrecto = true;
                    responseApi.Valor = TipoMovCuentaCorrienteCC;

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

        public async Task<IActionResult> Guardar(TipoMovCuentaCorrienteCC tipomovcuentacorriente)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbTipoMovCuentaCorrienteCC = new TipoMovimientoCxc
                {
                    IdTipoMovimientoCxc = tipomovcuentacorriente.IdTipoMovimientoCXC,
                    OperacionCuentaCorriente = tipomovcuentacorriente.OperacionCuentaCorriente,
                    Nombre = tipomovcuentacorriente.Nombre,
                    FechaCreacion = tipomovcuentacorriente.FechaCreacion,
                    UsuarioCreacion = tipomovcuentacorriente.UsuarioCreacion,
                    FechaModificacion = tipomovcuentacorriente.FechaModificacion,
                    UsuarioModificacion = tipomovcuentacorriente.UsuarioModificacion,
                };

                _dbcontent.TipoMovimientoCxcs.Add(dbTipoMovCuentaCorrienteCC);
                await _dbcontent.SaveChangesAsync();

                if (dbTipoMovCuentaCorrienteCC.IdTipoMovimientoCxc != 0)
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = dbTipoMovCuentaCorrienteCC.IdTipoMovimientoCxc;
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

        public async Task<IActionResult> Editar(TipoMovCuentaCorrienteCC tipomovcuentacorriente, int id)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbTipoMovCuentaCorrienteCC = await _dbcontent.TipoMovimientoCxcs.FirstOrDefaultAsync(e => e.IdTipoMovimientoCxc == id);


                if (dbTipoMovCuentaCorrienteCC != null)
                {

                    dbTipoMovCuentaCorrienteCC.Nombre = tipomovcuentacorriente.Nombre;
                    dbTipoMovCuentaCorrienteCC.OperacionCuentaCorriente = tipomovcuentacorriente.OperacionCuentaCorriente;
                    dbTipoMovCuentaCorrienteCC.FechaCreacion = tipomovcuentacorriente.FechaCreacion;
                    dbTipoMovCuentaCorrienteCC.UsuarioCreacion = tipomovcuentacorriente.UsuarioCreacion;
                    dbTipoMovCuentaCorrienteCC.FechaModificacion = tipomovcuentacorriente.FechaModificacion;
                    dbTipoMovCuentaCorrienteCC.UsuarioModificacion = tipomovcuentacorriente.UsuarioModificacion;


                    _dbcontent.TipoMovimientoCxcs.Update(dbTipoMovCuentaCorrienteCC);
                    await _dbcontent.SaveChangesAsync();

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = dbTipoMovCuentaCorrienteCC.IdTipoMovimientoCxc;


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
                var dbTipoMovCuentaCorrienteCC = await _dbcontent.TipoMovimientoCxcs.FirstOrDefaultAsync(e => e.IdTipoMovimientoCxc == id);


                if (dbTipoMovCuentaCorrienteCC != null)
                {



                    _dbcontent.TipoMovimientoCxcs.Remove(dbTipoMovCuentaCorrienteCC);
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
