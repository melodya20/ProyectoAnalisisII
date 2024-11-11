using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using BlazorCrud.Server.Models;
using BlazorCrud.Shared;
using Microsoft.EntityFrameworkCore;

namespace BlazorCrud.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioPreguntaController : ControllerBase
    {
        private readonly ProyectoAnalisisContext _dbcontext;

        public UsuarioPreguntaController(ProyectoAnalisisContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

    
        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var responseApi = new ResponseAPI<List<UsuarioPreguntumUP>>();
            var listaUsuarioPreguntas = new List<UsuarioPreguntumUP>();
            try
            {
                var preguntas = await _dbcontext.UsuarioPregunta
                    .Include(d => d.IdUsuarioNavigation)
                    .ToListAsync();

                foreach (var item in preguntas)
                {
                    listaUsuarioPreguntas.Add(new UsuarioPreguntumUP
                    {
                        IdPregunta = item.IdPregunta,
                        IdUsuario = item.IdUsuario,
                        Pregunta = item.Pregunta,
                        Respuesta = item.Respuesta,
                        OrdenPregunta = item.OrdenPregunta,
                        FechaCreacion = item.FechaCreacion,
                        UsuarioCreacion = item.UsuarioCreacion,
                        FechaModificacion = item.FechaModificacion,
                        UsuarioModificacion = item.UsuarioModificacion
                    });
                }

                responseApi.EsCorrecto = true;
                responseApi.Valor = listaUsuarioPreguntas;
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
            var responseApi = new ResponseAPI<UsuarioPreguntumUP>();
            var UsuarioPreguntumUP = new UsuarioPreguntumUP();

            try
            {
                var dbUsuarioPreguntumUP = await _dbcontext.UsuarioPregunta.FirstOrDefaultAsync(X => X.IdPregunta == id);
                if (dbUsuarioPreguntumUP != null)
                {

                    UsuarioPreguntumUP.IdPregunta = dbUsuarioPreguntumUP.IdPregunta;
                    UsuarioPreguntumUP.IdUsuario = dbUsuarioPreguntumUP.IdUsuario;
                    UsuarioPreguntumUP.Pregunta = dbUsuarioPreguntumUP.Pregunta;
                    UsuarioPreguntumUP.Respuesta = dbUsuarioPreguntumUP.Respuesta;
                    UsuarioPreguntumUP.OrdenPregunta = dbUsuarioPreguntumUP.OrdenPregunta;
                    UsuarioPreguntumUP.FechaCreacion = dbUsuarioPreguntumUP.FechaCreacion;
                    UsuarioPreguntumUP.UsuarioCreacion = dbUsuarioPreguntumUP.UsuarioCreacion;
                    UsuarioPreguntumUP.FechaModificacion = dbUsuarioPreguntumUP.FechaModificacion;
                    UsuarioPreguntumUP.UsuarioModificacion = dbUsuarioPreguntumUP.UsuarioModificacion;

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = UsuarioPreguntumUP;

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
        public async Task<IActionResult> Guardar(UsuarioPreguntumUP usuarioPregunta)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbPregunta = new UsuarioPreguntum
                {
                    IdUsuario = usuarioPregunta.IdUsuario,
                    Pregunta = usuarioPregunta.Pregunta,
                    Respuesta = usuarioPregunta.Respuesta,
                    OrdenPregunta = usuarioPregunta.OrdenPregunta,
                    FechaCreacion = usuarioPregunta.FechaCreacion,
                    UsuarioCreacion = usuarioPregunta.UsuarioCreacion,
                    FechaModificacion = usuarioPregunta.FechaModificacion,
                    UsuarioModificacion = usuarioPregunta.UsuarioModificacion
                };

                _dbcontext.UsuarioPregunta.Add(dbPregunta);
                await _dbcontext.SaveChangesAsync();

                if (dbPregunta.IdPregunta != 0)
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = dbPregunta.IdPregunta;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensajes = "No se pudo guardar la pregunta";
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
        public async Task<IActionResult> Editar(UsuarioPreguntumUP usuarioPregunta, int id)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbPregunta = await _dbcontext.UsuarioPregunta.FirstOrDefaultAsync(e => e.IdPregunta == id);

                if (dbPregunta != null)
                {
                    dbPregunta.Pregunta = usuarioPregunta.Pregunta;
                    dbPregunta.Respuesta = usuarioPregunta.Respuesta;
                    dbPregunta.OrdenPregunta = usuarioPregunta.OrdenPregunta;
                    dbPregunta.FechaModificacion = usuarioPregunta.FechaModificacion;
                    dbPregunta.UsuarioModificacion = usuarioPregunta.UsuarioModificacion;

                    _dbcontext.UsuarioPregunta.Update(dbPregunta);
                    await _dbcontext.SaveChangesAsync();

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = dbPregunta.IdPregunta;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensajes = "Pregunta no encontrada";
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
                var dbPregunta = await _dbcontext.UsuarioPregunta.FirstOrDefaultAsync(e => e.IdPregunta == id);

                if (dbPregunta != null)
                {
                    _dbcontext.UsuarioPregunta.Remove(dbPregunta);
                    await _dbcontext.SaveChangesAsync();

                    responseApi.EsCorrecto = true;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensajes = "Pregunta no encontrada";
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
