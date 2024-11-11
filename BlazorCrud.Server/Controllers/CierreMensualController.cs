using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BlazorCrud.Server.Models;
using BlazorCrud.Shared;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BlazorCrud.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CierreMensualController : ControllerBase
    {
        private readonly ProyectoAnalisisContext _dbcontent;

        // Variable para activar o desactivar el modo de prueba
        private readonly bool modoPrueba = true;

        public CierreMensualController(ProyectoAnalisisContext dbcontent)
        {
            _dbcontent = dbcontent;
        }

        [HttpPost("RealizarCierre")]
        public async Task<IActionResult> RealizarCierre()
        {
            try
            {
                // Establece la fecha actual
                var fechaActual = DateTime.Now;

                // Si está en modo de prueba, usa una fecha específica
                if (modoPrueba)
                {
                    fechaActual = new DateTime(2024, 11, 30); // Fecha de prueba
                }

                // Define el primer y último día del mes
                var primerDiaMes = new DateTime(fechaActual.Year, fechaActual.Month, 1);
                var ultimoDiaMes = primerDiaMes.AddMonths(1).AddDays(-1);

                // Verifica si es el último día del mes (excepto en modo prueba)
                if (!modoPrueba && fechaActual.Day != ultimoDiaMes.Day)
                {
                    return BadRequest("El cierre mensual solo puede realizarse el último día del mes.");
                }

                // Obtiene todos los movimientos del mes actual
                var movimientosMesActual = await _dbcontent.MovimientoCuenta
                    .Where(m => m.FechaMovimiento >= primerDiaMes && m.FechaMovimiento <= ultimoDiaMes)
                    .ToListAsync();

                // Si no hay movimientos para cerrar, retorna un mensaje
                if (!movimientosMesActual.Any())
                {
                    return BadRequest("No hay movimientos para cerrar este mes.");
                }

                // Procesa cada movimiento para actualizar el saldo en la tabla SaldoCuenta
                foreach (var movimiento in movimientosMesActual)
                {
                    var saldoCuenta = await _dbcontent.SaldoCuenta
                        .FirstOrDefaultAsync(s => s.IdSaldoCuenta == movimiento.IdSaldoCuenta);

                    if (saldoCuenta != null)
                    {
                        // Acumula los débitos y créditos del mes en el saldo correspondiente
                        saldoCuenta.Debitos += movimiento.ValorMovimiento;
                        saldoCuenta.Creditos += movimiento.ValorMovimientoPagado;

                        // Actualiza el saldo anterior para el nuevo mes usando el saldo actual calculado
                        saldoCuenta.SaldoAnterior = saldoCuenta.SaldoAnterior + saldoCuenta.Debitos - saldoCuenta.Creditos;

                        // Resetea débitos y créditos para el nuevo mes
                        saldoCuenta.Debitos = 0;
                        saldoCuenta.Creditos = 0;
                    }
                }

                // Guarda los cambios en la base de datos
                await _dbcontent.SaveChangesAsync();
                return Ok("Cierre mensual completado.");
            }
            catch (Exception ex)
            {
                // Captura cualquier error y devuelve un mensaje de error
                return StatusCode(500, $"Error al realizar el cierre: {ex.Message}");
            }
        }

    }
}
