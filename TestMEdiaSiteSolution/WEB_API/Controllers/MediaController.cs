using Microsoft.AspNetCore.Mvc;
using REPOSITORY.Abstractions;
using WEB_API.Models;

namespace WEB_API.Controllers;

public class MediaController : BaseApiController
{
    private readonly IMediaRepository _mediaRepository;

    public MediaController(IMediaRepository mediaRepository)
    {
        _mediaRepository = mediaRepository;
    }


    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<MediaViewModel>>> Get(int? mediaTypeId, string? searchTerm)
    {
        if(searchTerm == null)
        {
            var mediaList = await _mediaRepository.GetMediaListAsync(mediaTypeId);
            return Ok(mediaList.Select(media => new MediaViewModel
                {MediaId = media.MediaId, MediaTypeId = media.MediaTypeId, MediaTitle = media.MediaTitle, PictureUrl = media.PictureUrl}));
        }
        
        var searchMediaList = await _mediaRepository.SearchMediaAsync(mediaTypeId, searchTerm);
        return Ok(searchMediaList.Select(media => new MediaViewModel
            {MediaId = media.MediaId, MediaTypeId = media.MediaTypeId, MediaTitle = media.MediaTitle, PictureUrl = media.PictureUrl}));
        
    }

    [HttpGet("{mediaId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MediaViewModel?>> Get(int mediaId)
    {
        var media = await _mediaRepository.GetMediaByIdAsync(mediaId);
        if (media == null) return NotFound(new {message = "ჩანაწერი ვერ მოიძებნა"});
        return Ok(new MediaViewModel
            {MediaId = media.MediaId, MediaTypeId = media.MediaTypeId, MediaTitle = media.MediaTitle, PictureUrl = media.PictureUrl});
    }


    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Post(MediaViewModel media)
    {
        var operationResult = await _mediaRepository.AddMediaAsync( media.MediaTypeId, media.MediaTitle, media.PictureUrl);

        if (operationResult.IsSuccessfully)
            return Ok();
        
        if (operationResult.RecordNotFoundStatus)
            return NotFound(operationResult.ErrorString);


        return BadRequest(new {message = operationResult.ErrorString});
    }


    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Put(MediaViewModel media)
    {
        var operationResult =
            await _mediaRepository.EditMediaAsync(media.MediaId, media.MediaTypeId, media.MediaTitle, media.PictureUrl);

        if (operationResult.IsSuccessfully)
            return Ok();
        
        if (operationResult.RecordNotFoundStatus)
            return NotFound(operationResult.ErrorString);


        return BadRequest(new {message = operationResult.ErrorString});
    }


    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(int mediaId)
    {
        var operationResult = await _mediaRepository.DeleteMediaAsync(mediaId);
        if (operationResult.IsSuccessfully)
            return Ok();

        return BadRequest(new {message = operationResult.ErrorString});
    }
}