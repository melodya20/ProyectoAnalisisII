using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using BlazorCrud.Server.Models;
using BlazorCrud.Shared;
using Microsoft.EntityFrameworkCore;

namespace BlazorCrud.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly ProyectoAnalisisContext _dbcontent;

        public MenuController(ProyectoAnalisisContext dbcontent)
        {
            _dbcontent = dbcontent;
        }

        [HttpGet]
        [Route("Lista")]

        public async Task<IActionResult> Lista()
        {
            var responseApi = new ResponseAPI<List<MenuMN>>();
            var listaMenuMN = new List<MenuMN>();
            try
            {
                foreach (var item in await _dbcontent.Menus.Include(d => d.IdModuloNavigation).ToListAsync())
                {
                    listaMenuMN.Add(new MenuMN
                    {
                        IdMenu = item.IdMenu,
                        IdModulo = item.IdModulo,
                        Modulo = new ModuloMD
                        {
                            IdModulo = item.IdModuloNavigation.IdModulo,
                            Nombre = item.IdModuloNavigation.Nombre,
                            OrdenMenu = item.IdModuloNavigation.OrdenMenu,
                            FechaCreacion = item.IdModuloNavigation.FechaCreacion,
                            UsuarioCreacion = item.IdModuloNavigation.UsuarioCreacion,
                            FechaModificacion = item.IdModuloNavigation.FechaModificacion,
                            UsuarioModificacion = item.IdModuloNavigation.UsuarioModificacion,
                        },
                        Nombre = item.Nombre,
                        OrdenMenu = item.OrdenMenu,
                        FechaCreacion = item.FechaCreacion,
                        UsuarioCreacion = item.UsuarioCreacion,
                        FechaModificacion = item.FechaModificacion,
                        UsuarioModificacion = item.UsuarioModificacion,
                    });
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = listaMenuMN;
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
            var responseApi = new ResponseAPI<MenuMN>();
            var MenuMN = new MenuMN();

            try
            {
                var dbMenuMN = await _dbcontent.Menus.FirstOrDefaultAsync(X => X.IdMenu == id);
                if (dbMenuMN != null)
                {

                    MenuMN.IdMenu = dbMenuMN.IdMenu;
                    MenuMN.IdModulo = dbMenuMN.IdModulo;
                    MenuMN.Nombre = dbMenuMN.Nombre;
                    MenuMN.OrdenMenu = dbMenuMN.OrdenMenu;
                    MenuMN.FechaCreacion = dbMenuMN.FechaCreacion;
                    MenuMN.UsuarioCreacion = dbMenuMN.UsuarioCreacion;
                    MenuMN.FechaModificacion = dbMenuMN.FechaModificacion;
                    MenuMN.UsuarioModificacion = dbMenuMN.UsuarioModificacion;

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = MenuMN;

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

        public async Task<IActionResult> Guardar(MenuMN menu)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbMenu = new Menu
                {
                    IdMenu = menu.IdMenu,
                    IdModulo = menu.IdModulo,
                    Nombre = menu.Nombre,
                    OrdenMenu = menu.OrdenMenu,
                    FechaCreacion = menu.FechaCreacion,
                    UsuarioCreacion = menu.UsuarioCreacion,
                    FechaModificacion = menu.FechaModificacion,
                    UsuarioModificacion = menu.UsuarioModificacion,
                };

                _dbcontent.Menus.Add(dbMenu);
                await _dbcontent.SaveChangesAsync();

                if (dbMenu.IdMenu != 0)
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = dbMenu.IdMenu;
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

        public async Task<IActionResult> Editar(MenuMN menu, int id)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbMenuMN = await _dbcontent.Menus.FirstOrDefaultAsync(e => e.IdMenu == id);


                if (dbMenuMN != null)
                {

                    dbMenuMN.IdMenu = menu.IdMenu;
                    dbMenuMN.IdModulo = menu.IdModulo;
                    dbMenuMN.Nombre = menu.Nombre;
                    dbMenuMN.OrdenMenu = menu.OrdenMenu;
                    dbMenuMN.FechaCreacion = menu.FechaCreacion;
                    dbMenuMN.UsuarioCreacion = menu.UsuarioCreacion;
                    dbMenuMN.FechaModificacion = menu.FechaModificacion;
                    dbMenuMN.UsuarioModificacion = menu.UsuarioModificacion;


                    _dbcontent.Menus.Update(dbMenuMN);
                    await _dbcontent.SaveChangesAsync();

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = dbMenuMN.IdMenu;


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
                var dbMenu = await _dbcontent.Menus.FirstOrDefaultAsync(e => e.IdMenu == id);


                if (dbMenu != null)
                {



                    _dbcontent.Menus.Remove(dbMenu);
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
