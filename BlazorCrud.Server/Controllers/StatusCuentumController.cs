using BlazorCrud.Server.Models;
using BlazorCrud.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorCrud.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusCuentumController : ControllerBase
    {
        private readonly ProyectoAnalisisContext _dbcontent;

        public StatusCuentumController(ProyectoAnalisisContext dbcontent)
        {
            _dbcontent = dbcontent;
        }


        [HttpGet]
        [Route("Lista")]

        public async Task<IActionResult> Lista()
        {
            var responseApi = new ResponseAPI<List<StatusCuentumST>>();
            var listaStatusCuentumST = new List<StatusCuentumST>();
            try
            {
                foreach (var item in await _dbcontent.StatusCuenta.ToListAsync())
                {
                    listaStatusCuentumST.Add(new StatusCuentumST
                    {
                        IdStatusCuentum = item.IdStatusCuenta,
                        Nombre = item.Nombre,
                        FechaCreacion = item.FechaCreacion,
                        UsuarioCreacion = item.UsuarioCreacion,
                        FechaModificacion = item.FechaModificacion,
                        UsuarioModificacion = item.UsuarioModificacion,

                    });
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = listaStatusCuentumST;
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
            var responseApi = new ResponseAPI<StatusCuentumST>();
            var StatusCuentumST = new StatusCuentumST();



            try
            {
                var dbStatusCuentumST = await _dbcontent.StatusCuenta.FirstOrDefaultAsync(X => X.IdStatusCuenta == id);
                if (dbStatusCuentumST != null)
                {

                    StatusCuentumST.IdStatusCuentum = dbStatusCuentumST.IdStatusCuenta;
                    StatusCuentumST.Nombre = dbStatusCuentumST.Nombre;
                    StatusCuentumST.FechaCreacion = dbStatusCuentumST.FechaCreacion;
                    StatusCuentumST.UsuarioCreacion = dbStatusCuentumST.UsuarioCreacion;
                    StatusCuentumST.FechaModificacion = dbStatusCuentumST.FechaModificacion;
                    StatusCuentumST.UsuarioModificacion = dbStatusCuentumST.UsuarioModificacion;



                    responseApi.EsCorrecto = true;
                    responseApi.Valor = StatusCuentumST;

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

        public async Task<IActionResult> Guardar(StatusCuentumST status)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbStatusCuentumST = new StatusCuentum
                {
                    Nombre = status.Nombre,
                    FechaCreacion = status.FechaCreacion,
                    UsuarioCreacion = status.UsuarioCreacion,
                    FechaModificacion = status.FechaModificacion,
                    UsuarioModificacion = status.UsuarioModificacion,
                };

                _dbcontent.StatusCuenta.Add(dbStatusCuentumST);
                await _dbcontent.SaveChangesAsync();

                if (dbStatusCuentumST.IdStatusCuenta != 0)
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = dbStatusCuentumST.IdStatusCuenta;
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

        public async Task<IActionResult> Editar(StatusCuentumST status, int id)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbStatusCuentum = await _dbcontent.StatusCuenta.FirstOrDefaultAsync(e => e.IdStatusCuenta == id);


                if (dbStatusCuentum != null)
                {

                    dbStatusCuentum.Nombre = status.Nombre;
                    dbStatusCuentum.FechaCreacion = status.FechaCreacion;
                    dbStatusCuentum.UsuarioCreacion = status.UsuarioCreacion;
                    dbStatusCuentum.FechaModificacion = status.FechaModificacion;
                    dbStatusCuentum.UsuarioModificacion = status.UsuarioModificacion;


                    _dbcontent.StatusCuenta.Update(dbStatusCuentum);
                    await _dbcontent.SaveChangesAsync();

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = dbStatusCuentum.IdStatusCuenta;


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
                var dbStatusCuentum = await _dbcontent.StatusCuenta.FirstOrDefaultAsync(e => e.IdStatusCuenta == id);


                if (dbStatusCuentum != null)
                {



                    _dbcontent.StatusCuenta.Remove(dbStatusCuentum);
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
    