using System;

namespace AirLineProject.Dtos
{
    public class NotificationMessageDTO
    {

        public NotificationMessageDTO()
        {
            Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }
        public string FligthCode { get; set; }


        public decimal OldPrice { get; set; }


        public decimal NewPrice { get; set; }

        public string WebhookType { get; set; }

    }
}
