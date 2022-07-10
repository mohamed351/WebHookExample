using Microsoft.EntityFrameworkCore;

namespace AirLineProject.Models
{
    public class AirlineDbContext:DbContext
    {

        public AirlineDbContext(DbContextOptions<AirlineDbContext> options)
            :base(options)
        {

        }
        public DbSet<WebHookSubscription> WebHookSubscriptions { get; set; }
        public DbSet<FlightDetail> FlightDetails { get; set; }
    }
}
