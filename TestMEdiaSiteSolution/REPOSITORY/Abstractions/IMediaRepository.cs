using CORE.Entities;
using REPOSITORY.Utilities;

namespace REPOSITORY.Abstractions;

public interface IMediaRepository
{
    Task<Media?> GetMediaByIdAsync(int id);
    Task<IReadOnlyList<Media>> GetMediaListAsync();
    Task<IReadOnlyList<Media>> SearchMediaAsync(string mediaTitle);

    Task<OperationResult> AddMediaAsync(int mediaTypeId, string mediaTitle, string pictureUrl);
    Task<OperationResult> EditMediaAsync(int mediaId, int mediaTypeId, string mediaTitle, string pictureUrl);
    Task<OperationResult> DeleteMediaAsync(int mediaId);
}