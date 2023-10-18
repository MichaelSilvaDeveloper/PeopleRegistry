using Microsoft.EntityFrameworkCore;
using PeopleRegistry.Data;
using PeopleRegistry.Repositories;
using PeopleRegistry.Repositories.Interfaces;
using PeopleRegistry.Services;

namespace PeopleRegistry
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
                
            builder.Services.AddEntityFrameworkSqlServer()
                .AddDbContext<PeopleRegistryDBContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("DataBase")));
            builder.Services.AddScoped<IPersonRepository, PersonRepository>();
            builder.Services.AddSingleton<CpfService>();




            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}