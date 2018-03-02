using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TopMovieApp.Models
{
    public class Movie
    {
        public int Id { get; set; }
        
        public string Title { get; set; }

        public string TotalGross { get; set; }
        public string Director { get; set; }
    }
}