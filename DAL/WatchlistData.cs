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
            WatchlistItem item = _context.WatchlistItems.FirstOrDefault(x => x.UserId == userId && x.Film.Id == filmId) ?? throw new ArgumentException("No film with that id could be found on this user's watchlist", nameof(filmId));
            item.Watched = true;
            _context.Update(item);
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
            return _context.WatchlistItems.Where(x => x.UserId == userId)
                .Include(x => x.Film)
                .ToList();
        }

        public List<Film> GetFilmsOnWatchlist(int userId)
        {
            return _context.WatchlistItems.Where(x => x.UserId == userId)
                .Select(x => x.Film)
                .ToList();
        }
    }
}