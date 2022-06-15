using NUnit.Framework;
using Logic_Layer;
using DAL.Model;

namespace Tests
{
    public class Tests
    {
        MockFilmData filmData;
        MockWatchlistData watchlistData;
        Algorithm algorithm;

        [SetUp]
        public void Setup()
        {
            filmData = new();
            watchlistData = new();
            algorithm = new(filmData, watchlistData);
        }

        [Test]
        public void CanFindMostLikedGenresInOrder()
        {
            //Arrange
            List<Genre> genres1 = new() { new Genre(1, "Drama"), new Genre(2, "Crime") };
            List<Genre> genres2 = new() { new Genre(1, "Drama"), new Genre(2, "Romance") };
            Film film1 = new(1, "film1", 1900, 6, genres1);
            Film film2 = new(2, "film2", 1900, 9, genres1);
            Film film3 = new(3, "film3", 1900, 8, genres2);
            watchlistData.likedFilms = new() { film1, film2, film3 };

            //Act
            List<string> favoriteGenres = algorithm.GetMostLikedGenres(1);

            //Assert
            Assert.That(favoriteGenres.Count, Is.EqualTo(3));
            Assert.That(favoriteGenres[0], Is.EqualTo("Drama"));
            Assert.That(favoriteGenres[1], Is.EqualTo("Crime"));
            Assert.That(favoriteGenres[2], Is.EqualTo("Romance"));
        }

        [Test]
        public void GetMostLikedGenresWorksWithEmptyWatchlist()
        {
            //Arrange
            watchlistData.likedFilms = new();

            //Act
            List<string> favoriteGenres = algorithm.GetMostLikedGenres(1);

            //Assert
            Assert.That(favoriteGenres.Count, Is.EqualTo(0));
        }

        [Test]
        public void RecommendFilmThrowsExceptionWithN()
        {
            //Arrange
            List<string> favoriteGenres = new();
            List<Genre> genres = new() { new Genre(1, "Drama"), new Genre(2, "Crime") };
            Film film1 = new(1, "film1", 1900, 6, genres);
            Film film2 = new(2, "film2", 1900, 9, genres);
            List<Film> films = new() { film1, film2 };

            //Assert
            Assert.Throws<ApplicationException>(() => algorithm.RecommendFilm(favoriteGenres, films));
        }

        [Test]
        public void FilmWithHighestRatingInGenreIsReturned()
        {
            //Arrange
            List<string> favoriteGenres = new() { "Drama" };
            List<Genre> genres = new() { new Genre(1, "Drama"), new Genre(2, "Crime") };
            Film film1 = new(1, "film1", 1900, 6.0, genres);
            Film film2 = new(2, "film2", 1900, 9.0, genres);
            Film film3 = new(3, "film3", 1900, 8.5, genres);
            List<Film> films = new() { film1, film2, film3 };

            //Act
            Film result = algorithm.RecommendFilm(favoriteGenres, films);

            //Assert
            Assert.That(result, Is.EqualTo(film2));
        }

        [Test]
        public void FilmsAlreadyOnWatchlistDontGetRecommended()
        {
            //Arrange
            List<Genre> genres = new() { new Genre(1, "Drama"), new Genre(2, "Crime") };
            Film film1 = new(1, "film1", 1999, 9.8, genres);
            Film film2 = new(2, "film2", 2022, 9.0, genres);
            Film film3 = new(3, "film3", 1932, 8, genres);
            watchlistData.likedFilms = new() { film1 };
            filmData.films = new() { film1, film2, film3 };

            //Act
            List<Film> result = algorithm.MakeRecommendationList(1, 2);

            //Assert
            Assert.That(result.Contains(film1), Is.False);
            Assert.That(result.Contains(film2), Is.True);
            Assert.That(result.Contains(film3), Is.True);
        }

        [Test]
        public void FilmsRecommendationsContainNoDuplicates()
        {
            //Arrange
            List<Genre> genres1 = new() { new Genre(1, "Drama"), new Genre(2, "Crime") };
            List<Genre> genres2 = new() { new Genre(1, "Drama"), new Genre(2, "Romance") };
            Film film1 = new(1, "film1", 1999, 6, genres1);
            Film film2 = new(2, "film2", 2022, 9.0, genres1);
            Film film3 = new(3, "film3", 1932, 8, genres2);
            Film film4 = new(4, "film4", 1900, 7.0, genres2);
            Film film5 = new(5, "film5", 1912, 3.4, genres2);
            watchlistData.likedFilms = new() { film1 };
            filmData.films = new() { film1, film2, film3, film4, film5 };

            //Act
            List<Film> result = algorithm.MakeRecommendationList(1, 4);

            //Assert
            Assert.That(result.Count, Is.EqualTo(4));
            Assert.That(result.Distinct().Count, Is.EqualTo(4));
        }

        [Test]
        public void RandomFilmWithHighestRatingGetsRecommendedWhenNoMoreMatchingGenresCanBeFound()
        {
            //Arrange
            List<Genre> genres1 = new() { new Genre(1, "Drama"), new Genre(2, "Crime") };
            List<Genre> genres2 = new() { new Genre(1, "Sci-Fi"), new Genre(2, "Romance") };
            Film film1 = new(1, "film1", 1999, 6, genres1);
            Film film2 = new(2, "film2", 2022, 9.0, genres1);
            Film film3 = new(3, "film3", 1932, 8.2, genres2);
            Film film4 = new(4, "film4", 1932, 8.1, genres2);
            watchlistData.likedFilms = new() { film1 };
            filmData.films = new() { film1, film2, film3, film4};

            //Act
            List<Film> result = algorithm.MakeRecommendationList(1, 2);

            //Assert
            Assert.That(result[0], Is.EqualTo(film2));
            Assert.That(result[1], Is.EqualTo(film3));
            Assert.That(result.Contains(film4), Is.False);
        }

        [Test]
        public void ExceptionIsThrownWhenNoMoreFilmsAreInDatabase()
        {
            //Arrange
            List<Genre> genres1 = new() { new Genre(1, "Drama"), new Genre(2, "Crime") };
            Film film1 = new(1, "film1", 1999, 6, genres1);
            watchlistData.likedFilms = new() { film1 };
            filmData.films = new() { film1 };

            //Assert
            Assert.Throws<IndexOutOfRangeException>(() => algorithm.MakeRecommendationList(1, 3));
        }

        [Test]
        public void ExceptionIsThrownWhenRecommendationRunsOutOfFilms()
        {
            //Arrange
            List<Genre> genres1 = new() { new Genre(1, "Drama"), new Genre(2, "Crime") };
            Film film1 = new(1, "film1", 1999, 6, genres1);
            Film film2 = new(2, "film2", 2022, 9.0, genres1);
            Film film3 = new(3, "film3", 1932, 8.2, genres1);
            watchlistData.likedFilms = new() { film1 };
            filmData.films = new() { film1, film2, film3 };

            //Assert
            Assert.Throws<IndexOutOfRangeException>(() => algorithm.MakeRecommendationList(1, 3));
        }
    }
}