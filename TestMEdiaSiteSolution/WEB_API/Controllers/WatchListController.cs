using Microsoft.AspNetCore.Mvc;
using REPOSITORY.Abstractions;
using WEB_API.Models;

namespace WEB_API.Controllers;

public class WatchListController : BaseApiController
{
    private readonly IWatchListRepository _watchListRepository;

    public WatchListController(IWatchListRepository watchListRepository)
    {
        _watchListRepository = watchListRepository;
    }


    [HttpGet("{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<WatchListViewModel>>> Get(int userId)
    {
        var watchListList = await _watchListRepository.GetWatchListListByUserIdAsync(userId);
        return Ok(watchListList.Select(watchList => new WatchListViewModel
        {
            UserId = watchList.UserId, 
            
            MediaId = watchList.Media.MediaId, 
            MediaTitle = watchList.Media.MediaTitle,

            MediaTypeId = watchList.Media.MediaTypeId, 
            MediaTypeTitle = watchList.Media.MediaType.MediaTypeTitle,

            PictureUrl = watchList.Media.PictureUrl,

            Watched = watchList.Watched
        }));
    }


    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Post(int userId, int mediaId)
    {
        var operationResult = await _watchListRepository.AddWatchListAsync( userId, mediaId);

        if (operationResult.IsSuccessfully)
            return Ok();

        return BadRequest(new {message = operationResult.ErrorString});
    }


    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Put(int userId, int mediaId)
    {
        var operationResult =
            await _watchListRepository.ChangeMediaWatchedStatusAsync(userId, mediaId);

        if (operationResult.IsSuccessfully)
            return Ok();
        
        if (operationResult.RecordNotFoundStatus)
            return NotFound(operationResult.ErrorString);

        return BadRequest(new {message = operationResult.ErrorString});
    }


    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(int userId, int mediaId)
    {
        var operationResult = await _watchListRepository.DeleteWatchListAsync(userId, mediaId);
        if (operationResult.IsSuccessfully)
            return Ok();

        return BadRequest(new {message = operationResult.ErrorString});
    }
}