using Microsoft.EntityFrameworkCore;
using resortlibrary.Models;

public static class ResortContextSeed
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        SeedAccessibilities(modelBuilder);
        SeedAccomodationTypes(modelBuilder);
        SeedAccomodations(modelBuilder);
        SeedAdditionalOptions(modelBuilder);
        SeedCustomers(modelBuilder);
        SeedBookings(modelBuilder);
        SeedGuests(modelBuilder);
        SeedPriceChanges(modelBuilder);
        SeedUsers(modelBuilder);
        SeedAccomodationAccessibilityRelations(modelBuilder);
    }

    private static void SeedAccessibilities(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Accessibility>().HasData(
            new Accessibility { Id = 1, Name = "Rullstolsanpassad", Description = "Tillgänglig med rullstol" },
            new Accessibility { Id = 2, Name = "Hörselhjälpmedel", Description = "Utrustning för hörselskadade" }
        );
    }

    private static void SeedAccomodationTypes(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AccomodationType>().HasData(
            new AccomodationType { Id = 1, Name = "Enkelrum", Description = "Ett rum med en säng", BasePrice = 800 },
            new AccomodationType { Id = 2, Name = "Dubbelrum", Description = "Rum med två sängar", BasePrice = 1200 },
            new AccomodationType { Id = 3, Name = "Svit", Description = "Lyxsvit med utsikt", BasePrice = 2500 }
        );
    }

    private static void SeedAccomodations(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Accomodation>().HasData(
            new Accomodation { Id = 1, Name = "101A", MaxOccupancy = 1, AccomodationTypeId = 1 },
            new Accomodation { Id = 2, Name = "202B", MaxOccupancy = 2, AccomodationTypeId = 2 },
            new Accomodation { Id = 3, Name = "Penthouse 1", MaxOccupancy = 4, AccomodationTypeId = 3 }
        );
    }


    private static void SeedAdditionalOptions(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AdditionalOption>().HasData(
            new AdditionalOption { Id = 1, Name = "Frukost", Description = "Bufféfrukost", Price = 120 },
            new AdditionalOption { Id = 2, Name = "Sen utcheckning", Description = "Utcheckning kl 14", Price = 200 }
        );
    }

    private static void SeedCustomers(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>().HasData(
            new Customer { Id = 1, Type = "Privat", FirstName = "Anna", LastName = "Svensson", Email = "anna@example.com", Phone = "0701234567", PaymentMethod = "Kort" },
            new Customer { Id = 2, Type = "Företag", FirstName = "Lars", LastName = "Andersson", Email = "lars@firma.se", Phone = "0739876543", PaymentMethod = "Faktura" }
        );
    }

    private static void SeedBookings(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Booking>().HasData(
            new Booking
            {
                Id = 1,
                CheckIn = new DateTime(2025, 6, 10),
                CheckOut = new DateTime(2025, 6, 12),
                TimeOfBooking = new DateTime(2025, 5, 20, 12, 34, 56),
                Active = true,
                CancellationDate = null,
                Cost = 2640,
                AmountPaid = 2640,
                CustomerId = 1,
                AccomodationId = 2
            },
            new Booking
            {
                Id = 2,
                CheckIn = new DateTime(2025, 7, 1),
                CheckOut = new DateTime(2025, 7, 7),
                TimeOfBooking = new DateTime(2025, 5, 21, 9, 10, 0),
                Active = false,
                CancellationDate = new DateTime(2025, 5, 22),
                Cost = 5000,
                AmountPaid = 0,
                CustomerId = 2,
                AccomodationId = 3
            }
        );
    }

    private static void SeedGuests(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Guest>().HasData(
            new Guest { Id = 1, FirstName = "Anna", LastName = "Svensson", Age = 34, BookingId = 1 },
            new Guest { Id = 2, FirstName = "Maria", LastName = "Nilsson", Age = 28, BookingId = 1 },
            new Guest { Id = 3, FirstName = "Lars", LastName = "Andersson", Age = 45, BookingId = 2 },
            new Guest { Id = 4, FirstName = "Eva", LastName = "Karlsson", Age = 42, BookingId = 2 }
        );
    }

    private static void SeedPriceChanges(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PriceChanges>().HasData(
            new PriceChanges { Id = 1, PriceChange = 1, Type = "Weekend-tillägg" },
            new PriceChanges { Id = 2, PriceChange = 1, Type = "Kompis till chefen" }
        );
    }

    private static void SeedUsers(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasData(
            new User { Id = 1, Username = "admin", PasswordHash = "Adminadmin123#", Role = "Admin", RefreshToken = null, RefreshTokenExpiryTime = null },
            new User { Id = 2, Username = "reception", PasswordHash = "Reception123#", Role = "User", RefreshToken = null, RefreshTokenExpiryTime = null }
        );
    }

    public static void SeedAccomodationAccessibilityRelations(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Accomodation>()
            .HasMany(a => a.Accessibilities)
            .WithMany(ac => ac.Accomodations)
            .UsingEntity<Dictionary<string, object>>(
                "AccomodationAccessibility",
                j => j.HasOne<Accessibility>().WithMany().HasForeignKey("AccessibilityId"),
                j => j.HasOne<Accomodation>().WithMany().HasForeignKey("AccomodationId"))
            .HasData(
                new { AccomodationId = 1, AccessibilityId = 1 },
                new { AccomodationId = 2, AccessibilityId = 2 },
                new { AccomodationId = 3, AccessibilityId = 1 },
                new { AccomodationId = 3, AccessibilityId = 2 }
            );
    }


}
