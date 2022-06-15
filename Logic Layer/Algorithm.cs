using DAL.Model;
using DAL.Interfaces;
using Logic_Layer.Interfaces;
using DAL;

namespace Logic_Layer
{
    public class Algorithm: IRecommendation
    {
        private readonly IFilmData filmData;
        private readonly IWatchlist watchList;


        public Algorithm(IFilmData? _filmData = null, IWatchlist? _watchlist = null)
        {
            filmData = _filmData;
            watchList = _watchlist;
        }

        public List<Film> MakeRecommendationList(int userId, int amount)
        {
            //list of all films, while this makes manipulating the data here easier it is a very heavy database operation
            //it might be better to perform some of these actions directly on the database instead of reading them into a list first
            List<Film> allFilms = filmData.GetFilmList();
            List<Film> watchedFilms = watchList.GetFilmsOnWatchlist(userId);
            //remove watched films from films
            allFilms = allFilms.Except(watchedFilms).ToList();
            if (allFilms.Count == 0)
            {
                throw new IndexOutOfRangeException("No more films could be found in the database");
            }

            List<Film> result = new();
            List<string> favoriteGenres = GetMostLikedGenres(userId);
            for (int i = 0; i < amount; i++)
            {
                if (allFilms.Count == 0)
                {
                    throw new IndexOutOfRangeException("No more films could be found in the database");
                }
                Film recommendation = RecommendFilm(favoriteGenres, allFilms);
                result.Add(recommendation);
                allFilms.Remove(recommendation);
            }
            return result;
        }

        public Film RecommendFilm(List<string> favoriteGenres, List<Film> filmList)
        {
            if (favoriteGenres.Count == 0)
            {
                throw new ApplicationException("No liked films could be found");
            }
            Random rnd = new();
            string genre;
            //picks a random genre out of the users' top 5 liked genres
            if (favoriteGenres.Count > 5)
            {
                genre = favoriteGenres[rnd.Next(5)];
            }
            else
            {
                genre = favoriteGenres[rnd.Next(favoriteGenres.Count)];
            }
            //GetGenres is a method of the Film object, this should really be handled through an interface
            List<Film> films = filmList.Where(x => x.GetGenres().Contains(genre)).ToList();
            //if no more films with the genre could be found the film with the highest rating gets returned
            if (films.Count == 0)
            {
                return filmList.OrderByDescending(x => x.Rating).First(); 
            }
            return films.OrderByDescending(x => x.Rating).First();
        }

        //Because we are going through a lot of strings with this method, it might be better to set up the database with a seperate table for genres
        //and a table to match the genre id with specific film ids, which would make searching them much faster.
        public List<string> GetMostLikedGenres(int userId)
        {
            List<Film> LikedFilms = watchList.GetLikedFilms(userId);

            List<string> likedGenres = new();
            foreach (Film f in LikedFilms)
            {
                likedGenres.AddRange(f.GetGenres());
            }
            List<string> mostPopular = likedGenres.GroupBy(x => x)
                .OrderByDescending(x => x.Count())
                .Select(x => x.Key)
                .ToList();
            return mostPopular;
        }
    }
}