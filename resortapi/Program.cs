
using Microsoft.EntityFrameworkCore;
using restortlibrary.Data;
using restortlibrary.Models;
using restortlibrary.Repositories;
using Scalar.AspNetCore;

namespace resortapi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddDbContext<ResortContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly("resortapi")));
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            // Adding services for repo dependency injection
            builder.Services.AddScoped<IRepository<Customer>, CustomerRepo>();
            builder.Services.AddScoped<IRepository<Booking>, BookingRepo>();
            builder.Services.AddScoped<IRepository<Accomodation>, AccomodationRepo>();
            builder.Services.AddScoped<IRepository<AccomodationType>, AccomodationTypeRepo>();
            builder.Services.AddScoped<IRepository<PriceChanges>, PriceChangesRepo>();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
