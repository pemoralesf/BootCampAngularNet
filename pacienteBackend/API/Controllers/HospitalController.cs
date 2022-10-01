using System.Net;
using AutoMapper;
using Core.Dto;
using Core.Entidades;
using Infraestructura.Data;
using Infraestructura.Data.IRepositorio;
using Infraestructura.Data.Repositorio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HospitalController : ControllerBase
    {
        //private readonly ApplicationDbContext _db;

        private ResponseDto _response;
        private readonly ILogger<HospitalController> _logger;
        private readonly IMapper _mapper;
        private readonly IUnidadTrabajo _unidadTrabajo;

        public HospitalController(IUnidadTrabajo unidadTrabajo, ILogger<HospitalController> logger,
                                    IMapper mapper)
        {
            _unidadTrabajo = unidadTrabajo;
            _mapper = mapper;
            _logger = logger;

            _response = new ResponseDto();

        }

        [HttpGet]

        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Hospital>>> GetHospital()
        {
            _logger.LogInformation("Listado de   Hospitales del  EndPoint");
            var lista = await _unidadTrabajo.Hospital.ObtenerTodos();
            _response.Resultado = lista;
            _response.Mensaje = "Listado de Hospital";
            _response.StatusCode = HttpStatusCode.OK;

            return Ok(_response);
        }

        [HttpGet("{id}", Name = "GetHospital")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Hospital>> GetHospital(int id)
        {

            if (id == 0)
            {
                _logger.LogError("Debe de  enviar  el  id del  Hospital");
                _response.Mensaje = "Debe  de  enviar  el  ID";
                _response.IsExitoso = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }



            var hosp = await _unidadTrabajo.Hospital.ObtenerPrimero(c=>c.Id==id);

            if (hosp == null)
            {
                _logger.LogError("Hospital  no  Existe");
                _response.Mensaje = "El  Hospital  no  existe";
                _response.IsExitoso = false;
                _response.StatusCode = HttpStatusCode.NotFound;
                return NotFound(_response);
            }
            _response.Resultado = hosp;
            _response.Mensaje = "Datos  del  Hospital  " + hosp.Id;
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Hospital>> PostHospital([FromBody] HospitalDto hospitalDto)
        {
            if (hospitalDto == null)
            {
                _response.Mensaje = "Informacion  incorrecta";
                _response.IsExitoso = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var hospitalExiste = await  _unidadTrabajo.Hospital.ObtenerPrimero(h => h.NombreHospital.ToLower() == hospitalDto.NombreHospital.ToLower());

            if (hospitalExiste != null)
            {
                //ModelState.AddModelError("NombreDupliocado", "El nombre del  Hospital ya Existe");
                _response.IsExitoso = false;
                _response.Mensaje = "El nombre del  Hospital ya Existe";
                _response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            Hospital hospital = _mapper.Map<Hospital>(hospitalDto);


            await _unidadTrabajo.Hospital.Agregar(hospital);
            await _unidadTrabajo.Guardar();
            _response.IsExitoso=true;
            _response.Mensaje="Hospital  guardado  con  Exito";
            _response.StatusCode=HttpStatusCode.Created;
            return CreatedAtRoute("GetHospital", new { id = hospital.Id }, hospital);

        }

        [HttpPut("{id}")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult> PutHospital(int id, [FromBody] HospitalDto hospitalDto)
        {
            if (id != hospitalDto.Id)
            {
                _response.IsExitoso=false;
                _response.Mensaje="Id  de Hospital no coincide";
                _response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);

            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var hospitalExiste = await  _unidadTrabajo.Hospital.ObtenerPrimero(
                                c => c.NombreHospital.ToLower() == hospitalDto.NombreHospital.ToLower()
                                && c.Id != hospitalDto.Id);

            if (hospitalExiste != null)
            {
                //ModelState.AddModelError("NombreDuplicado", "Nombre  del Hospitalk ya  Existe");
                _response.IsExitoso=false;
                _response.Mensaje="Nombre del Hospital ya  existe";
                return BadRequest(_response);
            }

            Hospital hospital = _mapper.Map<Hospital>(hospitalDto);
           _unidadTrabajo.Hospital.Actualizar(hospital);
            await _unidadTrabajo.Guardar();
            _response.IsExitoso=true;
            _response.Mensaje="Hospital  Actualizado";
            _response.StatusCode=HttpStatusCode.OK;
            return Ok(_response);

        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult> DeleteHospital(int id)
        {
            var hospital = await _unidadTrabajo.Hospital.ObtenerPrimero(c=>c.Id==id);
            if (hospital == null)
            {
                _response.IsExitoso=false;
                _response.Mensaje="Hospital No existe";
                _response.StatusCode=HttpStatusCode.NotFound;
                return NotFound(_response);
            }
           _unidadTrabajo.Hospital.Remover(hospital);
            await _unidadTrabajo.Guardar();
            _response.IsExitoso=true;
            _response.Mensaje="Hospital  Eliminado";
            _response.StatusCode=HttpStatusCode.NoContent;
            return Ok(_response);

        }


    }
}