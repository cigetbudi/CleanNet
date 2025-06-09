
using CleanNet.Application.CatFacts.Commands;
using CleanNet.Application.Interfaces;
using CleanNet.Infra.Persistence;
using CleanNet.Infra.Services;

namespace CleanNet.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddHttpClient();
        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<GetAndSaveCatFactCommand>());

        builder.Services.AddScoped<ICatFactService, CatFactService>();
        builder.Services.AddScoped<IDatabaseService, DatabaseService>();

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
