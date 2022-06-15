using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Model
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Genre()
        {

        }

        public Genre(int id, string name)
        {
            Id = id;
            Name = name;
        }   
    }
}
