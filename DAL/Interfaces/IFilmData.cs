using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Model;

namespace DAL.Interfaces
{
    public interface IFilmData
    {
        public List<Film> GetFilmList();
        public void UpdateMainFilmList();
        public Film GetFilm(int id);
    }
}
