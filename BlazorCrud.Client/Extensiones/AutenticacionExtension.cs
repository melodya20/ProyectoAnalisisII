using BlazorCrud.Shared;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;
using System.Security.Claims;

namespace BlazorCrud.Client.Extensiones
{
    public class AutenticacionExtension : AuthenticationStateProvider
    {
        private readonly ISessionStorageService _sessionStorage;
        private readonly NavigationManager _navManager;
        private readonly HttpClient _httpClient;
        private ClaimsPrincipal _sinInformacion = new ClaimsPrincipal(new ClaimsIdentity());

        public AutenticacionExtension(ISessionStorageService sessionStorage, NavigationManager navManager, HttpClient httpClient)
        {
            _sessionStorage = sessionStorage;
            _navManager = navManager;
            _httpClient = httpClient;

        }

        public async Task ActualizarEstadoAutenticacion(SesionDTO? sesionUsuario, bool esAccesoConcedido, bool esUsuarioInactivo, bool esPasswordIncorrecto)
        {
            ClaimsPrincipal claimsPrincipal;

            if (sesionUsuario != null)
            {
                sesionUsuario.UltimaActividad = DateTime.Now;

                claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.Name,sesionUsuario.Nombre),
                    new Claim(ClaimTypes.Email,sesionUsuario.Correo),
                    new Claim(ClaimTypes.Role,sesionUsuario.Rol)
                }, "JwtAuth"));

                await _sessionStorage.GuardarStorage("sesionUsuario", sesionUsuario);

                // Determinar el tipo de acceso (acción) basado en las condiciones de autenticación
                int idTipoAcceso;
                string accion;

                if (esAccesoConcedido)
                {
                    idTipoAcceso = 1; // Acceso Concedido
                    accion = "Acceso Concedido";
                }
                else if (esPasswordIncorrecto)
                {
                    idTipoAcceso = 2; // Bloqueado - Password incorrecto
                    accion = "Bloqueado - Password incorrecto/Numero de intentos excedidos";
                }
                else if (esUsuarioInactivo)
                {
                    idTipoAcceso = 3; // Usuario Inactivo
                    accion = "Usuario Inactivo";
                }
                else
                {
                    idTipoAcceso = 4; // Usuario ingresado no existe
                    accion = "Usuario ingresado no existe";
                }

                // Registrar en la bitácora
                await RegistrarAccesoEnBitacora(sesionUsuario, idTipoAcceso, accion);
            }
            else
            {
                claimsPrincipal = _sinInformacion;
                await _sessionStorage.RemoveItemAsync("sesionUsuario");
            }

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));

            if (sesionUsuario == null)
            {
                _navManager.NavigateTo("/"); // Redirigir a la página de inicio o login
            }
        }

        // Método para registrar el acceso en la bitácora
        private async Task RegistrarAccesoEnBitacora(SesionDTO sesionUsuario, int idTipoAcceso, string accion)
        {

            var bitacoraAcceso = new BitacoraAccesoDTO
            {
                IdUsuario = sesionUsuario.Correo,
                IdTipoAcceso = idTipoAcceso,
                Accion = accion,
                HttpUserAgent = sesionUsuario.HttpUserAgent,
                DireccionIp = sesionUsuario.DireccionIp,
                SistemaOperativo = sesionUsuario.SistemaOperativo,
                Dispositivo = sesionUsuario.Dispositivo,
                Browser = sesionUsuario.Browser,
                Sesion = sesionUsuario.Sesion
            };

            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/bitacora_acceso/RegistrarAcceso", bitacoraAcceso);
                if (!response.IsSuccessStatusCode)
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error al registrar el acceso en la bitácora: {errorMessage}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Excepción al registrar el acceso en la bitácora: {ex.Message}");
            }
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            await VerificarInactividad();

            var sesionUsuario = await _sessionStorage.ObtenerStorage<SesionDTO>("sesionUsuario");

            if (sesionUsuario == null)
                return await Task.FromResult(new AuthenticationState(_sinInformacion));

            var claimPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.Name,sesionUsuario.Nombre),
                    new Claim(ClaimTypes.Email, sesionUsuario.Correo),
                    new Claim(ClaimTypes.Role, sesionUsuario.Rol)
                }, "JwtAuth"));


            return await Task.FromResult(new AuthenticationState(claimPrincipal));

        }
        public async Task<bool> VerificarInactividad()
        {
            var sesionUsuario = await _sessionStorage.ObtenerStorage<SesionDTO>("sesionUsuario");

            if (sesionUsuario != null && (DateTime.Now - sesionUsuario.UltimaActividad) > TimeSpan.FromMinutes(1)) // Ajusta el tiempo según tu necesidad
            {
                Console.WriteLine("Inactividad detectada, cerrando sesión");
                await ActualizarEstadoAutenticacion(null, false, false, false); // Cerrar sesión
                return true;
            }
            return false;
        }

        public async Task ActualizarUltimaActividad()
        {
            var sesionUsuario = await _sessionStorage.ObtenerStorage<SesionDTO>("sesionUsuario");

            if (sesionUsuario != null)
            {
                sesionUsuario.UltimaActividad = DateTime.Now; // Actualiza la última actividad
                await _sessionStorage.GuardarStorage("sesionUsuario", sesionUsuario); // Guarda los cambios
            }
        }
    }
}
