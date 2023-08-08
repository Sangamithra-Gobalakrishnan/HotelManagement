using Microsoft.EntityFrameworkCore;
using UserAuthentication.Interfaces;
using UserAuthentication.Models;
using UserAuthentication.Models.Context;
using UserAuthentication.Models.DTO;
using UserAuthentication.Repository;
using UserAuthentication.Services;

namespace UserAuthentication
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
            builder.Services.AddDbContext<UserContext>(opts =>
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

            builder.Services.AddScoped<IAgentDTOUserAdapter, AgentDTOUserAdapter>();
            builder.Services.AddScoped<ITravellerDTOUserAdapter, TravellerDTOUserAdapter>();
            builder.Services.AddScoped<IAgentRepo<TravelAgent, int>, AgentRepo>();
            builder.Services.AddScoped<ITravellerRepo<Traveller,int>, TravellerRepo>();
            builder.Services.AddScoped<IUserRepo<User,int,string>,UserRepo>();
            builder.Services.AddScoped<IUserService<UserDTO, TravellerDTO, AgentDTO, StatusDTO>, UserService>();
            builder.Services.AddScoped<IFilterService<Traveller, TravelAgent, StatusDTO>, FilterService>();
            builder.Services.AddScoped<IGenerateToken<UserDTO>, GenerateTokenService>();

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