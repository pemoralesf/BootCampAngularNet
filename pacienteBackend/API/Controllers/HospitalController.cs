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

        public HospitalController(ApplicationDbContext db)
        {
            _db = db;

            _response = new ResponseDto();

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Hospital>>> GetHospital()
        {
            var lista = await _db.TbHospital.ToListAsync();

            _response.Resultado = lista;
            _response.Mensaje = "Listado de Hospital";
            

            return Ok(_response);
        }

         [HttpGet("{id}", Name = "GetHospital")]
        public async Task<ActionResult<Hospital>> GetHospital(int id)
        {
            var hosp = await _db.TbHospital.FindAsync(id);
            _response.Resultado = hosp;
            _response.Mensaje="Datos  del  Hospital  " + hosp.Id;
            return Ok(_response);
        }

        [HttpPost]
        public async Task<ActionResult<Hospital>> PostHospital([FromBody] Hospital hospital )
        {
            await _db.TbHospital.AddAsync(hospital);
            await _db.SaveChangesAsync();
            return CreatedAtRoute("GetHospital", new {id = hospital.Id}, hospital);

        }

        [HttpPut("{id}")]

        public async Task<ActionResult> PutHospital(int id, [FromBody] Hospital hospital)
        {
            if (id != hospital.Id)
            {
                return BadRequest("Id  de Hospital no coincide");
            }

            _db.Update(hospital);
            await _db.SaveChangesAsync();
            return Ok(hospital);

        }

        [HttpDelete ("{id}")]

        public async Task<ActionResult> DeleteHospital ( int id)
        {
            var hospital = await _db.TbHospital.FindAsync(id);
            if(hospital == null)
            {
                return NotFound();
            }
            _db.TbHospital.Remove(hospital);
            await _db.SaveChangesAsync();
            return NoContent();

        }


    }
}