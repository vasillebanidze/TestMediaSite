using CORE.DbContexts;
using CORE.Entities;
using Microsoft.EntityFrameworkCore;
using REPOSITORY.Abstractions;
using REPOSITORY.Utilities;
using REPOSITORY.Validators;

namespace REPOSITORY.Implementations;

public class MediaRepository : IMediaRepository
{
    private readonly MediaContext _context;

    public MediaRepository(MediaContext context)
    {
        _context = context;
    }

    public async Task<Media?> GetMediaByIdAsync(int mediaId)
    {
        return await _context.Medias.AsNoTracking().FirstOrDefaultAsync(o => o.MediaId == mediaId);
    }

    public async Task<IReadOnlyList<Media>> GetMediaListAsync(int? mediaTypeId)
    {
        if(mediaTypeId == null)
            return await _context.Medias.Include(o => o.MediaType).Include(o => o.WatchLists).AsNoTracking().ToListAsync();
        return await _context.Medias.Include(o => o.MediaType).Include(o => o.WatchLists).Where(o => o.MediaTypeId == mediaTypeId).AsNoTracking().ToListAsync();
    }

    public async Task<IReadOnlyList<Media>> SearchMediaAsync(int? mediaTypeId, string? mediaTitle = "")
    {
        if (string.IsNullOrEmpty(mediaTitle))

            return await GetMediaListAsync(mediaTypeId);
            
        if(mediaTypeId == null)
        {
            var mediaListQuery = from media in _context.Set<Media>()
                join mediaType in _context.Set<MediaType>() on media.MediaTypeId equals mediaType.MediaTypeId
                where EF.Functions.Like(media.MediaTitle, "%" + mediaTitle + "%")
                select media;
            return await mediaListQuery.ToListAsync();
        }

        var mediaListQueryWithFilter = from media in _context.Set<Media>()
            join mediaType in _context.Set<MediaType>() on media.MediaTypeId equals mediaType.MediaTypeId
            where EF.Functions.Like(media.MediaTitle, "%" + mediaTitle + "%") && media.MediaTypeId == mediaTypeId
            select media;


        return await mediaListQueryWithFilter.ToListAsync();
    }

    public async Task<OperationResult> AddMediaAsync(int mediaTypeId, string mediaTitle, string pictureUrl)
    {
        var operationResult = new OperationResult();

        var media = new Media
        {
            MediaTypeId = mediaTypeId,
            MediaTitle = mediaTitle,
            PictureUrl = pictureUrl
        };


        var validateResult = new MediaValidator().Validate(media);
        if (validateResult.IsValid)
            try
            {
                _context.Medias.Add(media);
                await _context.SaveChangesAsync();

                operationResult.IsSuccessfully = true;
                return operationResult;
            }
            catch (Exception e)
            {
                operationResult.ErrorString = e.InnerException != null ? e.InnerException.Message : e.Message;
                return operationResult;
            }

        operationResult.IsValidationError = true;
        operationResult.ErrorString = validateResult.Errors.Select(x => x.ErrorMessage).First();
        return operationResult;
    }

    public async Task<OperationResult> EditMediaAsync(int mediaId, int mediaTypeId, string mediaTitle,
        string pictureUrl)
    {
        var operationResult = new OperationResult();


        try
        {
            var existMedia = await _context.Medias.FirstOrDefaultAsync(o => o.MediaId == mediaId);

            if (existMedia == null)
            {
                operationResult.RecordNotFoundStatus = true;
                throw new Exception("ჩანაწერი, რომლის იდენტიფიკატორია - " + mediaId +
                                    ", ბაზაში ვერ მოიძებნა. შესაძლებელია იგი წაშლილი იქნა სხვა მომხმარებლის მიერ სანამ თქვენ ახდენდით მისი ცვლილებას.");
            }


            existMedia.MediaTypeId = mediaTypeId;
            existMedia.MediaTitle = mediaTitle;
            existMedia.PictureUrl = pictureUrl;


            var validateResult = new MediaValidator().Validate(existMedia);

            if (!validateResult.IsValid)
            {
                operationResult.IsValidationError = true;
                throw new Exception(validateResult.Errors.Select(x => x.ErrorMessage).First());
            }

            _context.Medias.Update(existMedia);
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

    public async Task<OperationResult> DeleteMediaAsync(int mediaId)
    {
        var operationResult = new OperationResult();

        try
        {
            var existMedia = await _context.Medias.FirstOrDefaultAsync(o => o.MediaId == mediaId);

            if (existMedia == null)
            {
                operationResult.RecordNotFoundStatus = true;
                throw new Exception("ჩანაწერი, რომლის იდენტიფიკატორია - " + mediaId +
                                    ", ბაზაში ვერ მოიძებნა. შესაძლებელია იგი უკვე წაშლილია სხვა მომხმარებლის მიერ.");
            }

            _context.Medias.Remove(existMedia);
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