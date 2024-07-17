using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;

namespace VideoGamesRental.Application;

[ExcludeFromCodeCoverage]
public static class ServicesRegistration
{
    public static void RegisterApplication(this IServiceCollection inServices)
    {
        inServices.AddMediatR(inConfig => inConfig.RegisterServicesFromAssembly(typeof(ServicesRegistration).Assembly));
    }
}