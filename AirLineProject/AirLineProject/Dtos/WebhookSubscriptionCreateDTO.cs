using System.ComponentModel.DataAnnotations;

namespace AirLineProject.Dtos
{
    public class WebhookSubscriptionCreateDTO
    {
        [Required]
        public string WebhookUrl { get; set; }
     
        [Required]
        public string WebhookType { get; set; }
      
    }
}
