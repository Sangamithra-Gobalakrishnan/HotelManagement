using Microsoft.EntityFrameworkCore;
using Reservation.Interfaces;
using Reservation.Models;
using Reservation.Models.Context;
using Reservation.Models.DTO;
using Reservation.Repository;
using Reservation.Services;

namespace Reservation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //User defined Services
            builder.Services.AddDbContext<BookingContext>(opts =>
            {
                opts.UseSqlServer(builder.Configuration.GetConnectionString("conn"));
            });

            builder.Services.AddCors(opts =>
            {
                opts.AddPolicy("HotelCORS", options =>
                {
                    options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
                });
            });

            builder.Services.AddScoped<IBookingService<BookingDTO, int>, BookingService>();
            builder.Services.AddScoped<IBookingRepo<Booking, int>, BookingRepo>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("HotelCORS");
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}