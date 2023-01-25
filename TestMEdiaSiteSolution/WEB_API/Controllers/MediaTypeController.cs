using Microsoft.AspNetCore.Mvc;
using REPOSITORY.Abstractions;
using WEB_API.Models;

namespace WEB_API.Controllers;

public class MediaTypeController : BaseApiController
{
    private readonly IMediaTypeRepository _mediaTypeRepository;

    public MediaTypeController(IMediaTypeRepository mediaTypeRepository)
    {
        _mediaTypeRepository = mediaTypeRepository;
    }


    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<MediaTypeViewModel>>> Get()
    {
        var mediaTypeList = await _mediaTypeRepository.GetMediaTypeListAsync();
        return Ok(mediaTypeList.Select(mediaType => new MediaTypeViewModel
            {MediaTypeId = mediaType.MediaTypeId, MediaTypeTitle = mediaType.MediaTypeTitle}));
    }

    [HttpGet("{mediaTypeId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MediaTypeViewModel?>> Get(int mediaTypeId)
    {
        var mediaType = await _mediaTypeRepository.GetMediaTypeByIdAsync(mediaTypeId);
        if (mediaType == null) return NotFound(new {message = "ჩანაწერი ვერ მოიძებნა"});
        return Ok(new MediaTypeViewModel
            {MediaTypeId = mediaType.MediaTypeId, MediaTypeTitle = mediaType.MediaTypeTitle});
    }


    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Post(MediaTypeViewModel mediaType)
    {
        var operationResult = await _mediaTypeRepository.AddMediaTypeAsync(mediaType.MediaTypeTitle);

        if (operationResult.IsSuccessfully)
            return Ok();
        
        return BadRequest(new {message = operationResult.ErrorString});
    }


    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Put(MediaTypeViewModel mediaType)
    {
        var operationResult =
            await _mediaTypeRepository.EditMediaTypeAsync(mediaType.MediaTypeId, mediaType.MediaTypeTitle);

        if (operationResult.IsSuccessfully)
            return Ok();

        
        if (operationResult.RecordNotFoundStatus)
            return NotFound(operationResult.ErrorString);


        return BadRequest(new {message = operationResult.ErrorString});
    }


    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(int mediaTypeId)
    {
        var operationResult = await _mediaTypeRepository.DeleteMediaTypeAsync(mediaTypeId);
        if (operationResult.IsSuccessfully)
            return Ok();

        return BadRequest(new {message = operationResult.ErrorString});
    }
}