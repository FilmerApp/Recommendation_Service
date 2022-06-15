using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Model;

namespace Logic_Layer.Interfaces
{
    public interface IRecommendation
    {
        public List<Film> MakeRecommendationList(int userId, int amount);
    }
}
