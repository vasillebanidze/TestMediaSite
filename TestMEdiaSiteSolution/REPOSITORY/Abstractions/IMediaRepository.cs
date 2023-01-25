using CORE.Entities;
using REPOSITORY.Utilities;

namespace REPOSITORY.Abstractions;

public interface IMediaRepository
{
    Task<Media?> GetMediaByIdAsync(int id);
    Task<IReadOnlyList<Media>> GetMediaListAsync(int? mediaTypeId);
    Task<IReadOnlyList<Media>> SearchMediaAsync(int? mediaTypeId, string? mediaTitle = "");

    Task<OperationResult> AddMediaAsync(int mediaTypeId, string mediaTitle, string pictureUrl);
    Task<OperationResult> EditMediaAsync(int mediaId, int mediaTypeId, string mediaTitle, string pictureUrl);
    Task<OperationResult> DeleteMediaAsync(int mediaId);
}