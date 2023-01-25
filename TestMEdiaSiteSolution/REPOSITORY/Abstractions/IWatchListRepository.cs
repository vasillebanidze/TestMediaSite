using CORE.Entities;
using REPOSITORY.Utilities;

namespace REPOSITORY.Abstractions;

public interface IWatchListRepository
{
    Task<IReadOnlyList<WatchList>> GetWatchListListByUserIdAsync(int userId);

    Task<OperationResult> AddWatchListAsync(int userId, int mediaId);
    Task<OperationResult> ChangeMediaWatchedStatusAsync(int userId, int mediaId);
    Task<OperationResult> DeleteWatchListAsync(int userId, int mediaId);
}