//The API now directly uses the DAL, which isn't really supposed to happen. There should be an interface in the logic layer that this API uses
//as well as a data transfer object that can be returned by the endpoint, instead of directly using the Film object from the DAL.
using DAL.Interfaces;
using DAL.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FilmContext = DAL.FilmContext;

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
        [Route("GetWatchlist")]
        public IActionResult GetWatchlist(int userId)
        {
            List<WatchlistItem> result = watchlistData.GetWatchlist(userId);
            return Ok(result);
        }

        [HttpGet]
        [Route("GetLikedFilms")]
        public IActionResult GetLikedFilms(int userId)
        {
            List<Film> result = watchlistData.GetLikedFilms(userId);
            return Ok(result);
        }

        [HttpPost]
        [Route("AddToWatchlist")]
        public IActionResult AddToWatchlist(int userId, int filmId)
        {
            watchlistData.AddFilm(userId, filmId);
            return Ok();
        }

        [HttpPut]
        [Route("MarkAsWatched")]
        public IActionResult MarkFilmAsWatched(int userId, int filmId)
        {
            watchlistData.MarkFilmAsWatched(userId, filmId);
            return Ok();
        }

        [HttpPut]
        [Route("LikeFilm")]
        public IActionResult LikeFilm(int userId, int filmId)
        {
            watchlistData.LikeFilm(userId, filmId);
            return Ok();
        }

        [HttpPut]
        [Route("DislikeFilm")]
        public IActionResult DislikeFilm(int userId, int filmId)
        {
            watchlistData.DislikeFilm(userId, filmId);
            return Ok();
        }
    }
}
