//using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using BlazorCrud.Server.Models;
using BlazorCrud.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using BlazorCrud.Server.Data;
using System.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Security.Cryptography;
using System.Text;

namespace BlazorCrud.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly ProyectoAnalisisContext _dbcontent;

        public UsuarioController(ProyectoAnalisisContext dbcontent)
        {
            _dbcontent = dbcontent;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var responseApi = new ResponseAPI<List<UsuarioUSO>>();
            var listaUsuarioUSO = new List<UsuarioUSO>();

            try
            {
                var usuarios = await _dbcontent.Usuarios
                    .Include(d => d.IdGeneroNavigation)
                    .Include(d => d.IdStatusUsuarioNavigation)
                    .Include(d => d.IdSucursalNavigation)
                    .ToListAsync();

                foreach (var item in usuarios)
                {
                    listaUsuarioUSO.Add(new UsuarioUSO
                    {
                        IdUsuario = item.IdUsuario,
                        Nombre = item.Nombre,
                        Apellido = item.Apellido,
                        FechaNacimiento = item.FechaNacimiento,
                        IdStatusUsuario = item.IdStatusUsuario,
                        Password = Convert.ToBase64String(item.Password),
                        IdGenero = item.IdGenero,
                        UltimaFechaIngreso = item.UltimaFechaIngreso,
                        IntentosDeAcceso = item.IntentosDeAcceso,
                        SesionActual = item.SesionActual,
                        UltimaFechaCambioPassword = item.UltimaFechaCambioPassword,
                        CorreoElectronico = item.CorreoElectronico,
                        RequiereCambiarPassword = item.RequiereCambiarPassword,
                        Fotografia = item.Fotografia,
                        TelefonoMovil = item.TelefonoMovil,
                        IdSucursal = item.IdSucursal,
                        FechaCreacion = item.FechaCreacion,
                        UsuarioCreacion = item.UsuarioCreacion,
                        FechaModificacion = item.FechaModificacion,
                        UsuarioModificacion = item.UsuarioModificacion,
                    });
                }

                responseApi.EsCorrecto = true;
                responseApi.Valor = listaUsuarioUSO;
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
        public async Task<IActionResult> Buscar(string id)
        {
            var responseApi = new ResponseAPI<UsuarioUSO>();
            var usuarioUSO = new UsuarioUSO();

            try
            {
                var dbUsuario = await _dbcontent.Usuarios
                    .Include(d => d.IdGeneroNavigation)
                    .Include(d => d.IdStatusUsuarioNavigation)
                    .Include(d => d.IdSucursalNavigation)
                    .FirstOrDefaultAsync(x => x.IdUsuario == id);

                if (dbUsuario != null)
                {
                    usuarioUSO = new UsuarioUSO
                    {
                        IdUsuario = dbUsuario.IdUsuario,
                        Nombre = dbUsuario.Nombre,
                        Apellido = dbUsuario.Apellido,
                        FechaNacimiento = dbUsuario.FechaNacimiento,
                        IdStatusUsuario = dbUsuario.IdStatusUsuario,
                        Password = Convert.ToBase64String(dbUsuario.Password),
                        IdGenero = dbUsuario.IdGenero,
                        UltimaFechaIngreso = dbUsuario.UltimaFechaIngreso,
                        IntentosDeAcceso = dbUsuario.IntentosDeAcceso,
                        SesionActual = dbUsuario.SesionActual,
                        UltimaFechaCambioPassword = dbUsuario.UltimaFechaCambioPassword,
                        CorreoElectronico = dbUsuario.CorreoElectronico,
                        RequiereCambiarPassword = dbUsuario.RequiereCambiarPassword,
                        Fotografia = dbUsuario.Fotografia,
                        TelefonoMovil = dbUsuario.TelefonoMovil,
                        IdSucursal = dbUsuario.IdSucursal,
                        FechaCreacion = dbUsuario.FechaCreacion,
                        UsuarioCreacion = dbUsuario.UsuarioCreacion,
                        FechaModificacion = dbUsuario.FechaModificacion,
                        UsuarioModificacion = dbUsuario.UsuarioModificacion,
                    };

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = usuarioUSO;
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
        public async Task<IActionResult> Guardar(UsuarioUSO usuario)
        {
            var responseApi = new ResponseAPI<string>();

            try
            {
                byte[] hashedPassword = ComputeHashG(usuario.Password);
                var dbUsuario = new Usuario
                {
                    IdUsuario = usuario.IdUsuario,
                    Nombre = usuario.Nombre,
                    Apellido = usuario.Apellido,
                    FechaNacimiento = usuario.FechaNacimiento,
                    IdStatusUsuario = usuario.IdStatusUsuario,
                    Password = hashedPassword,
                    IdGenero = usuario.IdGenero,
                    UltimaFechaIngreso = usuario.UltimaFechaIngreso,
                    IntentosDeAcceso = usuario.IntentosDeAcceso,
                    SesionActual = usuario.SesionActual,
                    UltimaFechaCambioPassword = usuario.UltimaFechaCambioPassword,
                    CorreoElectronico = usuario.CorreoElectronico,
                    RequiereCambiarPassword = usuario.RequiereCambiarPassword,
                    Fotografia = usuario.Fotografia,
                    TelefonoMovil = usuario.TelefonoMovil,
                    IdSucursal = usuario.IdSucursal,
                    FechaCreacion = usuario.FechaCreacion,
                    UsuarioCreacion = usuario.UsuarioCreacion,
                    FechaModificacion = usuario.FechaModificacion,
                    UsuarioModificacion = usuario.UsuarioModificacion,
                };

                _dbcontent.Usuarios.Add(dbUsuario);
                await _dbcontent.SaveChangesAsync();

                if (!string.IsNullOrEmpty(dbUsuario.IdUsuario))
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = dbUsuario.IdUsuario;
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
        public async Task<IActionResult> Editar(UsuarioUSO usuario, string id)
        {
            var responseApi = new ResponseAPI<string>();

            try
            {
                var dbUsuario = await _dbcontent.Usuarios.FirstOrDefaultAsync(e => e.IdUsuario == id);

                if (dbUsuario != null)
                {
                    dbUsuario.Nombre = usuario.Nombre;
                    dbUsuario.Apellido = usuario.Apellido;
                    dbUsuario.FechaNacimiento = usuario.FechaNacimiento;
                    dbUsuario.IdStatusUsuario = usuario.IdStatusUsuario;
                    dbUsuario.Password = ComputeHashG(usuario.Password);
                    dbUsuario.IdGenero = usuario.IdGenero;
                    dbUsuario.UltimaFechaIngreso = usuario.UltimaFechaIngreso;
                    dbUsuario.IntentosDeAcceso = usuario.IntentosDeAcceso;
                    dbUsuario.SesionActual = usuario.SesionActual;
                    dbUsuario.UltimaFechaCambioPassword = usuario.UltimaFechaCambioPassword;
                    dbUsuario.CorreoElectronico = usuario.CorreoElectronico;
                    dbUsuario.RequiereCambiarPassword = usuario.RequiereCambiarPassword;
                    dbUsuario.Fotografia = usuario.Fotografia;
                    dbUsuario.TelefonoMovil = usuario.TelefonoMovil;
                    dbUsuario.IdSucursal = usuario.IdSucursal;
                    dbUsuario.FechaCreacion = usuario.FechaCreacion;
                    dbUsuario.UsuarioCreacion = usuario.UsuarioCreacion;
                    dbUsuario.FechaModificacion = usuario.FechaModificacion;
                    dbUsuario.UsuarioModificacion = usuario.UsuarioModificacion;

                    _dbcontent.Usuarios.Update(dbUsuario);
                    await _dbcontent.SaveChangesAsync();

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = dbUsuario.IdUsuario;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensajes = "No encontrado";
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
        public async Task<IActionResult> Eliminar(string id)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbUsuario = await _dbcontent.Usuarios.FirstOrDefaultAsync(e => e.IdUsuario == id);

                if (dbUsuario != null)
                {
                    _dbcontent.Usuarios.Remove(dbUsuario);
                    await _dbcontent.SaveChangesAsync();

                    responseApi.EsCorrecto = true;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensajes = "No encontrado";
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
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] UsuarioUSO login)
        {
            var responseApi = new ResponseAPI<RoleRL>();
            try
            {
                if (!ModelState.IsValid)
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensajes = "Datos de entrada inválidos";
                    //return BadRequest(responseApi);
                    return BadRequest("Datos de entrada inválidos");
                }

                if (string.IsNullOrEmpty(login.CorreoElectronico))
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensajes = "El correo electrónico es requerido.";
                    //return BadRequest(responseApi);
                    return BadRequest("El correo electrónico es requerido.");
                }

                SesionDTO sesionDTO = new SesionDTO();
                var usuario = await _dbcontent.Usuarios
                    .FirstOrDefaultAsync(u => u.CorreoElectronico == login.CorreoElectronico);

                string usuarioCorreo = login.CorreoElectronico;


                if (usuario != null)
                {
                    if (usuario.IdStatusUsuario == 2 || usuario.IdStatusUsuario == 3)
                    {
                        responseApi.EsCorrecto = false;
                        responseApi.Mensajes = "Usuario se encuentra bloqueado";
                        //return BadRequest(responseApi);
                        return BadRequest("Usuario se encuentra bloqueado");
                    }

                    if (usuario.IntentosDeAcceso >= 5)
                    {
                        responseApi.EsCorrecto = false;
                        responseApi.Mensajes = "Ha excedido el límite de intentos de acceso. Contacte al administrador.";
                        //return BadRequest(responseApi);
                        return BadRequest("Ha excedido el límite de intentos de acceso. Contacte al administrador.");
                    }

                    int diasParaCaducar = await _dbcontent.Empresas
                        .FromSqlRaw("select E.PasswordCantidadCaducidadDias from EMPRESA E join SUCURSAL s on s.IdEmpresa = E.IdEmpresa join USUARIO u on u.IdSucursal = s.IdSucursal where u.CorreoElectronico = {0}", usuarioCorreo)
                        .Select(e => e.PasswordCantidadCaducidadDias)
                        .FirstOrDefaultAsync();

                    if (usuario.UltimaFechaCambioPassword.AddDays(diasParaCaducar) <= DateTime.Now)
                    {
                        responseApi.EsCorrecto = false;
                        responseApi.Mensajes = "Tu contraseña ha caducado. Debe realizar el cambio.";
                        //return BadRequest(responseApi);
                        return BadRequest("Tu contraseña ha caducado. Debe realizar el cambio.");
                    }

                    string hashedPassword = ComputeHash(login.Password);
                    string storedPasswordHash = BitConverter.ToString((byte[])usuario.Password).Replace("-", "").ToLowerInvariant();

                    if (storedPasswordHash == hashedPassword)
                    {
                        string user = await _dbcontent.Roles
                        .FromSqlRaw("select R.Nombre from ROLE R join USUARIO_ROLE URL on URL.IdRole = R.IdRole join USUARIO U on  U.IdUsuario = URL.IdUsuario where U.CorreoElectronico = {0}", usuarioCorreo)
                        .Select(ur => ur.Nombre)
                        .FirstOrDefaultAsync();

                        {
                            sesionDTO.Nombre = usuario.Nombre;
                            sesionDTO.Correo = usuario.CorreoElectronico;
                            sesionDTO.Rol = user;
                        }

                        responseApi.EsCorrecto = true;
                        responseApi.Valor = null;
                        responseApi.Mensajes = "Correo encontrado.";

                        await _dbcontent.Database.ExecuteSqlRawAsync(
                        "update USUARIO set UltimaFechaIngreso = GETDATE() where CorreoElectronico = {0}", usuarioCorreo);

                        await _dbcontent.Database.ExecuteSqlRawAsync(
                        "update USUARIO set IntentosDeAcceso = 0 where CorreoElectronico = {0}", usuarioCorreo);

                        return StatusCode(StatusCodes.Status200OK, sesionDTO);
                    }
                    else
                    {
                        responseApi.EsCorrecto = false;
                        responseApi.Mensajes = "Correo o contraseña incorrectos";

                        await _dbcontent.Database.ExecuteSqlRawAsync(
                        "update USUARIO set IntentosDeAcceso = IntentosDeAcceso + 1 where CorreoElectronico = {0}", usuarioCorreo);

                        if (usuario.IntentosDeAcceso >= 4)
                        {
                            await _dbcontent.Database.ExecuteSqlRawAsync(
                            "update USUARIO set IdStatusUsuario = 2 where CorreoElectronico = {0}", usuarioCorreo);
                        }
                        //return BadRequest(responseApi);
                        return BadRequest("Correo o contraseña incorrectos");
                    }
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensajes = "Correo o contraseña incorrectos";

                    await _dbcontent.Database.ExecuteSqlRawAsync(
                    "update USUARIO set IntentosDeAcceso = IntentosDeAcceso + 1 where CorreoElectronico = {0}", usuarioCorreo);

                    //return BadRequest(responseApi);
                    return BadRequest("Correo o contraseña incorrectos");
                }
            }
            catch (ArgumentNullException ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensajes = $"Valor nulo detectado: {ex.ParamName}";
                return BadRequest(responseApi);
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensajes = ex.Message;
                return StatusCode(500, responseApi);
            }
        }

        private string ComputeHash(string password)
        {
            using (var md5 = MD5.Create())
            {
                // Convertir la contraseña a un arreglo de bytes y calcular el hash
                byte[] inputBytes = System.Text.Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convertir el hash a una cadena hexadecimal
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
            }
        }

        private byte[] ComputeHashG(string password)
        {
            using (var md5 = MD5.Create())
            {
                // Convertir la contraseña a un arreglo de bytes y calcular el hash
                byte[] inputBytes = System.Text.Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                return hashBytes; // Retornar el arreglo de bytes del hash
            }
        }
    }
}
