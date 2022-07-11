using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineSendAgent.App
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> contextOptions)
            :base(contextOptions)
        {

        }

        public DbSet<WebHookSubscription> WebHookSubscriptions { get; set; }
        public DbSet<FlightDetail> FlightDetails { get; set; }
    }

    public class FlightDetail
    {
        public int Id { get; set; }

        [Required]
        public string FlightCode { get; set; }

        [Required]
        [Column(TypeName = "decimal(9,5)")]
        public decimal Price { get; set; }
    }

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

    public class FlightDetailUpdateDTO
    {
        public string Url { get; set; }
        public string Publisher { get; set; }
        public string Secret { get; set; }

        public string FligthCode { get; set; }


        public decimal OldPrice { get; set; }


        public decimal NewPrice { get; set; }

        public string WebhookType { get; set; }
    }
}
