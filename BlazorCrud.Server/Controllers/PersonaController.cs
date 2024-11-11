using BlazorCrud.Server.Models;
using BlazorCrud.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace BlazorCrud.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonaController : ControllerBase
    {
        private readonly ProyectoAnalisisContext _dbcontent;

        public PersonaController(ProyectoAnalisisContext dbcontent)
        {
            _dbcontent = dbcontent;
        }


        [HttpGet]
        [Route("Lista")]

        public async Task<IActionResult> Lista()
        {
            var responseApi = new ResponseAPI<List<PersonaP>>();
            var listaPersonaP = new List<PersonaP>();
            try
            {
                foreach (var item in await _dbcontent.Personas.ToListAsync())
                {
                    listaPersonaP.Add(new PersonaP
                    {
                        IdPersona = item.IdPersona,
                        Nombre = item.Nombre,
                        Apellido = item.Apellido,
                        FechaNacimiento = item.FechaNacimiento,
                        IdGenero = item.IdGenero,
                        Direccion = item.Direccion,
                        Telefono = item.Telefono,
                        CorreoElectronico = item.CorreoElectronico,
                        IdEstadoCivil = item.IdEstadoCivil,
                        FechaCreacion = item.FechaCreacion,
                        UsuarioCreacion = item.UsuarioCreacion,
                        FechaModificacion = item.FechaModificacion,
                        UsuarioModificacion = item.UsuarioModificacion,

                    });
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = listaPersonaP;
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
            var responseApi = new ResponseAPI<PersonaP>();
            var PersonaP = new PersonaP();

            try
            {
                var dbPersonaP = await _dbcontent.Personas.FirstOrDefaultAsync(X => X.IdPersona == id);
                if (dbPersonaP != null)
                {
                    PersonaP.IdPersona = dbPersonaP.IdPersona;
                    PersonaP.Nombre = dbPersonaP.Nombre;
                    PersonaP.Apellido = dbPersonaP.Apellido;
                    PersonaP.FechaNacimiento = dbPersonaP.FechaNacimiento;
                    PersonaP.IdGenero = dbPersonaP.IdGenero;
                    PersonaP.Direccion = dbPersonaP.Direccion;
                    PersonaP.Telefono = dbPersonaP.Telefono;
                    PersonaP.CorreoElectronico = dbPersonaP.CorreoElectronico;
                    PersonaP.IdEstadoCivil = dbPersonaP.IdEstadoCivil;
                    PersonaP.FechaCreacion = dbPersonaP.FechaCreacion;
                    PersonaP.UsuarioCreacion = dbPersonaP.UsuarioCreacion;
                    PersonaP.FechaModificacion = dbPersonaP.FechaModificacion;
                    PersonaP.UsuarioModificacion = dbPersonaP.UsuarioModificacion;


                    responseApi.EsCorrecto = true;
                    responseApi.Valor = PersonaP;

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

        public async Task<IActionResult> Guardar(PersonaP persona)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbPersona = new Persona
                {

                    Nombre = persona.Nombre,
                    Apellido = persona.Apellido,
                    FechaNacimiento = persona.FechaNacimiento,
                    IdGenero = persona.IdGenero,
                    Direccion = persona.Direccion,
                    Telefono = persona.Telefono,
                    CorreoElectronico = persona.CorreoElectronico,
                    IdEstadoCivil = persona.IdEstadoCivil,
                    FechaCreacion = persona.FechaCreacion,
                    UsuarioCreacion = persona.UsuarioCreacion,
                    FechaModificacion = persona.FechaModificacion,
                    UsuarioModificacion = persona.UsuarioModificacion,

                };

                _dbcontent.Personas.Add(dbPersona);
                await _dbcontent.SaveChangesAsync();

                if (dbPersona.IdPersona != 0)
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = dbPersona.IdPersona;
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

        public async Task<IActionResult> Editar(PersonaP persona, int id)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbPersona = await _dbcontent.Personas.FirstOrDefaultAsync(e => e.IdPersona == id);


                if (dbPersona != null)
                {

                    dbPersona.Nombre = persona.Nombre;
                    dbPersona.Apellido = persona.Apellido;
                    dbPersona.FechaNacimiento = persona.FechaNacimiento;
                    dbPersona.IdGenero = persona.IdGenero;
                    dbPersona.Direccion = persona.Direccion;
                    dbPersona.Telefono = persona.Telefono;
                    dbPersona.CorreoElectronico = persona.CorreoElectronico;
                    dbPersona.IdEstadoCivil = persona.IdEstadoCivil;
                    dbPersona.FechaCreacion = persona.FechaCreacion;
                    dbPersona.UsuarioCreacion = persona.UsuarioCreacion;
                    dbPersona.FechaModificacion = persona.FechaModificacion;
                    dbPersona.UsuarioModificacion = persona.UsuarioModificacion;



                    _dbcontent.Personas.Update(dbPersona);
                    await _dbcontent.SaveChangesAsync();

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = dbPersona.IdPersona;


                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensajes = "No  encontrado";
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
                var dbPersona = await _dbcontent.Personas.FirstOrDefaultAsync(e => e.IdPersona == id);


                if (dbPersona != null)
                {



                    _dbcontent.Personas.Remove(dbPersona);
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
