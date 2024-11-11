using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BlazorCrud.Server.Models;
using BlazorCrud.Shared;
using Microsoft.EntityFrameworkCore;

namespace BlazorCrud.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class bitacora_accesoController : ControllerBase
    {
        private readonly ProyectoAnalisisContext _dbcontent;

        public bitacora_accesoController(ProyectoAnalisisContext dbcontent)
        {
            _dbcontent = dbcontent;
        }

        // Método para registrar acceso en la bitácora utilizando BitacoraAcceso
        [HttpPost("RegistrarAcceso")]
        public async Task<IActionResult> RegistrarAcceso([FromBody] BitacoraAccesoDTO bitacoraAcceso)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                // Procesar y guardar datos
                var nuevaBitacora = new BitacoraAcceso
                {
                    IdUsuario = bitacoraAcceso.IdUsuario,
                    IdTipoAcceso = bitacoraAcceso.IdTipoAcceso,
                    FechaAcceso = DateTime.Now,
                    HttpUserAgent = bitacoraAcceso.HttpUserAgent,
                    DireccionIp = bitacoraAcceso.DireccionIp,
                    Accion = bitacoraAcceso.Accion,
                    SistemaOperativo = bitacoraAcceso.SistemaOperativo,
                    Dispositivo = bitacoraAcceso.Dispositivo,
                    Browser = bitacoraAcceso.Browser,
                    Sesion = bitacoraAcceso.Sesion
                };

                _dbcontent.BitacoraAccesos.Add(nuevaBitacora);
                await _dbcontent.SaveChangesAsync();

                responseApi.EsCorrecto = true;
                responseApi.Valor = nuevaBitacora.IdBitacoraAcceso;
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensajes = $"Error al registrar el acceso: {ex.Message}";
            }

            return Ok(responseApi);
        }


        // Método para obtener la lista de bitácoras
        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var responseApi = new ResponseAPI<List<BitacoraBT>>();
            var listaBitacoraBT = new List<BitacoraBT>();

            try
            {
                var accesos = await _dbcontent.BitacoraAccesos.Include(d => d.IdTipoAccesoNavigation).ToListAsync();

                foreach (var item in accesos)
                {
                    listaBitacoraBT.Add(new BitacoraBT
                    {
                        IdBitacoraAcceso = item.IdBitacoraAcceso,
                        IdUsuario = item.IdUsuario,
                        IdTipoAcceso = item.IdTipoAcceso,
                        TipoAcceso = new TipoAccesoTA
                        {
                            IdTipoAcceso = item.IdTipoAccesoNavigation.IdTipoAcceso,
                            Nombre = item.IdTipoAccesoNavigation.Nombre,
                            FechaCreacion = item.IdTipoAccesoNavigation.FechaCreacion,
                            UsuarioCreacion = item.IdTipoAccesoNavigation.UsuarioCreacion,
                            FechaModificacion = item.IdTipoAccesoNavigation.FechaModificacion,
                            UsuarioModificacion = item.IdTipoAccesoNavigation.UsuarioModificacion,
                        },
                        FechaAcceso = item.FechaAcceso,
                        HttpUserAgent = item.HttpUserAgent,
                        DireccionIp = item.DireccionIp,
                        Accion = item.Accion,
                        SistemaOperativo = item.SistemaOperativo,
                        Dispositivo = item.Dispositivo,
                        Browser = item.Browser,
                        Sesion = item.Sesion,
                    });
                }

                responseApi.EsCorrecto = true;
                responseApi.Valor = listaBitacoraBT;
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensajes = "Error al obtener la lista de bitácoras: " + ex.Message;
            }

            return Ok(responseApi);
        }

        // Método para buscar una bitácora por su id
        [HttpGet]
        [Route("Buscar/{id}")]
        public async Task<IActionResult> Buscar(int id)
        {
            var responseApi = new ResponseAPI<BitacoraBT>();
            var bitacoraBT = new BitacoraBT();

            try
            {
                var dbBitacoraBT = await _dbcontent.BitacoraAccesos.FirstOrDefaultAsync(x => x.IdBitacoraAcceso == id);
                if (dbBitacoraBT != null)
                {
                    bitacoraBT.IdBitacoraAcceso = dbBitacoraBT.IdBitacoraAcceso;
                    bitacoraBT.IdUsuario = dbBitacoraBT.IdUsuario;
                    bitacoraBT.IdTipoAcceso = dbBitacoraBT.IdTipoAcceso;
                    bitacoraBT.FechaAcceso = dbBitacoraBT.FechaAcceso;
                    bitacoraBT.HttpUserAgent = dbBitacoraBT.HttpUserAgent;
                    bitacoraBT.DireccionIp = dbBitacoraBT.DireccionIp;
                    bitacoraBT.Accion = dbBitacoraBT.Accion;
                    bitacoraBT.SistemaOperativo = dbBitacoraBT.SistemaOperativo;
                    bitacoraBT.Dispositivo = dbBitacoraBT.Dispositivo;
                    bitacoraBT.Browser = dbBitacoraBT.Browser;
                    bitacoraBT.Sesion = dbBitacoraBT.Sesion;

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = bitacoraBT;
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

        // Método para guardar una bitácora
        [HttpPost]
        [Route("Guardar")]
        public async Task<IActionResult> Guardar(BitacoraBT bitacora)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbBitacora = new BitacoraAcceso
                {
                    IdBitacoraAcceso = bitacora.IdBitacoraAcceso,
                    IdUsuario = bitacora.IdUsuario,
                    IdTipoAcceso = bitacora.IdTipoAcceso,
                    FechaAcceso = bitacora.FechaAcceso,
                    HttpUserAgent = bitacora.HttpUserAgent,
                    DireccionIp = bitacora.DireccionIp,
                    Accion = bitacora.Accion,
                    SistemaOperativo = bitacora.SistemaOperativo,
                    Dispositivo = bitacora.Dispositivo,
                    Browser = bitacora.Browser,
                    Sesion = bitacora.Sesion
                };

                _dbcontent.BitacoraAccesos.Add(dbBitacora);
                await _dbcontent.SaveChangesAsync();

                if (dbBitacora.IdBitacoraAcceso != 0)
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = dbBitacora.IdBitacoraAcceso;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensajes = "No se pudo registrar";
                }
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensajes = ex.Message;
            }

            return Ok(responseApi);
        }

        // Método para editar una bitácora
        [HttpPut]
        [Route("Editar/{id}")]
        public async Task<IActionResult> Editar(int id, [FromBody] BitacoraBT bitacora)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbBitacora = await _dbcontent.BitacoraAccesos.FirstOrDefaultAsync(e => e.IdBitacoraAcceso == id);

                if (dbBitacora != null)
                {
                    dbBitacora.IdUsuario = bitacora.IdUsuario;
                    dbBitacora.IdTipoAcceso = bitacora.IdTipoAcceso;
                    dbBitacora.FechaAcceso = bitacora.FechaAcceso;
                    dbBitacora.HttpUserAgent = bitacora.HttpUserAgent;
                    dbBitacora.DireccionIp = bitacora.DireccionIp;
                    dbBitacora.Accion = bitacora.Accion;
                    dbBitacora.SistemaOperativo = bitacora.SistemaOperativo;
                    dbBitacora.Dispositivo = bitacora.Dispositivo;
                    dbBitacora.Browser = bitacora.Browser;
                    dbBitacora.Sesion = bitacora.Sesion;

                    await _dbcontent.SaveChangesAsync();
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = dbBitacora.IdBitacoraAcceso;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensajes = "No se encontró la bitácora";
                }
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensajes = ex.Message;
            }

            return Ok(responseApi);
        }

        // Método para eliminar una bitácora
        [HttpDelete]
        [Route("Eliminar/{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbBitacora = await _dbcontent.BitacoraAccesos.FirstOrDefaultAsync(e => e.IdBitacoraAcceso == id);

                if (dbBitacora != null)
                {
                    _dbcontent.BitacoraAccesos.Remove(dbBitacora);
                    await _dbcontent.SaveChangesAsync();
                    responseApi.EsCorrecto = true;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensajes = "No se encontró la bitácora";
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
