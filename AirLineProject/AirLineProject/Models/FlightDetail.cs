using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AirLineProject.Models
{
    public class FlightDetail
    {
        public int Id { get; set; }

        [Required]
        public string FlightCode { get; set; }

        [Required]
        [Column(TypeName ="decimal(9,5)")]
        public decimal Price { get; set; }
    }
}
