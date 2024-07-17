using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using VideoGamesRental.Infrastructure.VideoGames;
// ReSharper disable ReturnTypeCanBeEnumerable.Global - EF Core requirement

namespace VideoGamesRental.Infrastructure;

[ExcludeFromCodeCoverage]
public class ApplicationContext(DbContextOptions inOptions) : DbContext(inOptions)
{
    public DbSet<VideoGameDataModel> VideoGames => Set<VideoGameDataModel>();
    
    protected override void OnModelCreating(ModelBuilder inModelBuilder)
    {
        base.OnModelCreating(inModelBuilder);
        
        foreach (var relationship in inModelBuilder.Model.GetEntityTypes()
                     .SelectMany(inMutableEntityType => inMutableEntityType.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }
    }
    
    protected override void ConfigureConventions(ModelConfigurationBuilder inConfigurationBuilder)
    {
        base.ConfigureConventions(inConfigurationBuilder);
        inConfigurationBuilder.Properties<decimal>().HavePrecision(18, 2);
        inConfigurationBuilder.Properties<decimal?>().HavePrecision(18, 2);
    }
}