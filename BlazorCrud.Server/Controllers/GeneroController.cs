using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


using BlazorCrud.Server.Models;
using BlazorCrud.Shared;
using Microsoft.EntityFrameworkCore;


namespace BlazorCrud.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneroController : ControllerBase
    {
        private readonly ProyectoAnalisisContext _dbcontent;

        public GeneroController(ProyectoAnalisisContext dbcontent)
        {
            _dbcontent = dbcontent;
        }


        [HttpGet]
        [Route("Lista")]

        public async Task<IActionResult> Lista()
        {
            var responseApi = new ResponseAPI<List<GeneroGR>>();
            var listaGeneroGR = new List<GeneroGR>();
            try
            {
                foreach (var item in await _dbcontent.Generos.ToListAsync())
                {
                    listaGeneroGR.Add(new GeneroGR
                    {
                        IdGenero = item.IdGenero,
                        Nombre = item.Nombre,
                        FechaCreacion = item.FechaCreacion,
                        UsuarioCreacion = item.UsuarioCreacion,
                        FechaModificacion = item.FechaModificacion,
                        UsuarioModificacion = item.UsuarioModificacion,

                    });
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = listaGeneroGR;
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
            var responseApi = new ResponseAPI<GeneroGR>();
            var GeneroGR = new GeneroGR();



            try
            {
                var dbGeneroGR = await _dbcontent.Generos.FirstOrDefaultAsync(X => X.IdGenero == id);
                if (dbGeneroGR != null)
                {

                    GeneroGR.IdGenero = dbGeneroGR.IdGenero;
                    GeneroGR.Nombre = dbGeneroGR.Nombre;
                    GeneroGR.FechaCreacion = dbGeneroGR.FechaCreacion;
                    GeneroGR.UsuarioCreacion = dbGeneroGR.UsuarioCreacion;
                    GeneroGR.FechaModificacion = dbGeneroGR.FechaModificacion;
                    GeneroGR.UsuarioModificacion = dbGeneroGR.UsuarioModificacion;


                    responseApi.EsCorrecto = true;
                    responseApi.Valor = GeneroGR;

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

        public async Task<IActionResult> Guardar(GeneroGR genero)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbGenero = new Genero
                {
                    IdGenero = genero.IdGenero,
                    Nombre = genero.Nombre,
                    FechaCreacion = genero.FechaCreacion,
                    UsuarioCreacion = genero.UsuarioCreacion,
                    FechaModificacion = genero.FechaModificacion,
                    UsuarioModificacion = genero.UsuarioModificacion,
                };

                _dbcontent.Generos.Add(dbGenero);
                await _dbcontent.SaveChangesAsync();

                if (dbGenero.IdGenero != 0)
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = dbGenero.IdGenero;
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

        public async Task<IActionResult> Editar(GeneroGR genero, int id)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbGenero = await _dbcontent.Generos.FirstOrDefaultAsync(e => e.IdGenero == id);


                if (dbGenero != null)
                {

                    dbGenero.Nombre = genero.Nombre;
                    dbGenero.FechaCreacion = genero.FechaCreacion;
                    dbGenero.UsuarioCreacion = genero.UsuarioCreacion;
                    dbGenero.FechaModificacion = genero.FechaModificacion;
                    dbGenero.UsuarioModificacion = genero.UsuarioModificacion;


                    _dbcontent.Generos.Update(dbGenero);
                    await _dbcontent.SaveChangesAsync();

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = dbGenero.IdGenero;


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
                var dbGenero = await _dbcontent.Generos.FirstOrDefaultAsync(e => e.IdGenero == id);


                if (dbGenero != null)
                {



                    _dbcontent.Generos.Remove(dbGenero);
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
