using Microsoft.EntityFrameworkCore;
using restortlibrary.Models;

namespace restortlibrary.Data
{
    public class ResortContext : DbContext
    {
        public ResortContext(DbContextOptions<ResortContext> options) : base(options)
        {
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Accessibility> Accessibilities { get; set; }
        public DbSet<Accomodation> Accommodations { get; set; }
        public DbSet<AccomodationType> AccomodationTypes { get; set; }
        public DbSet<AdditionalOption> AdditionalOptions { get; set; }
        public DbSet<PriceChanges> PriceChanges { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            ResortContextSeed.Seed(modelBuilder);
        }

    }
}
