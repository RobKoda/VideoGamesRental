using Microsoft.EntityFrameworkCore;

// ReSharper disable MemberCanBePrivate.Global - Core methods

namespace VideoGamesRental.Infrastructure.Tests.Core;

public class DataBuilder<TDbContext> where TDbContext : DbContext
{
    private static string ConnectionString =>
        "Server=VAULTINATOR; Database=VideoGamesRental-unittests-database;TrustServerCertificate=True;User Id=VideoGamesRental_User;Password=VideoGamesRental123!";

    private DataBuilder(TDbContext inContext)
    {
        Context = inContext;
    }

    public TDbContext Context { get; }

    public async Task CommitAsync()
    {
        await Context.SaveChangesAsync();
        ClearTracking();
    }

    public void ClearTracking() => Context.ChangeTracker.Clear(); 
    
    public static DataBuilder<TDbContext> Build() => new(CreateContext());

    public DataBuilder<TDbContext> WithEntity<T>(T inEntity)
        where T : class
    {
        Context.Add(inEntity);
        return this;
    }

    public DataBuilder<TDbContext> WithEntities<T>(IEnumerable<T> inEntities)
        where T : class =>
            inEntities.Aggregate(this, (inCurrent, inEntity) => inCurrent.WithEntity(inEntity));
    
    private static TDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<TDbContext>()
            .UseSqlServer(ConnectionString)
            .EnableSensitiveDataLogging()
            .Options;
        var context = (TDbContext) Activator.CreateInstance(typeof(TDbContext), options)!;
        context.Database.EnsureCreated();
        return context;
    }
}