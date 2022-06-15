using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Model;
using DAL.Interfaces;

namespace DAL
{
    public class FilmData : IFilmData
    {
        private readonly FilmContext _context;

        public FilmData(FilmContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public List<Film> GetFilmList()
        {
            return _context.Films.ToList();
        }

        public Film GetFilm(int id)
        {
            return _context.Films.Where(x => x.Id == id).FirstOrDefault() ?? throw new ArgumentException("No film with that id could be found", nameof(id));
        }

        public void UpdateMainFilmList()
        {
            throw new NotImplementedException();
        }
    }
}