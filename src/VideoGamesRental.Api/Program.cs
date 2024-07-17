using FluentValidation;
using Mapster;
using Serilog;
using VideoGamesRental.Application;
using VideoGamesRental.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

AddMappingConfigs();
AddValidators(builder);
ConfigureLogs(builder);
builder.Services.RegisterApplication();
builder.Services.RegisterInfrastructure(builder.Configuration);

builder.Services.AddControllers()
    .AddJsonOptions(inOptions => inOptions.JsonSerializerOptions.PropertyNamingPolicy = null);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

await EnsureDatabaseCreatedAsync(app);

app.Run();
return;

void AddMappingConfigs()
{
    var typeAdapterConfig = TypeAdapterConfig.GlobalSettings;
    typeAdapterConfig.Scan(typeof(VideoGamesRental.Application.ServicesRegistration).Assembly);
    typeAdapterConfig.Scan(typeof(VideoGamesRental.Infrastructure.ServicesRegistration).Assembly);
}

void AddValidators(IHostApplicationBuilder inBuilder)
{
    inBuilder.Services.AddValidatorsFromAssembly(typeof(VideoGamesRental.Application.ServicesRegistration).Assembly);
    ValidatorOptions.Global.DefaultRuleLevelCascadeMode = CascadeMode.Stop;
    ValidatorOptions.Global.DefaultClassLevelCascadeMode = CascadeMode.Stop;
}

async Task EnsureDatabaseCreatedAsync(IHost inWebApplication)
{
    var context = inWebApplication.Services.CreateScope().ServiceProvider.GetRequiredService<ApplicationContext>();
#if DEBUG
    await context.Database.EnsureCreatedAsync();
#else
    await context.Database.MigrateAsync();
#endif
}

static void ConfigureLogs(IHostApplicationBuilder inBuilder)
{
    const string logFormat = "[{Timestamp:yyyy-MM-dd HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}";
    
    var logger = new LoggerConfiguration()
        .WriteTo.Console(outputTemplate: logFormat)
        .WriteTo.File("c:/logs/video-games-rental.txt", rollingInterval: RollingInterval.Day, outputTemplate: logFormat)
        .CreateLogger();
    
    inBuilder.Logging.ClearProviders();
    inBuilder.Logging.AddSerilog(logger);
}

// ReSharper disable All - used in AcceptanceContext
#pragma warning disable CA1050
namespace VideoGamesRental.Api
{
    public partial class Program
    {
    }
}
#pragma warning restore CA1050