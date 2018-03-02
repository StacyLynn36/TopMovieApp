using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TopMovieApp.Models;
using TopMovieApp.DAL;




namespace TopMovieApp.Controllers
{
        public class MovieController : Controller
    {
        [HttpGet]
        public ActionResult Index(string sortOrder)
        {
            //
            //instantiate a repository
            //
            MovieRepository movieRepository = new MovieRepository();

            //
            //return the data context as an enumerable
            // 

            IEnumerable<Movie> movies;
            using (movieRepository)
            {
                movies = movieRepository.SelectAll() as IList<Movie>;
            }

            switch (sortOrder)
            {
                case "Title":
                    movies = movies.OrderBy(movie => movie.Title);
                    break;
                case "TotalGross":
                    movies = movies.OrderBy(movie => movie.TotalGross);
                    break;
                case "MovieTrailerLink":
                    movies = movies.OrderBy(movie => movie.Director);
                    break;
                default:

                    break;
            }

            return View(movies);
         
        }
        [HttpPost]
        public ActionResult Index(string searchCriteria, string totalGrossFilter)
        {
            //
            //instantiate a repository
            //
            MovieRepository movieRepository = new MovieRepository();

            //
            //return the data context as an enumerable
            //
            IEnumerable<Movie> movies;
            using (movieRepository)
            {
                movies = movieRepository.SelectAll() as IList<Movie>;
            }

            //
            //if posted with a search on movie title
            //
            if (searchCriteria != null)
            {
                movies = movies.Where(movie => movie.Title.ToUpper().Contains(searchCriteria.ToUpper()));
            }
            return View(movies);
        }

        // GET: Movie/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Movie/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Movie/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Movie/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Movie/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Movie/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Movie/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
