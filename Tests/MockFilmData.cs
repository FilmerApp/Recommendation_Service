using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using DAL.Model;

namespace Tests
{
    public class MockFilmData : IFilmData
    {
        public List<Film> films { get; set; }

        public Film GetFilm(int id)
        {
            throw new NotImplementedException();
        }

        public List<Film> GetFilmList()
        {
            return films;
        }

        public void UpdateMainFilmList()
        {
            throw new NotImplementedException();
        }
    }
}
