
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using resortapi.Converters;
using resortapi.Data;
using resortapi.Repositories;
using resortapi.Services;
using resortdtos;
using resortlibrary.Builders;
using resortlibrary.Models;
using Scalar.AspNetCore;

namespace resortapi
{
    public class Program
    {
        public static void Main(string[] args)

        {

            var builder = WebApplication.CreateBuilder(args);

            // Configure CORS to allow requests from the frontend

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend", policy =>
                {
                    policy.WithOrigins("http://localhost:5173")
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials();
                });
            });

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddOpenApi();

            builder.Services.AddDbContext<ResortContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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

            // JSON serialization options for reference handling
            //builder.Services.AddControllers()
            //    .AddJsonOptions(options =>
            //    {
            //        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
            //    });


            builder.Services.AddScoped<IAuthService, AuthService>();

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

            // Adding services for repo dependency injection
            builder.Services.AddScoped<IRepository<Customer>, CustomerRepo>();
            builder.Services.AddScoped<IRepository<Booking>, BookingRepo>();
            builder.Services.AddScoped<IRepository<Accomodation>, AccomodationRepo>();
            builder.Services.AddScoped<IRepository<AdditionalOption>, AdditionalOptionsRepo>();
            builder.Services.AddScoped<IRepository<AccomodationType>, AccomodationTypeRepo>();
            builder.Services.AddScoped<CustomerBuilder>();
            builder.Services.AddScoped<AccomodationRepo>();

            // Adding services for DTO conversion
            builder.Services.AddScoped<IBookingConverter, BookingConverter>();
            builder.Services.AddScoped<IConverter<Guest, GuestDto>, GuestConverter>();
            builder.Services.AddTransient<IConverter<Customer, CreateCustomerRequestDTO>, CustomerConverter>();

            // Adding services for services
            builder.Services.AddScoped<ICustomerService, CustomerService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();
            }

            app.UseHttpsRedirection();

            app.UseCors("AllowFrontend");

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
