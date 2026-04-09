using Microsoft.EntityFrameworkCore;
using NoteApp.DbContext;
using NoteApp.Repository;
using NoteApp.Service;

namespace NoteApp;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")?? throw new InvalidOperationException("Connection string"
            + "'DefaultConnection' not found.");;
        // Add services to the container.
        builder.Services.AddDbContext<NoteAppDbContext>(options => options.UseNpgsql(connectionString));
        builder.Services.AddControllers();
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();
        //Register Repository and service
        builder.Services.AddScoped<INoteAppRepository, NoteAppRepository>();
        builder.Services.AddScoped<INoteAppService, NoteAppService>();
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}