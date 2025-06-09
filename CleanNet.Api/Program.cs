
using CleanNet.Application.CatFacts.Commands;
using CleanNet.Application.Interfaces;
using CleanNet.Infra.Logging;
using CleanNet.Infra.Persistence;
using CleanNet.Infra.Services;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Serilog;

namespace CleanNet.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // serilog
        builder.Host.UseSerilog((ctx, config) =>
        {
            config.ReadFrom.Configuration(ctx.Configuration);
        });


        // OpenTelemetry Tracing
        builder.Services.AddOpenTelemetry()
    .WithTracing(t =>
    {
        var jaegerConfig = builder.Configuration.GetSection("Jaeger");
        var serviceName = jaegerConfig.GetValue<string>("ServiceName");

        t.SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(serviceName))
         .AddAspNetCoreInstrumentation()
         .AddHttpClientInstrumentation()
         .AddSource("CleanNet")
         .AddOtlpExporter(o =>
         {
             o.Endpoint = new Uri("http://localhost:4317");
         });
    });


        // Add services to the container.
        builder.Services.AddHttpClient();
        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<GetAndSaveCatFactCommand>());

        builder.Services.AddSingleton(typeof(IAppLogger<>), typeof(AppLogger<>));
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
