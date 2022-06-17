//The API now directly uses the DAL, which isn't ideal. There should be an interface in the logic layer that this API uses
//as well as a data transfer object that can be returned by the endpoint, instead of directly using the Film object from the DAL.
using DAL.Interfaces;
using DAL.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WatchlistService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WatchlistController : ControllerBase
    {
        private readonly IWatchlist watchlistData;

        public WatchlistController(IWatchlist? _watchlistData = null)
        {
            watchlistData = _watchlistData;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<WatchlistItem>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("GetWatchlist")]
        public IActionResult GetWatchlist(int userId)
        {
            try
            {
                List<WatchlistItem> result = watchlistData.GetWatchlist(userId);
                return Ok(result);
            }
            catch (ArgumentException)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Film>))]
        [Route("GetLikedFilms")]
        public IActionResult GetLikedFilms(int userId)
        {
            List<Film> result = watchlistData.GetLikedFilms(userId);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("AddToWatchlist")]
        public IActionResult AddToWatchlist(int userId, int filmId)
        {
            try
            {
                watchlistData.AddFilm(userId, filmId);
                return Ok();
            } 
            catch (ArgumentException)
            {
                return BadRequest();
            }

        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("MarkAsWatched")]
        public IActionResult MarkFilmAsWatched(int userId, int filmId)
        {
            try
            {
                watchlistData.MarkFilmAsWatched(userId, filmId);
                return Ok();
            } 
            catch (ArgumentException)
            {
                return BadRequest();
            }

        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("LikeFilm")]
        public IActionResult LikeFilm(int userId, int filmId)
        {
            try
            {
                watchlistData.LikeFilm(userId, filmId);
                return Ok();
            }
            catch (ArgumentException)
            {
                return BadRequest();
            }

        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("DislikeFilm")]
        public IActionResult DislikeFilm(int userId, int filmId)
        {
            try
            {
                watchlistData.DislikeFilm(userId, filmId);
                return Ok();
            }
            catch (ArgumentException)
            {
                return BadRequest();
            }
        }
    }
}
