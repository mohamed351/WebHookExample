using System.ComponentModel.DataAnnotations;

namespace AirLineProject.Models
{
    public class WebHookSubscription
    {
        public int Id { get; set; }
        [Required]
        public string WebhookUrl { get; set; }
        [Required]
        public string Secret { get; set; }
        [Required]
        public string WebhookType { get; set; }
        [Required]
        public string WebhookPublisher { get; set; }
    }
}
