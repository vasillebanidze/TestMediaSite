using CORE.Entities;
using REPOSITORY.Utilities;

namespace REPOSITORY.Abstractions;

public interface IMediaTypeRepository
{
    Task<MediaType?> GetMediaTypeByIdAsync(int id);
    Task<IReadOnlyList<MediaType>> GetMediaTypeListAsync();

    Task<OperationResult> AddMediaTypeAsync(string mediaTypeTitle);
    Task<OperationResult> EditMediaTypeAsync(int mediaTypeId, string mediaTypeTitle);
    Task<OperationResult> DeleteMediaTypeAsync(int mediaTypeId);
}