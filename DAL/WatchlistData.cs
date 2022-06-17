using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using DAL.Model;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class WatchlistData : IWatchlist
    {
        private readonly FilmContext _context;
        private readonly FilmData _data;

        public WatchlistData(FilmContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _data = new FilmData(context);
        }

        public void AddFilm(int userId, int filmId)
        {
                Film film = _data.GetFilm(filmId);
                WatchlistItem item = new(userId, film);
                _context.WatchlistItems.Add(item);
                _context.SaveChanges();
        }

        public void LikeFilm(int userId, int filmId)
        {
            WatchlistItem item = _context.WatchlistItems.FirstOrDefault(x => x.UserId == userId && x.Film.Id == filmId) ?? throw new ArgumentException("No film with that id could be found on this user's watchlist", nameof(filmId));
            item.Liked = true;
            _context.Update(item);
            _context.SaveChanges();
        }

        public void DislikeFilm(int userId, int filmId)
        {
            WatchlistItem item = _context.WatchlistItems.FirstOrDefault(x => x.UserId == userId && x.Film.Id == filmId) ?? throw new ArgumentException("No film with that id could be found on this user's watchlist", nameof(filmId));
            item.Liked = false;
            _context.Update(item);
            _context.SaveChanges();
        }

        public void MarkFilmAsWatched(int userId, int filmId)
        {
            WatchlistItem item = _context.WatchlistItems.FirstOrDefault(x => (x.UserId == userId) && (x.Film.Id == filmId)) ?? throw new ArgumentException("No film with that id could be found on this user's watchlist", nameof(filmId));
            item.Watched = true;
            _context.SaveChanges();
        }

        public void MarkFilmAsUnwatched(int userId, int filmId)
        {
            WatchlistItem item = _context.WatchlistItems.FirstOrDefault(x => (x.UserId == userId) && (x.Film.Id == filmId)) ?? throw new ArgumentException("No film with that id could be found on this user's watchlist", nameof(filmId));
            item.Watched = false;
            _context.SaveChanges();
        }

        public List<Film> GetLikedFilms(int userId)
        {
            return _context.WatchlistItems.Where(x => x.UserId == userId && x.Liked == true)
                .Select(x => x.Film)
                .ToList();
        }

        public List<WatchlistItem> GetWatchlist(int userId)
        {
            List<WatchlistItem> result = _context.WatchlistItems.Where(x => x.UserId == userId)
                .Include(x => x.Film)
                .ToList();
            if (result.Count == 0)
            {
                throw new ArgumentException("Either a user with this Id does not exist or there are no items on this user's watchlist", nameof(userId));
            }
            return result;
        }

        public List<Film> GetFilmsOnWatchlist(int userId)
        {
            List<Film> result = _context.WatchlistItems.Where(x => x.UserId == userId)
                .Select(x => x.Film)
                .ToList();
            if (result.Count == 0)
            {
                throw new ArgumentException("Either a user with this Id does not exist or there are no items on this user's watchlist", nameof(userId));
            }
            return result;
        }
    }
}