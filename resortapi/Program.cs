
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using restortlibrary.Data;
using restortlibrary.Models;
using restortlibrary.Repositories;
using restortlibrary.Factories;
using restortlibrary.Factories.IFactories;
using resortapi.Services;
using Scalar.AspNetCore;
using restortlibrary.Converters;
using restortlibrary.Models.DTOs;

namespace resortapi
{
    public class Program
    {
        public static void Main(string[] args)

        {
           
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddOpenApi();

            builder.Services.AddDbContext<ResortContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly("resortapi")));

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = builder.Configuration["AppSettings:Issuer"],
                        ValidateAudience = true,
                        ValidAudience = builder.Configuration["AppSettings:Audience"],
                        ValidateLifetime = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            System.Text.Encoding.UTF8.GetBytes(builder.Configuration["AppSettings:Token"]!)),
                        ValidateIssuerSigningKey = true
                    };
                });

            builder.Services.AddScoped<IAuthService, AuthService>();

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

            // Adding services for repo dependency injection
            builder.Services.AddScoped<IRepository<Customer>,CustomerRepo>();
            builder.Services.AddScoped<IRepository<Booking>, BookingRepo>();

            // Adding services for DTO conversion
            builder.Services.AddTransient<IConverter<Customer, CreateCustomerRequestDTO>, CustomerConverter>();

            // Adding services for Factories
            builder.Services.AddScoped<ICustomerFactory, CustomerFactory>();

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
