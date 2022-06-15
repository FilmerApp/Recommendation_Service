using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using DAL.Model;

namespace Tests
{
    public class MockWatchlistData : IWatchlist
    {

        public List<Film> likedFilms { get; set; }

        public void AddFilm(int userId, int filmId)
        {
            throw new NotImplementedException();
        }

        public void DislikeFilm(int userId, int filmId)
        {
            throw new NotImplementedException();
        }

        public List<Film> GetFilmsOnWatchlist(int userId)
        {
            return likedFilms;
        }

        public List<Film> GetLikedFilms(int userId)
        {
            return likedFilms;
        }

        public List<WatchlistItem> GetWatchlist(int userId)
        {
            throw new NotImplementedException();
        }

        public void LikeFilm(int userId, int filmId)
        {
            throw new NotImplementedException();
        }

        public void MarkFilmAsWatched(int userId, int filmId)
        {
            throw new NotImplementedException();
        }
    }
}
