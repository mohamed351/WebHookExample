using AirLineProject.Dtos;
using AirLineProject.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace AirLineProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightDetailController : ControllerBase
    {
        private readonly AirlineDbContext context;
        private readonly IMapper mapper;

        public FlightDetailController(AirlineDbContext context , IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        [HttpGet("{id}",Name = "GetDetails")]
        public ActionResult<FlightDetailReadDTO> GetDetails(int id)
        {
          var detail =   context.FlightDetails.Find(id);
            if(detail == null)
            {
                return NotFound();
            }
            return this.mapper.Map<FlightDetailReadDTO>(detail);

        }
        [HttpPost]
        public ActionResult<FlightDetailReadDTO> PostDetil(FlightDetailCreateDTO createDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
          var newDetail=  this.mapper.Map<FlightDetail>(createDTO);
            this.context.FlightDetails.Add(newDetail);
            this.context.SaveChanges();
            return CreatedAtAction(nameof(GetDetails), new { id = newDetail.Id }, createDTO);

        }
        [HttpPut("{id}")]
        public IActionResult ChangeFlight(int id, FlightDetailCreateDTO createDTO)
        {
            var flight = context.FlightDetails.FirstOrDefault(a => a.Id == id);
            if(flight == null)
            {
                return BadRequest();
            }
            mapper.Map(createDTO, flight);
            context.SaveChanges();
            return NoContent();

        }
    }
}
