using BlazorCrud.Server.Models;
using BlazorCrud.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorCrud.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadoCivilController : ControllerBase
    {
        private readonly ProyectoAnalisisContext _dbcontent;

        public EstadoCivilController(ProyectoAnalisisContext dbcontent)
        {
            _dbcontent = dbcontent;
        }


        [HttpGet]
        [Route("Lista")]

        public async Task<IActionResult> Lista()
        {
            var responseApi = new ResponseAPI<List<EstadoCivilEC>>();
            var listaEstadoCivilEC = new List<EstadoCivilEC>();
            try
            {
                foreach (var item in await _dbcontent.EstadoCivils.ToListAsync())
                {
                    listaEstadoCivilEC.Add(new EstadoCivilEC
                    {
                        IdEstadoCivil = item.IdEstadoCivil,
                        Nombre = item.Nombre,
                        FechaCreacion = item.FechaCreacion,
                        UsuarioCreacion = item.UsuarioCreacion,
                        FechaModificacion = item.FechaModificacion,
                        UsuarioModificacion = item.UsuarioModificacion,

                    });
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = listaEstadoCivilEC;
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
            var responseApi = new ResponseAPI<EstadoCivilEC>();
            var EstadoCivilEC = new EstadoCivilEC();



            try
            {
                var dbEstadoCivilEC = await _dbcontent.EstadoCivils.FirstOrDefaultAsync(X => X.IdEstadoCivil == id);
                if (dbEstadoCivilEC != null)
                {

                    EstadoCivilEC.IdEstadoCivil = dbEstadoCivilEC.IdEstadoCivil;
                    EstadoCivilEC.Nombre = dbEstadoCivilEC.Nombre;
                    EstadoCivilEC.FechaCreacion = dbEstadoCivilEC.FechaCreacion;
                    EstadoCivilEC.UsuarioCreacion = dbEstadoCivilEC.UsuarioCreacion;
                    EstadoCivilEC.FechaModificacion = dbEstadoCivilEC.FechaModificacion;
                    EstadoCivilEC.UsuarioModificacion = dbEstadoCivilEC.UsuarioModificacion;


                    responseApi.EsCorrecto = true;
                    responseApi.Valor = EstadoCivilEC;

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

        public async Task<IActionResult> Guardar(EstadoCivilEC estadocivil)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbEstadoCivil = new EstadoCivil
                {
                    IdEstadoCivil = estadocivil.IdEstadoCivil,
                    Nombre = estadocivil.Nombre,
                    FechaCreacion = estadocivil.FechaCreacion,
                    UsuarioCreacion = estadocivil.UsuarioCreacion,
                    FechaModificacion = estadocivil.FechaModificacion,
                    UsuarioModificacion = estadocivil.UsuarioModificacion,
                };

                _dbcontent.EstadoCivils.Add(dbEstadoCivil);
                await _dbcontent.SaveChangesAsync();

                if (dbEstadoCivil.IdEstadoCivil != 0)
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = dbEstadoCivil.IdEstadoCivil;
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

        public async Task<IActionResult> Editar(EstadoCivilEC estadocivil, int id)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbEstadoCivil = await _dbcontent.EstadoCivils.FirstOrDefaultAsync(e => e.IdEstadoCivil == id);


                if (dbEstadoCivil != null)
                {

                    dbEstadoCivil.Nombre = estadocivil.Nombre;
                    dbEstadoCivil.FechaCreacion = estadocivil.FechaCreacion;
                    dbEstadoCivil.UsuarioCreacion = estadocivil.UsuarioCreacion;
                    dbEstadoCivil.FechaModificacion = estadocivil.FechaModificacion;
                    dbEstadoCivil.UsuarioModificacion = estadocivil.UsuarioModificacion;


                    _dbcontent.EstadoCivils.Update(dbEstadoCivil);
                    await _dbcontent.SaveChangesAsync();

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = dbEstadoCivil.IdEstadoCivil;


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
                var dbEstadoCivil = await _dbcontent.EstadoCivils.FirstOrDefaultAsync(e => e.IdEstadoCivil == id);


                if (dbEstadoCivil != null)
                {



                    _dbcontent.EstadoCivils.Remove(dbEstadoCivil);
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
    