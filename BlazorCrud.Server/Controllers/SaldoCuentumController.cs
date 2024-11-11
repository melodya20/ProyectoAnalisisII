using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


using BlazorCrud.Server.Models;
using BlazorCrud.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using BlazorCrud.Client.Services;

namespace BlazorCrud.Server.Controllers
{
    [Route("api/SaldoCuentum")]
    [ApiController]

    public class SaldoCuentumController : ControllerBase
    {
        private readonly ProyectoAnalisisContext _dbcontent;

        public SaldoCuentumController(ProyectoAnalisisContext dbcontent)
        {
            _dbcontent = dbcontent;
        }



        [HttpGet]
        [Route("Lista")]

        public async Task<IActionResult> Lista()
        {
            var responseApi = new ResponseAPI<List<SaldoCuentumSC>>();
            var listaSaldoCuemtum = new List<SaldoCuentumSC>();
            try
            {
                var vsaldo = await _dbcontent.SaldoCuenta
                    .Include(d => d.IdPersonaNavigation)
                    .Include(d => d.IdStatusCuentaNavigation)
                    .Include(d => d.IdTipoSaldoCuentaNavigation)
                    .ToListAsync();

                foreach (var item in await _dbcontent.SaldoCuenta.ToListAsync())
                {
                    listaSaldoCuemtum.Add(new SaldoCuentumSC
                    {
                        IdSaldoCuenta = item.IdSaldoCuenta,
                        IdPersona = item.IdPersona,

                        Persona = item.IdPersonaNavigation != null ? new PersonaP
                        {
                            IdPersona = item.IdPersonaNavigation.IdPersona,
                            Nombre = item.IdPersonaNavigation.Nombre,
                        } : null,

                        IdStatusCuenta = item.IdStatusCuenta,

                        StatusCuentum = item.IdStatusCuentaNavigation != null ? new StatusCuentumST
                        {
                            IdStatusCuentum = item.IdStatusCuentaNavigation.IdStatusCuenta,
                            Nombre = item.IdStatusCuentaNavigation.Nombre,
                        } : null,

                        IdTipoSaldoCuenta = item.IdSaldoCuenta,

                        TipoSaldoCuenta = item.IdTipoSaldoCuentaNavigation != null ? new TipoSaldoCuentaTSC
                        {
                            IdTipoSaldoCuenta = item.IdTipoSaldoCuentaNavigation.IdTipoSaldoCuenta,
                            Nombre = item.IdTipoSaldoCuentaNavigation.Nombre,
                        } : null,

                        SaldoAnterior = item.SaldoAnterior,
                        Debitos = item.Debitos,
                        Creditos = item.Creditos,
                        FechaCreacion = item.FechaCreacion,
                        UsuarioCreacion = item.UsuarioCreacion,
                        FechaModificacion = item.FechaModificacion,
                        UsuarioModificacion = item.UsuarioModificacion,

                    });
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = listaSaldoCuemtum;
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
            var responseApi = new ResponseAPI<SaldoCuentumSC>();
            var SaldoCuentumSC = new SaldoCuentumSC();

            try
            {
                var dbSaldoCuentum = await _dbcontent.SaldoCuenta.FirstOrDefaultAsync(X => X.IdSaldoCuenta == id);
                if (dbSaldoCuentum != null)
                {
                    SaldoCuentumSC.IdSaldoCuenta = dbSaldoCuentum.IdSaldoCuenta;
                    SaldoCuentumSC.IdPersona = dbSaldoCuentum.IdPersona;
                    SaldoCuentumSC.IdStatusCuenta = dbSaldoCuentum.IdStatusCuenta;
                    SaldoCuentumSC.IdTipoSaldoCuenta = dbSaldoCuentum.IdTipoSaldoCuenta;
                    SaldoCuentumSC.SaldoAnterior = dbSaldoCuentum.SaldoAnterior;
                    SaldoCuentumSC.Debitos = dbSaldoCuentum.Debitos;
                    SaldoCuentumSC.Creditos = dbSaldoCuentum.Creditos;
                    SaldoCuentumSC.FechaCreacion = dbSaldoCuentum.FechaCreacion;
                    SaldoCuentumSC.UsuarioCreacion = dbSaldoCuentum.UsuarioCreacion;
                    SaldoCuentumSC.FechaModificacion = dbSaldoCuentum.FechaModificacion;
                    SaldoCuentumSC.UsuarioModificacion = dbSaldoCuentum.UsuarioModificacion;

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = SaldoCuentumSC;
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

        public async Task<IActionResult> Guardar(SaldoCuentumSC saldo)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbSaldo = new SaldoCuentum
                {
 
                    IdSaldoCuenta = saldo.IdSaldoCuenta,
                    IdPersona = saldo.IdPersona,
                    IdStatusCuenta = saldo.IdStatusCuenta,
                    IdTipoSaldoCuenta = saldo.IdSaldoCuenta,
                    SaldoAnterior = saldo.SaldoAnterior,
                    Debitos = saldo.Debitos,
                    Creditos = saldo.Creditos,
                    FechaCreacion = DateTime.Now,
                    UsuarioCreacion = User.Identity.Name,
                    FechaModificacion = DateTime.Now,
                    UsuarioModificacion = User.Identity.Name,
                };

                _dbcontent.SaldoCuenta.Add(dbSaldo);
                await _dbcontent.SaveChangesAsync();

                if (dbSaldo.IdSaldoCuenta != 0)
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = dbSaldo.IdSaldoCuenta;
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

        [HttpGet]
        [Route("Persona/{idPersona}")]
        public async Task<IActionResult> ObtenerPorPersona(int idPersona)
        {
            var responseApi = new ResponseAPI<List<SaldoCuentumSC>>();
            var listaSaldoCuentum = new List<SaldoCuentumSC>();

            try
            {
                // Realiza la consulta para obtener los saldos de la persona especificada
                var saldos = await _dbcontent.SaldoCuenta
                    .Where(s => s.IdPersona == idPersona)
                    .Include(s => s.IdPersonaNavigation)
                    .Include(s => s.IdStatusCuentaNavigation)
                    .Include(s => s.IdTipoSaldoCuentaNavigation)
                    .ToListAsync();

                // Mapea los resultados a la clase SaldoCuentumSC
                foreach (var item in saldos)
                {
                    listaSaldoCuentum.Add(new SaldoCuentumSC
                    {
                        IdSaldoCuenta = item.IdSaldoCuenta,
                        IdPersona = item.IdPersona,

                        Persona = item.IdPersonaNavigation != null ? new PersonaP
                        {
                            IdPersona = item.IdPersonaNavigation.IdPersona,
                            Nombre = item.IdPersonaNavigation.Nombre,
                        } : null,

                        IdStatusCuenta = item.IdStatusCuenta,
                        StatusCuentum = item.IdStatusCuentaNavigation != null ? new StatusCuentumST
                        {
                            IdStatusCuentum = item.IdStatusCuentaNavigation.IdStatusCuenta,
                            Nombre = item.IdStatusCuentaNavigation.Nombre,
                        } : null,

                        IdTipoSaldoCuenta = item.IdTipoSaldoCuenta,
                        TipoSaldoCuenta = item.IdTipoSaldoCuentaNavigation != null ? new TipoSaldoCuentaTSC
                        {
                            IdTipoSaldoCuenta = item.IdTipoSaldoCuentaNavigation.IdTipoSaldoCuenta,
                            Nombre = item.IdTipoSaldoCuentaNavigation.Nombre,
                        } : null,

                        SaldoAnterior = item.SaldoAnterior,
                        Debitos = item.Debitos,
                        Creditos = item.Creditos,
                        FechaCreacion = item.FechaCreacion,
                        UsuarioCreacion = item.UsuarioCreacion,
                        FechaModificacion = item.FechaModificacion,
                        UsuarioModificacion = item.UsuarioModificacion,
                    });
                }

                // Configura la respuesta en caso de éxito
                responseApi.EsCorrecto = true;
                responseApi.Valor = listaSaldoCuentum;
            }
            catch (Exception ex)
            {
                // Configura la respuesta en caso de error
                responseApi.EsCorrecto = false;
                responseApi.Mensajes = ex.Message;
            }

            return Ok(responseApi);
        }

    }
}

    

