using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;

namespace TopMovieApp.Models
{
    public class Movie
    {
        //[Required(ErrorMessage = "A unique Id must be entered.")]
        public int Id { get; set; }
        
        //[Required]
        //[Display(Title = "Movie Title")]
        public string Title { get; set; }

        public string TotalGross { get; set; }

        public int Director { get; set; }
        
    }
}