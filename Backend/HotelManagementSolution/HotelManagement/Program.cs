using HotelManagement.Interfaces;
using HotelManagement.Models;
using HotelManagement.Models.Context;
using HotelManagement.Models.DTO;
using HotelManagement.Repository;
using HotelManagement.Services;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement
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
            builder.Services.AddDbContext<HotelContext>(opts =>
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


            builder.Services.AddScoped<IHotelService<HotelDTO, int>, HotelService>();
            builder.Services.AddScoped<IHotelRepo<Hotel, int>, HotelRepo>();
            builder.Services.AddScoped<IAmenityService<AmenityOrImageDTO, HotelFunctionDTO>,AmenityService>();
            builder.Services.AddScoped<IAmenityRepo<Amenity, HotelFunctionDTO>, AmenityRepo>();
            builder.Services.AddScoped<IImageService<AmenityOrImageDTO, HotelFunctionDTO>, ImageService>();
            builder.Services.AddScoped<IImageRepo<Image, HotelFunctionDTO>, ImageRepo>();
            builder.Services.AddScoped<IRoomService<RoomDTO, RoomDeleteDTO, HotelFunctionDTO>, RoomService>();
            builder.Services.AddScoped<IRoomRepo<Room, RoomDeleteDTO, HotelFunctionDTO>, RoomRepo>();

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