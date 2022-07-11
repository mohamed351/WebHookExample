namespace TravelAgentWeb.Dtos
{
    public class FlightDetailUpdateDTO
    {
        public string Publisher { get; set; }
        public string Secret { get; set; }

        public string FligthCode { get; set; }


        public decimal OldPrice { get; set; }


        public decimal NewPrice { get; set; }

        public string WebhookType { get; set; }
    }
}
