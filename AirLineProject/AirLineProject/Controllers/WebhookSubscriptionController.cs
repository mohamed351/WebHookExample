using AirLineProject.Dtos;
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
    public class WebhookSubscriptionController : ControllerBase
    {
        private readonly AirlineDbContext context;
        private readonly IMapper mapper;

        public WebhookSubscriptionController(AirlineDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        [HttpGet("{secret}",Name = "GetWebHookSubscription")]
        public ActionResult<WebhookSubscriptionReadDto> GetWebHookSubscription(string secret)
        {
            if (string.IsNullOrWhiteSpace(secret))
            {
                return BadRequest("The Secret is not exist");
            }
          var subscription =   context.WebHookSubscriptions.FirstOrDefault(a => a.Secret == secret);
            if(subscription == null)
            {
                return NotFound("The ID is not found");
            }
            return mapper.Map<WebhookSubscriptionReadDto>(subscription);

        }
        [HttpPost]
        public ActionResult<WebhookSubscriptionReadDto> CreateSubscription(WebhookSubscriptionCreateDTO createDTO)
        {
           var subscription = context.WebHookSubscriptions.FirstOrDefault(a => a.WebhookUrl == createDTO.WebhookUrl);
            if(subscription == null)
            {
                var newSubscription = mapper.Map<WebHookSubscription>(createDTO);
                newSubscription.Secret = Guid.NewGuid().ToString();
                newSubscription.WebhookPublisher = "Unknown";
                context.WebHookSubscriptions.Add(newSubscription);
                context.SaveChanges();
                var readSubscriptoin = mapper.Map<WebhookSubscriptionReadDto>(newSubscription);
                return CreatedAtAction(nameof(GetWebHookSubscription), new { secret = newSubscription.Secret }, readSubscriptoin);
            }
            else
            {
                return NoContent();
            }

        } 
    }
}
