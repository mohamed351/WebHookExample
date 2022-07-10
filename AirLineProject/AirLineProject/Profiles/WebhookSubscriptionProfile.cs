using AirLineProject.Dtos;
using AirLineProject.Models;
using AutoMapper;

namespace AirLineProject.Profiles
{
    public class WebhookSubscriptionProfile:Profile
    {

        public WebhookSubscriptionProfile()
        {
            CreateMap<WebhookSubscriptionCreateDTO, WebHookSubscription>();
            CreateMap<WebHookSubscription, WebhookSubscriptionReadDto>();
        
        }
    }
}
