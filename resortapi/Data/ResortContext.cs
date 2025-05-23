﻿using Microsoft.EntityFrameworkCore;
using resortlibrary.Models;

namespace resortapi.Data
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

            modelBuilder.Entity<AccomodationType>()
                .Property(a => a.BasePrice)
                .HasPrecision(10, 2);

            modelBuilder.Entity<AdditionalOption>()
                .Property(o => o.Price)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Booking>()
                .Property(b => b.AmountPaid)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Booking>()
                .Property(b => b.Cost)
                .HasPrecision(10, 2);

            ResortContextSeed.Seed(modelBuilder);
        }

    }
}
