using Microsoft.EntityFrameworkCore;
using System.Net;

namespace FoodDelivery.Models
{
    public class Context2 : DbContext
    {
        public Context2(DbContextOptions<Context2> options) : base(options)
        {
        }

        public DbSet<Address> Address { get; set; }

        public DbSet<House> House { get; set; }

        public DbSet<Hierarchy> Hierarchy { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}

