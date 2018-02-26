using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TopMovieApp.Models;

namespace TopMovieApp.DAL
{
    public class MovieRepository : IMovieRepository, IDisposable
    {
        private List<Movie> _movies;

        public MovieRepository()
        {
            MovieXmlDataService movieXmlDataService = new MovieXmlDataService();

            using (movieXmlDataService)
            {
                _movies = movieXmlDataService.Read() as List<Movie>;
            }
        }

        public IEnumerable<Movie> SelectAll()
        {
            return _movies;
        }

        public Movie SelectOne(int id)
        {          

            Movie selectedMovie = _movies.Where(p => p.Id == id).FirstOrDefault();

            return selectedMovie;
        }

        public void Insert(Movie movie)
        {
            movie.Id = NextIdValue();
            _movies.Add(movie);

            Save();
        }

        private int NextIdValue()
        {
            int currentMaxId = _movies.OrderByDescending(b => b.Id).FirstOrDefault().Id;
            return currentMaxId + 1;
        }

        public void Update(Movie UpdateMovie)
        {
            var oldMovie = _movies.Where(b => b.Id == UpdateMovie.Id).FirstOrDefault();

            if (oldMovie != null)
            {
                _movies.Remove(oldMovie);
                _movies.Add(UpdateMovie);
            }

            Save();
        }
        public void Delete(int id)
        {
            var movie = _movies.Where(b => b.Id == id).FirstOrDefault();

            if (movie != null)
            {
                _movies.Remove(movie);
            }

            Save();
        }

        public void Save()
        {
            MovieXmlDataService movieXmlDataService = new MovieXmlDataService();

            using (movieXmlDataService)
            {
                movieXmlDataService.Write(_movies);
            }
        }

        public void Dispose()
        {
            _movies = null;
        }
    }
}