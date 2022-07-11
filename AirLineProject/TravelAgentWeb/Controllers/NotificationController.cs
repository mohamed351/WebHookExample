using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using TravelAgentWeb.Dtos;
using TravelAgentWeb.Models;

namespace TravelAgentWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly TravelAgentDbContext context;

        public NotificationController(TravelAgentDbContext context)
        {
            this.context = context;
        }
        [HttpPost]
        public ActionResult FlightChange(FlightDetailUpdateDTO flightDetailUpdateDTO)
        {
            Console.WriteLine($"Webhook recived from ${flightDetailUpdateDTO.Publisher}");
            var security = context.SubscriptionSecret.FirstOrDefault(a => a.Publisher == flightDetailUpdateDTO.Publisher && a.Secret == flightDetailUpdateDTO.Secret);
            if (security == null)
            {
                Console.WriteLine("Invalid secret");
                return Ok();
            }
            else
            {
                Console.WriteLine("Valid Webhook");
                Console.WriteLine($"Old Price {flightDetailUpdateDTO.OldPrice} New Price {flightDetailUpdateDTO.NewPrice} ");
                return Ok();
            }


        }
    }
}
