using Feedback.Interfaces;
using Feedback.Models;
using Feedback.Models.DTO;
using Feedback.Repository;
using Feedback.Services;
using FeedbackAPI.Models.Context;
using Microsoft.EntityFrameworkCore;

namespace Feedback
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
            builder.Services.AddDbContext<FeedbackContext>(opts =>
            {
                opts.UseSqlServer(builder.Configuration.GetConnectionString("conn"));
            });

            builder.Services.AddScoped<IFeedbackRepo<HotelFeedback, HotelIdDTO>, FeedbackRepo>();
            builder.Services.AddScoped<IFeedbackService<FeedbackDTO, HotelIdDTO>, FeedbackService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}