using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VideoGamesRental.Application.VideoGames.Persistence;
using VideoGamesRental.Infrastructure.VideoGames;

namespace VideoGamesRental.Infrastructure;

[ExcludeFromCodeCoverage]
public static class ServicesRegistration
{
    public static void RegisterInfrastructure(this IServiceCollection inServices, IConfiguration inConfiguration)
    {
        AddRepositories(inServices);

        inServices.AddDbContextFactory<ApplicationContext>(inOptions =>
        {
            inOptions.UseSqlServer(inConfiguration.GetConnectionString("DefaultConnection"));
#if DEBUG
            inOptions.EnableSensitiveDataLogging();
#endif
        });
    }

    private static void AddRepositories(IServiceCollection inServices)
    {
        inServices.AddScoped<IVideoGamesRepository, VideoGamesRepository>();
    }
}