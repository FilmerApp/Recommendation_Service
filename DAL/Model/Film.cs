using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Model
{
    public class Film
    {
        //Primary Key
        public int Id { get; set; }

        //Properties
        public string Name { get; set; }
        public int ReleaseYear { get; set; }
        public double Rating { get; set; }

        //Foreign Keys
        public ICollection<Genre> Genres { get; set; }

        public Film()
        {

        }

        public Film(int id, string name, int releaseYear, double rating, ICollection<Genre> genres)
        {
            Id = id;
            Name = name;
            ReleaseYear = releaseYear;
            Rating = rating;
            Genres = genres;
        }

        public List<string> GetGenres()
        {
            List<string> result = new();
            foreach (Genre g in Genres)
            {
                result.Add(g.Name);
            }
            return result;
        }
    }
}
