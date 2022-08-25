using AutoMapper;
using Core.Dto;
using Core.Entidades;
using Infraestructura.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HospitalController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        private ResponseDto _response;
        private readonly ILogger<HospitalController> _logger;
        private readonly IMapper _mapper;

        public HospitalController(ApplicationDbContext db, ILogger<HospitalController> logger,
                                    IMapper mapper)
        {
            _mapper = mapper;
            _logger = logger;
            _db = db;

            _response = new ResponseDto();

        }

        [HttpGet]

        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Hospital>>> GetHospital()
        {
            _logger.LogInformation("Listado de   Hospitales del  EndPoint");
            var lista = await _db.TbHospital.ToListAsync();
            _response.Resultado = lista;
            _response.Mensaje = "Listado de Hospital";


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
                _logger.LogError("Debe d e  enviar  eñ  id del  Hospital");
                _response.Mensaje = "Debe  de  enviar  el  ID";
                _response.IsExitoso = false;
                return BadRequest(_response);
            }



            var hosp = await _db.TbHospital.FindAsync(id);

            if (hosp == null)
            {
                _logger.LogError("Hospital  no  Existe");
                _response.Mensaje = "El  Hospital  no  existe";
                _response.IsExitoso = false;
                return NotFound(_response);
            }
            _response.Resultado = hosp;
            _response.Mensaje = "Datos  del  Hospital  " + hosp.Id;
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
                return BadRequest(_response);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var hospitalExiste = await _db.TbHospital.FirstOrDefaultAsync(
                                    h => h.NombreHospital.ToLower() == hospitalDto.NombreHospital.ToLower());

            if (hospitalExiste != null)
            {
                ModelState.AddModelError("NombreDupliocado", "El nombre del  Hospital ya Existe");
                return BadRequest(ModelState);
            }

            Hospital hospital = _mapper.Map<Hospital>(hospitalDto);


            await _db.TbHospital.AddAsync(hospital);
            await _db.SaveChangesAsync();
            return CreatedAtRoute("GetHospital", new { id = hospital.Id }, hospital);

        }

        [HttpPut("{id}")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult> PutHospital(int id, [FromBody] HospitalDto hospitalDto)
        {
            if (id != hospitalDto.Id)
            {
                return BadRequest("Id  de Hospital no coincide");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var hospitalExiste = await _db.TbHospital.FirstOrDefaultAsync(
                                c => c.NombreHospital.ToLower() == hospitalDto.NombreHospital.ToLower()
                                && c.Id != hospitalDto.Id);

            if (hospitalExiste != null)
            {
                ModelState.AddModelError("NombreDuplicado", "Nombre  del Hospitalk ya  Existe");
                return BadRequest(ModelState);
            }

            Hospital hospital = _mapper.Map<Hospital>(hospitalDto);
            _db.Update(hospital);
            await _db.SaveChangesAsync();
            return Ok(hospital);

        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult> DeleteHospital(int id)
        {
            var hospital = await _db.TbHospital.FindAsync(id);
            if (hospital == null)
            {
                return NotFound();
            }
            _db.TbHospital.Remove(hospital);
            await _db.SaveChangesAsync();
            return NoContent();

        }


    }
}