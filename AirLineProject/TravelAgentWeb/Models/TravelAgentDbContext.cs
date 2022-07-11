using Microsoft.EntityFrameworkCore;

namespace TravelAgentWeb.Models
{
    public class TravelAgentDbContext:DbContext
    {
        public TravelAgentDbContext(DbContextOptions<TravelAgentDbContext> contextOptions)
            :base(contextOptions)
        {

        }


        public DbSet<WebhookSecret> SubscriptionSecret { get; set; }
    }
}
