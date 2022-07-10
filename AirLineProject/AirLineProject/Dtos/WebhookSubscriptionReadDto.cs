namespace AirLineProject.Dtos
{
    public class WebhookSubscriptionReadDto
    {

        public int Id { get; set; }
    
        public string WebhookUrl { get; set; }
   
        public string Secret { get; set; }
    
        public string WebhookType { get; set; }
      
        public string WebhookPublisher { get; set; }
    }
}
