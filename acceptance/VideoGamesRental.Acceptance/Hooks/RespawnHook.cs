using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Respawn;
using TechTalk.SpecFlow;
using VideoGamesRental.Acceptance.Contexts;

namespace VideoGamesRental.Acceptance.Hooks;

[Binding]
public class RespawnHook
{
    private static Respawner? _respawner;
    private static string _connectionString = string.Empty;
    private readonly AcceptanceContext _context;

    public RespawnHook(AcceptanceContext inContext)
    {
        _context = inContext;
    }

    [BeforeScenario]
    public async Task RespawnDatabaseBeforeScenario()
    {
        var configuration = _context.ServiceProvider.GetRequiredService<IConfiguration>();
        _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        await ResetCheckpoint();
    }

    private static async Task ResetCheckpoint()
    {
        _respawner ??= await Respawner.CreateAsync(_connectionString, new RespawnerOptions
        {
            CheckTemporalTables = true
        });
        await _respawner.ResetAsync(_connectionString);
    }

    [AfterTestRun]
    public static async Task RespawnDatabaseAfterTestRun() =>
        await ResetCheckpoint();
}