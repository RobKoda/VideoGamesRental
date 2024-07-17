using LanguageExt;
using Mapster;
using Microsoft.EntityFrameworkCore;
// ReSharper disable UnusedMember.Global - Extension methods
// ReSharper disable MemberCanBePrivate.Global - Extension methods

namespace VideoGamesRental.Infrastructure.Core;

public static class ApplicationContextFactoryExtensions
{
    public static DbSet<TDbType> GetQueryable<TDbType>(ApplicationContext inContext) where TDbType : class, IGuidEntity => 
        inContext.Set<TDbType>();

    public static IQueryable<TDbType> GetQueryableAsNoTracking<TDbType>(this ApplicationContext inContext) where TDbType : class, IGuidEntity => 
        GetQueryable<TDbType>(inContext).AsNoTracking();

    public static IQueryable<TDbType> GetQueryable<TDbType>(this ApplicationContext inContext, Func<IQueryable<TDbType>, IQueryable<TDbType>> inGetQueryable) where TDbType : class, IGuidEntity => 
        inGetQueryable.Invoke(GetQueryable<TDbType>(inContext));
    
    public static IQueryable<TDbType> GetQueryableAsNoTracking<TDbType>(this ApplicationContext inContext, Func<IQueryable<TDbType>, IQueryable<TDbType>> inGetQueryable) where TDbType : class, IGuidEntity => 
        inGetQueryable.Invoke(GetQueryableAsNoTracking<TDbType>(inContext));
    
    public static async Task<IEnumerable<TDbType>> GetAllAsync<TDbType>(this ApplicationContext inContext, Func<IQueryable<TDbType>, IQueryable<TDbType>> inGetQueryable) where TDbType : class, IGuidEntity => 
        await GetQueryableAsNoTracking(inContext, inGetQueryable)
            .ToListAsync();

    public static Task<IEnumerable<TDbType>> GetAllAsync<TDbType>(this ApplicationContext inContext) where TDbType : class, IGuidEntity => 
        GetAllAsync<TDbType>(inContext, inQueryable => inQueryable);

    public static async Task<IEnumerable<TDomainType>> GetAllAsync<TDbType, TDomainType>(this ApplicationContext inContext, Func<IQueryable<TDbType>, IQueryable<TDbType>> inGetQueryable) where TDbType : class, IGuidEntity =>
        await GetQueryableAsNoTracking(inContext, inGetQueryable)
            .ProjectToType<TDomainType>()
            .ToListAsync();

    public static Task<IEnumerable<TDomainType>> GetAllAsync<TDbType, TDomainType>(this ApplicationContext inContext) where TDbType : class, IGuidEntity => 
        GetAllAsync<TDbType, TDomainType>(inContext, inQueryable => inQueryable);

    public static async Task<Option<TDbType>> GetSingleAsync<TDbType>(this ApplicationContext inContext, Guid inId, Func<IQueryable<TDbType>, IQueryable<TDbType>> inGetQueryable) where TDbType : class, IGuidEntity
    {
        var result = await GetQueryableAsNoTracking(inContext, inGetQueryable)
            .SingleOrDefaultAsync(inDbType => inDbType.Id == inId);
        
        return result == default
            ? Option<TDbType>.None
            : Option<TDbType>.Some(result);
    }
    
    public static async Task<Option<TDbType>> GetSingleAsync<TDbType>(this ApplicationContext inContext, Guid inId) where TDbType : class, IGuidEntity => 
        await GetSingleAsync<TDbType>(inContext, inId, inQueryable => inQueryable);
    
    public static async Task<Option<TDomainType>> GetSingleAsync<TDbType, TDomainType>(this ApplicationContext inContext, Guid inId, Func<IQueryable<TDbType>, IQueryable<TDbType>> inGetQueryable) where TDbType : class, IGuidEntity =>
        (await GetSingleAsync(inContext, inId, inGetQueryable))
        .Map<TDomainType>(inSome => inSome.Adapt<TDomainType>());

    public static Task<Option<TDomainType>> GetSingleAsync<TDbType, TDomainType>(this ApplicationContext inContext, Guid inId) where TDbType : class, IGuidEntity => 
        GetSingleAsync<TDbType, TDomainType>(inContext, inId, inQueryable => inQueryable);

    public static async Task<bool> AnyAsync<TDbType>(this ApplicationContext inContext, Guid inId) where TDbType : class, IGuidEntity =>
        await GetQueryableAsNoTracking<TDbType>(inContext)
            .AnyAsync(inDbType => inDbType.Id == inId);

    public static async Task DeleteAsync<TDbType>(this ApplicationContext inContext, Guid inId) where TDbType : class, IGuidEntity =>
        await GetQueryableAsNoTracking<TDbType>(inContext)
            .Where(inDbType => inDbType.Id == inId)
            .ExecuteDeleteAsync();

    public static Task SaveAsync<TDbType, TDomainType>(this ApplicationContext inContext, TDomainType inToSave) where TDbType : class, IGuidEntity => 
        SaveAsync(inContext, inToSave.Adapt<TDbType>());

    public static async Task SaveAsync<TDbType>(this ApplicationContext inContext, TDbType inToSave) where TDbType : class, IGuidEntity
    {
        var queryable = GetQueryable<TDbType>(inContext);

        if (await AnyAsync<TDbType>(inContext, inToSave.Id))
        {
            queryable.Update(inToSave);
        }
        else
        {
            await queryable.AddAsync(inToSave);
        }

        await inContext.SaveChangesAsync();
    }

    public static async Task<bool> CanBeDeleted<TDbType>(this ApplicationContext inContext, Guid inId) where TDbType : class, IGuidEntity
    {
        var toValidate = await GetSingleAsync<TDbType>(inContext, inId);
        
        return await toValidate.MatchAsync(async inSome =>
            {
                await inContext.Database.BeginTransactionAsync();
                inContext.Remove(inSome);
                try
                {
                    await inContext.SaveChangesAsync();
                    return true;
                }
                catch
                {
                    return false;
                }
                finally
                {
                    await inContext.Database.RollbackTransactionAsync();
                }
            },
            () => false);
    }
}