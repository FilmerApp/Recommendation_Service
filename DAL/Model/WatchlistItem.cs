using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Model
{
    public class WatchlistItem
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public Film Film { get; set; }
        public bool Watched { get; set; }
        public bool? Liked { get; set; }

        public WatchlistItem()
        {

        }

        public WatchlistItem(int userId, Film film)
        {
            UserId = userId;
            Film = film;
            Watched = false;
            Liked = null;
        }
    }
}
