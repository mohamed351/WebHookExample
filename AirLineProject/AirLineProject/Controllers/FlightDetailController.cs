using AirLineProject.Dtos;
using AirLineProject.MessageBus;
using AirLineProject.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace AirLineProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightDetailController : ControllerBase
    {
        private readonly AirlineDbContext context;
        private readonly IMapper mapper;
        private readonly IMessageBusClient client;

        public FlightDetailController(AirlineDbContext context , IMapper mapper , IMessageBusClient client)
        {
            this.context = context;
            this.mapper = mapper;
            this.client = client;
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
            return CreatedAtAction(nameof(GetDetails), new { id = newDetail.Id }, newDetail);

        }
        [HttpPut("{id}")]
        public IActionResult ChangeFlight(int id, FlightDetailCreateDTO createDTO)
        {
            var flight = context.FlightDetails.FirstOrDefault(a => a.Id == id);
            if(flight == null)
            {
                return BadRequest();
            }
            decimal oldPrice = flight.Price;

            mapper.Map(createDTO, flight);
            try
            {
                context.SaveChanges();
                if(oldPrice != flight.Price)
                {
                    Console.WriteLine("Price Changed ");
                    var message = new NotificationMessageDTO()
                    {
                        WebhookType ="price change",
                        FligthCode = flight.FlightCode,
                        OldPrice = oldPrice,
                        NewPrice = flight.Price,
                      

                    };
                    client.SendMessage(message);
                }

            }
            catch (System.Exception ex)
            {

                return BadRequest(ex.Message);
            }
          
            return NoContent();

        }
    }
}
