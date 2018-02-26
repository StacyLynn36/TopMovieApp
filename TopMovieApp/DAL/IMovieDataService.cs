using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TopMovieApp.Models;

namespace TopMovieApp.DAL
{   
    
        /// <summary>
        /// data service list to read and write file based on the movie class
        /// </summary>

        public interface IMovieDataService
        {
            List<Movie> Read();
            void Write(List<Movie> Movies);
        }
    
}