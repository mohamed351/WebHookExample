using System.ComponentModel.DataAnnotations;

namespace AirLineProject.Dtos
{
    public class FlightDetailCreateDTO
    {


        [Required]
        public string FlightCode { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}
