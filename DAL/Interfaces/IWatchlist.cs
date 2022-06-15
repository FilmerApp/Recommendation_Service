using DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IWatchlist
    {
        public List<WatchlistItem> GetWatchlist(int userId);
        public List<Film> GetFilmsOnWatchlist(int userId);
        public List<Film> GetLikedFilms(int userId);
        public void AddFilm(int userId, int filmId);
        public void LikeFilm(int userId, int filmId);
        public void DislikeFilm(int userId, int filmId);
        public void MarkFilmAsWatched(int userId, int filmId);
    }
}
