using BlazorCrud.Server.Models;
using BlazorCrud.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorCrud.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovimientoCuentaController : ControllerBase
    {
        private readonly ProyectoAnalisisContext _dbcontent;

        public MovimientoCuentaController(ProyectoAnalisisContext dbcontent)
        {
            _dbcontent = dbcontent;
        }



        [HttpGet]
        [Route("Lista")]

        public async Task<IActionResult> Lista()
        {
            var responseApi = new ResponseAPI<List<MovimientoCuentaMC>>();
            var listaMoviminetoC = new List<MovimientoCuentaMC>();
            try
            {
                var vsaldo = await _dbcontent.MovimientoCuenta
                    .Include(d => d.IdTipoMovimientoCxcNavigation)
                    .Include(d => d.IdSaldoCuentaNavigation)
                    .ToListAsync();

                foreach (var item in await _dbcontent.MovimientoCuenta.ToListAsync())
                {
                    listaMoviminetoC.Add(new MovimientoCuentaMC
                    {

                        IdMovimientoCuenta = item.IdMovimientoCuenta,
                        IdSaldoCuenta = item.IdSaldoCuenta,

                        saldo = item.IdSaldoCuentaNavigation != null ? new SaldoCuentumSC
                        {
                            IdSaldoCuenta = item.IdSaldoCuentaNavigation.IdSaldoCuenta,
                        } : null,

                        IdTipoMovimientoCxc = item.IdTipoMovimientoCxc,

                        movimiento = item.IdTipoMovimientoCxcNavigation != null ? new TipoMovCuentaCorrienteCC
                        {
                            IdTipoMovimientoCXC = item.IdTipoMovimientoCxcNavigation.IdTipoMovimientoCxc,
                            Nombre = item.IdTipoMovimientoCxcNavigation.Nombre,
                        } : null,

                        FechaMovimiento = item.FechaMovimiento,
                        ValorMovimiento = item.ValorMovimiento,
                        ValorMovimientoPagado = item.ValorMovimientoPagado,
                        GeneradoAutomaticamente = item.GeneradoAutomaticamente,
                        Descripcion = item.Descripcion,
                        FechaCreacion = item.FechaCreacion,
                        UsuarioCreacion = item.UsuarioCreacion,
                        FechaModificacion = item.FechaModificacion,
                        UsuarioModificacion = item.UsuarioModificacion,

                    });
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = listaMoviminetoC;
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
            var responseApi = new ResponseAPI<MovimientoCuentaMC>();
            var MovimientoCuenta = new MovimientoCuentaMC();

            try
            {
                var dbMovCuenta = await _dbcontent.MovimientoCuenta.FirstOrDefaultAsync(X => X.IdMovimientoCuenta == id);
                if (dbMovCuenta != null)
                {

                    MovimientoCuenta.IdSaldoCuenta = dbMovCuenta.IdSaldoCuenta;
                    MovimientoCuenta.IdTipoMovimientoCxc = dbMovCuenta.IdTipoMovimientoCxc;
                    MovimientoCuenta.FechaMovimiento = dbMovCuenta.FechaMovimiento;
                    MovimientoCuenta.ValorMovimiento = dbMovCuenta.ValorMovimiento;
                    MovimientoCuenta.ValorMovimientoPagado = dbMovCuenta.ValorMovimientoPagado;
                    MovimientoCuenta.GeneradoAutomaticamente = dbMovCuenta.GeneradoAutomaticamente;
                    MovimientoCuenta.Descripcion = dbMovCuenta.Descripcion;
                    MovimientoCuenta.FechaCreacion = dbMovCuenta.FechaCreacion;
                    MovimientoCuenta.UsuarioCreacion = dbMovCuenta.UsuarioCreacion;
                    MovimientoCuenta.FechaModificacion = dbMovCuenta.FechaModificacion;
                    MovimientoCuenta.UsuarioModificacion = dbMovCuenta.UsuarioModificacion;

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = MovimientoCuenta;
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
        public async Task<IActionResult> Guardar(MovimientoCuentaMC item)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                // Validaciones de los datos recibidos
                if (item == null)
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensajes = "Los datos del movimiento no son válidos.";
                    return BadRequest(responseApi);
                }

                var dbMovCuenta = new MovimientoCuentum
                {
                    IdSaldoCuenta = item.IdSaldoCuenta,
                    IdTipoMovimientoCxc = item.IdTipoMovimientoCxc,
                    FechaMovimiento = item.FechaMovimiento,
                    ValorMovimiento = item.ValorMovimiento,
                    ValorMovimientoPagado = item.ValorMovimientoPagado,
                    GeneradoAutomaticamente = item.GeneradoAutomaticamente,
                    Descripcion = item.Descripcion,
                    FechaCreacion = item.FechaCreacion,
                    UsuarioCreacion = item.UsuarioCreacion,
                    FechaModificacion = item.FechaModificacion,
                    UsuarioModificacion = item.UsuarioModificacion
                };

                _dbcontent.MovimientoCuenta.Add(dbMovCuenta);
                await _dbcontent.SaveChangesAsync();

                // Actualizar el saldo de la cuenta afectada
                var saldoCuenta = await _dbcontent.SaldoCuenta.FindAsync(item.IdSaldoCuenta);
                if (saldoCuenta != null)
                {

                    saldoCuenta.SaldoAnterior = saldoCuenta.SaldoActual;

                    if (item.IdTipoMovimientoCxc == 1) // débito
                    {
                        saldoCuenta.Debitos += item.ValorMovimiento;
                    }
                    else if (item.IdTipoMovimientoCxc == 2) // crédito
                    {
                        saldoCuenta.Creditos += item.ValorMovimiento;
                    }

                    await _dbcontent.SaveChangesAsync();
                }

                if (dbMovCuenta.IdMovimientoCuenta != 0)
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = dbMovCuenta.IdMovimientoCuenta;
                    return CreatedAtAction(nameof(Guardar), new { id = dbMovCuenta.IdMovimientoCuenta }, responseApi); // Retorna un 201 con el ID del objeto creado
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensajes = "El movimiento no se pudo guardar.";
                    return BadRequest(responseApi);
                }

            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensajes = $"Error: {ex.Message}"; 
                return StatusCode(StatusCodes.Status500InternalServerError, responseApi); 
            }
        }

        [HttpPost]
        [Route("AnularMovimiento")]
        public async Task<IActionResult> AnularMovimiento(int movimientoId)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                // Buscar el movimiento original
                var movOriginal = await _dbcontent.MovimientoCuenta.FindAsync(movimientoId);
                if (movOriginal == null)
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensajes = "Movimiento no encontrado.";
                    return BadRequest(responseApi);
                }

                // Crear un movimiento de anulación
                var movAnulacion = new MovimientoCuentum
                {
                    IdSaldoCuenta = movOriginal.IdSaldoCuenta,
                    IdTipoMovimientoCxc = movOriginal.IdTipoMovimientoCxc,
                    FechaMovimiento = DateTime.Now,
                    ValorMovimiento = -movOriginal.ValorMovimiento,
                    Descripcion = "Anulación de movimiento ID " + movimientoId,
                    FechaCreacion = DateTime.Now,
                    UsuarioCreacion = "Sistema"
                };

                // Guardar el movimiento de anulación
                _dbcontent.MovimientoCuenta.Add(movAnulacion);
                await _dbcontent.SaveChangesAsync();

                // Actualizar el saldo de la cuenta
                var saldoCuenta = await _dbcontent.SaldoCuenta.FindAsync(movOriginal.IdSaldoCuenta);
                if (saldoCuenta != null)
                {
                    if (movOriginal.IdTipoMovimientoCxc == 1) // Débito
                    {
                        saldoCuenta.Debitos -= movOriginal.ValorMovimiento;
                    }
                    else if (movOriginal.IdTipoMovimientoCxc == 2) // Crédito
                    {
                        saldoCuenta.Creditos -= movOriginal.ValorMovimiento;
                    }

                    saldoCuenta.SaldoAnterior = saldoCuenta.SaldoActual;

                    await _dbcontent.SaveChangesAsync();
                }

                responseApi.EsCorrecto = true;
                responseApi.Valor = movAnulacion.IdMovimientoCuenta;
                return Ok(responseApi);

            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensajes = $"Error: {ex.Message}";
                return StatusCode(StatusCodes.Status500InternalServerError, responseApi);
            }
        }

       

        [HttpGet]
        [Route("ObtenerMovimientosPorPersonaMes/{idPersona}/{mes}")]
        public async Task<IActionResult> ObtenerMovimientosPorPersonaMes(int idPersona, int mes)
        {
            if (mes < 1 || mes > 12)
            {
                return BadRequest(new { Message = "El mes debe estar entre 1 y 12." });
            }

            try
            {
                // Realizar la consulta para obtener los movimientos de una persona en un mes específico
                var movimientos = await _dbcontent.MovimientoCuenta
                    .Where(m => m.IdSaldoCuentaNavigation.IdPersona == idPersona && m.FechaMovimiento.Month == mes)
                    .Include(m => m.IdTipoMovimientoCxcNavigation)    // Incluye la relación TipoMovimiento si es necesario
                    .Include(m => m.IdSaldoCuentaNavigation)         // Incluye la relación SaldoCuenta para acceder a IdPersona
                    .Select(m => new MovimientoCuentaMC
                    {
                        IdMovimientoCuenta = m.IdMovimientoCuenta,
                        FechaMovimiento = m.FechaMovimiento,
                        ValorMovimiento = m.ValorMovimiento,
                        Descripcion = m.Descripcion,
                        // Agrega otros campos si es necesario
                    })
                    .ToListAsync();

                // Verificar si se encontraron movimientos
                if (movimientos == null || !movimientos.Any())
                {
                    return NotFound(new { Message = "No se encontraron movimientos para la persona en el mes especificado." });
                }

                // Devolver los movimientos en formato JSON
                return Ok(movimientos);
            }
            catch (Exception ex)
            {
                // Manejo de errores
                return StatusCode(500, new { Message = "Error al obtener los movimientos.", Details = ex.Message });
            }
        }

    }
}
