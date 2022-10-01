using System.Net;
using AutoMapper;
using Core.Dto;
using Core.Entidades;
using Infraestructura.Data;
using Infraestructura.Data.IRepositorio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacienteController : ControllerBase
    {


        private ResponseDto _response;
        private readonly ILogger<PacienteController> _logger;
        private readonly IMapper _mapper;
        private readonly IUnidadTrabajo _unidadTrabajo;

        public PacienteController(IUnidadTrabajo unidadTrabajo, ILogger<PacienteController> logger,
                                    IMapper mapper)
        {
            _unidadTrabajo = unidadTrabajo;
            _mapper = mapper;
            _logger = logger;

            _response = new ResponseDto();

        }

        [HttpGet]

        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PacienteReadyDto>>> GetPaciente()
        {
            _logger.LogInformation("Listado de  Pacientes del  EndPoint");
            var lista = await  _unidadTrabajo.Paciente.ObtenerTodos(incluirPropiedades: "Hospital");

            _response.Resultado = _mapper.Map<IEnumerable<Paciente>, IEnumerable<PacienteReadyDto>>(lista);
            _response.Mensaje = "Listado de Pacientes";
            _response.StatusCode = HttpStatusCode.OK;

            return Ok(_response);
        }

        [HttpGet("{id}", Name = "GetPaciente")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Paciente>> GetPaciente(int id)
        {

            if (id == 0)
            {
                _logger.LogError("Debe d e  enviar  eñ  id del  Paciente");
                _response.Mensaje = "Debe  de  enviar  el  ID";
                _response.IsExitoso = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }



            var pac = await _unidadTrabajo.Paciente.ObtenerPrimero(e => e.Id==id, incluirPropiedades:"Hospital");

            if (pac == null)
            {
                _logger.LogError("Paciente  no  Existe");
                _response.Mensaje = "El  Paciente  no  existe";
                _response.IsExitoso = false;
                _response.StatusCode =  HttpStatusCode.NotFound;
                return NotFound(_response);
            }
            _response.Resultado = _mapper.Map<Paciente, PacienteReadyDto>(pac);
            _response.Mensaje = "Datos  del  Paciente  " + pac.Id;
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        }

        [HttpGet]
        [Route("PacientesPorHospital/{hospitalId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PacienteReadyDto>>> GetPacientePorHospital(int hospitalId)
        {
            _logger.LogInformation("Listado de  Pacientes por  Hospital");
            var lista = await _unidadTrabajo.Paciente.ObtenerTodos(e => e.HospitalId == hospitalId,incluirPropiedades: "Hospital");
            _response.Resultado = _mapper.Map<IEnumerable<Paciente>, IEnumerable<PacienteReadyDto>>(lista);
            _response.IsExitoso = true;
            _response.Mensaje = "Listado  de Pacients por Hospital";
            return Ok(Response);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Paciente>> PostPaciente([FromBody] PacienteUpsertDto pacienteDto)
        {
            if (pacienteDto == null)
            {
                _response.Mensaje = "Informacion  incorrecta del  Paciente";
                _response.IsExitoso = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pacienteExiste = await _unidadTrabajo.Paciente.ObtenerPrimero(
                                    p => p.Apellidos.ToLower() == pacienteDto.Apellidos.ToLower() &&
                                    p.Nombres.ToLower() == pacienteDto.Nombres.ToLower());

            if (pacienteExiste != null)
            {
                //ModelState.AddModelError("NombreDupliocado", "El nombre del  paciente ya Existe");
                _response.IsExitoso = false;
                _response.Mensaje= "El nombre del  paciente ya Existe";
                _response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            Paciente paciente = _mapper.Map<Paciente>(pacienteDto);


            await _unidadTrabajo.Paciente.Agregar(paciente);
            await _unidadTrabajo.Guardar();
            _response.IsExitoso= true;
            _response.Mensaje="Compañia guardada con  exito";
            _response.StatusCode=HttpStatusCode.Created;
            return CreatedAtRoute("GetPaciente", new { id = paciente.Id }, paciente);

        }

        [HttpPut("{id}")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult> PutPaciente(int id, [FromBody] PacienteUpsertDto pacienteDto)
        {
            if (id != pacienteDto.Id)
            {
                _response.IsExitoso=false;
                _response.Mensaje="Id  del  Paciente no coincide";
                _response.StatusCode=HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pacienteExiste = await _unidadTrabajo.Paciente.ObtenerPrimero(
                                p => p.Apellidos.ToLower() == pacienteDto.Apellidos.ToLower() &&
                                p.Nombres.ToLower() == pacienteDto.Nombres.ToLower()
                                && p.Id != pacienteDto.Id);

            if (pacienteExiste != null)
            {
                //ModelState.AddModelError("NombreDuplicado", "Nombre  del Paciente ya  Existe");
                _response.IsExitoso=false;
                _response.Mensaje="Nombre  del Paciente ya  Existe";
                return BadRequest(_response);
            }

            Paciente paciente = _mapper.Map<Paciente>(pacienteDto);
            _unidadTrabajo.Paciente.Actualizar(paciente);
            await _unidadTrabajo.Guardar();
            _response.IsExitoso=true;
            _response.Mensaje="Compañia Actualizada";
            _response.StatusCode= HttpStatusCode.OK;
            return Ok(_response);

        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult> DeletePaciente(int id)
        {
            var paciente = await _unidadTrabajo.Paciente.ObtenerPrimero(p => p.Id==id);
            if (paciente == null)
            {
                _response.IsExitoso=false;
                _response.Mensaje="Paciente ya existe";
                _response.StatusCode= HttpStatusCode.NotFound;
                return NotFound(_response);
            }
           _unidadTrabajo.Paciente.Remover(paciente);
            await _unidadTrabajo.Guardar();
            _response.IsExitoso=true;
            _response.Mensaje="Paciente Eliminado";
            _response.StatusCode=HttpStatusCode.NoContent;
            return  Ok(_response);

        }


    }
}