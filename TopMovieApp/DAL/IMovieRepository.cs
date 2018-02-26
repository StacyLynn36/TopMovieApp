using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TopMovieApp.Models;

namespace TopMovieApp.DAL
{
    public class IMovieRepository
    {
         {
        IEnumerable<Movie> SelectAll();
        Movie SelectOne(int id);
        void Insert(Movie movie);
        void Update(Movie movie);
        void Delete(int id);
    }
}
}