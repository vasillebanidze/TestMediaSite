using CORE.DbContexts;
using CORE.Entities;
using Microsoft.EntityFrameworkCore;
using REPOSITORY.Abstractions;
using REPOSITORY.Utilities;

namespace REPOSITORY.Implementations;

public class WatchListRepository : IWatchListRepository
{
    private readonly MediaContext _context;

    public WatchListRepository(MediaContext context)
    {
        _context = context;
    }


    public async Task<IReadOnlyList<WatchList>> GetWatchListListByUserIdAsync(int userId)
    {
        return await _context.WatchLists.AsNoTracking().Where(o => o.UserId == userId).Include(o => o.Media)
            .ThenInclude(o => o.MediaType)
            .ToListAsync();
    }

    public async Task<OperationResult> AddWatchListAsync(int userId, int mediaId)
    {
        var operationResult = new OperationResult();

        try
        {
            if (_context.WatchLists.AsNoTracking().Any(o => o.UserId == userId && o.MediaId == mediaId))
            {
                operationResult.IsSuccessfully = true;
                return operationResult;
            }


            _context.WatchLists.Add(new WatchList {UserId = userId, MediaId = mediaId, Watched = false});
            await _context.SaveChangesAsync();

            operationResult.IsSuccessfully = true;
            return operationResult;
        }
        catch (Exception e)
        {
            operationResult.ErrorString = e.InnerException != null ? e.InnerException.Message : e.Message;
            return operationResult;
        }
    }

    public async Task<OperationResult> ChangeMediaWatchedStatusAsync(int userId, int mediaId)
    {
        var operationResult = new OperationResult();

        try
        {
            var existWatchList =
                await _context.WatchLists.FirstOrDefaultAsync(o => o.UserId == userId && o.MediaId == mediaId);

            if (existWatchList == null)
            {
                operationResult.RecordNotFoundStatus = true;
                throw new Exception("ჩანაწერი ვერ მოიძებნა");
            }

            existWatchList.Watched = !existWatchList.Watched;

            _context.WatchLists.Update(existWatchList);
            await _context.SaveChangesAsync();

            operationResult.IsSuccessfully = true;
            return operationResult;
        }
        catch (Exception e)
        {
            operationResult.ErrorString = e.InnerException != null ? e.InnerException.Message : e.Message;
            return operationResult;
        }
    }

    public async Task<OperationResult> DeleteWatchListAsync(int userId, int mediaId)
    {
        var operationResult = new OperationResult();

        try
        {
            var existWatchList =
                await _context.WatchLists.FirstOrDefaultAsync(o => o.UserId == userId && o.MediaId == mediaId);

            if (existWatchList == null)
            {
                operationResult.IsSuccessfully = true;
                return operationResult;
            }

            _context.WatchLists.Remove(existWatchList);
            await _context.SaveChangesAsync();

            operationResult.IsSuccessfully = true;
            return operationResult;
        }
        catch (Exception e)
        {
            operationResult.ErrorString = e.InnerException != null ? e.InnerException.Message : e.Message;
            return operationResult;
        }
    }
}