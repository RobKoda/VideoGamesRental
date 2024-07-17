using System.Reflection;
using AutoFixture;
using AutoFixture.Kernel;
using VideoGamesRental.Tests.Core;

namespace VideoGamesRental.Infrastructure.Tests.Core;

public static class FixtureBuilder
{
    public static IFixture GetFixture()
    {
        var fixture = new Fixture().Customize(new DateAndTimeOnlyCustomization());
        AddCustomBuilders(fixture);
        return fixture;
    }

    private static void AddCustomBuilders(IFixture inFixture)
    {
        var assembly = Assembly.GetExecutingAssembly();

        foreach (var type in assembly.GetTypes())
        {
            if (!typeof(ISpecimenBuilder).IsAssignableFrom(type) || type.IsInterface || type.IsAbstract) continue;
            
            var builderInstance = Activator.CreateInstance(type, inFixture);
            inFixture.Customizations.Add((ISpecimenBuilder)builderInstance!);
        }
    }
}