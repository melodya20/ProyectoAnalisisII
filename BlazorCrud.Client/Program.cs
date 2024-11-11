using BlazorCrud.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorCrud.Client.Services;
using CurrieTechnologies.Razor.SweetAlert2;

using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Authorization;
using BlazorCrud.Client.Extensiones;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5176") });
builder.Services.AddScoped<IGenero, GeneroService>();
builder.Services.AddScoped<IBitacora, BitacoraService>();
builder.Services.AddScoped<ITipoAcceso, TipoAccesoService>();
builder.Services.AddScoped<IStatusUsuario, StatusUsuarioService>();
builder.Services.AddScoped<IModulo, ModuloService>();
builder.Services.AddScoped<IMenu, MenuService>();
builder.Services.AddScoped<IUsuarioServices, UsuarioService>();
builder.Services.AddScoped<IUsuarioPregunta, UsuarioPreguntaService>();
builder.Services.AddScoped<IRole, RoleService>();
builder.Services.AddScoped<IRoleOP, RoleOpService>();
builder.Services.AddScoped<IOpcion, OpcionService>();
builder.Services.AddScoped<IUsuarioRole, UsuarioRoleService>();
builder.Services.AddScoped<ISucursal, SucursalService>();
builder.Services.AddScoped<IEmpresaService, EmpresaService>();
builder.Services.AddScoped<IStatusCuentum, StatusCuentumService>();
builder.Services.AddScoped<IEstadoCivil, EstadoCivilService>();
builder.Services.AddScoped<ITipoSaldoCuenta, TipoSaldoCuentaService>();
builder.Services.AddScoped<IPersona, PersonaService>();
builder.Services.AddScoped<ITipoDocumento, TipoDocumentoService>();
builder.Services.AddScoped<ITipoMovCuentaCorriente, TipoMovCuentaCorrienteService>();
builder.Services.AddScoped<ISaldoCuenta, SaldoCuentumService>();
builder.Services.AddScoped<IMovCuenta, MovimientoCuentaService>();
builder.Services.AddScoped<CierreMensualService>();


builder.Services.AddSweetAlert2();

builder.Services.AddScoped<AuthenticationStateProvider, AutenticacionExtension>();
builder.Services.AddBlazoredSessionStorage();
builder.Services.AddAuthorizationCore();

await builder.Build().RunAsync();
