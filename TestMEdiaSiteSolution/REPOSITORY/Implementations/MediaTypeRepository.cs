using CORE.DbContexts;
using CORE.Entities;
using Microsoft.EntityFrameworkCore;
using REPOSITORY.Abstractions;
using REPOSITORY.Utilities;
using REPOSITORY.Validators;

namespace REPOSITORY.Implementations;

public class MediaTypeRepository : IMediaTypeRepository
{
    private readonly MediaContext _context;

    public MediaTypeRepository(MediaContext context)
    {
        _context = context;
    }

    public async Task<MediaType?> GetMediaTypeByIdAsync(int mediaTypeId)
    {
        return await _context.MediaTypes.AsNoTracking().FirstOrDefaultAsync(o => o.MediaTypeId == mediaTypeId);
    }

    public async Task<IReadOnlyList<MediaType>> GetMediaTypeListAsync()
    {
        return await _context.MediaTypes.AsNoTracking().ToListAsync();
    }

    public async Task<OperationResult> AddMediaTypeAsync(string mediaTypeTitle)
    {
        var operationResult = new OperationResult();

        var mediaType = new MediaType
        {
            MediaTypeTitle = mediaTypeTitle
        };


        var validateResult = new MediaTypeValidator().Validate(mediaType);

        if (validateResult.IsValid)
            try
            {
                if (_context.MediaTypes.AsNoTracking().Any(o => o.MediaTypeTitle == mediaTypeTitle))
                {
                    operationResult.IsValidationError = true;
                    throw new Exception("დაფიქსირდა ფილმის ტიპის დასახელების დუბლიკატი, რომლის მნიშვნელობაა - " +
                                        mediaTypeTitle);
                }


                _context.MediaTypes.Add(mediaType);
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

    public async Task<OperationResult> EditMediaTypeAsync(int mediaTypeId, string mediaTypeTitle)
    {
        var operationResult = new OperationResult();


        try
        {
            if (_context.MediaTypes.AsNoTracking()
                .Any(o => o.MediaTypeTitle == mediaTypeTitle && o.MediaTypeId != mediaTypeId))
            {
                operationResult.IsValidationError = true;

                throw new Exception("დაფიქსირდა ფილმის ტიპის დასახელების დუბლიკატი, რომლის მნიშვნელობაა - " +
                                    mediaTypeTitle);
            }

            var existMediaType = await _context.MediaTypes.FirstOrDefaultAsync(o => o.MediaTypeId == mediaTypeId);

            if (existMediaType == null)
            {
                operationResult.RecordNotFoundStatus = true;
                throw new Exception("ჩანაწერი, რომლის იდენტიფიკატორია - " + mediaTypeId +
                                    ", ბაზაში ვერ მოიძებნა. შესაძლებელია იგი წაშლილი იქნა სხვა მომხმარებლის მიერ სანამ თქვენ ახდენდით მისი ცვლილებას.");
            }

            existMediaType.MediaTypeTitle = mediaTypeTitle;
            var validateResult = new MediaTypeValidator().Validate(existMediaType);

            if (!validateResult.IsValid)
            {
                operationResult.IsValidationError = true;
                throw new Exception(validateResult.Errors.Select(x => x.ErrorMessage).First());
            }

            _context.MediaTypes.Update(existMediaType);
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

    public async Task<OperationResult> DeleteMediaTypeAsync(int mediaTypeId)
    {
        var operationResult = new OperationResult();

        try
        {
            var existMediaType = await _context.MediaTypes.FirstOrDefaultAsync(o => o.MediaTypeId == mediaTypeId);

            if (existMediaType == null)
            {
                operationResult.RecordNotFoundStatus = true;
                throw new Exception("ჩანაწერი, რომლის იდენტიფიკატორია - " + mediaTypeId +
                                    ", ბაზაში ვერ მოიძებნა. შესაძლებელია იგი უკვე წაშლილია სხვა მომხმარებლის მიერ.");
            }

            _context.MediaTypes.Remove(existMediaType);
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