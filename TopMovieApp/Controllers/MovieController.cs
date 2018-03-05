using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TopMovieApp.Models;
using TopMovieApp.DAL;
using PagedList;




namespace TopMovieApp.Controllers
{
        public class MovieController : Controller
    {
        [HttpGet]
        public ActionResult Index(string sortOrder, int? page)
        {
            //
            //instantiate a repository
            //
            MovieRepository movieRepository = new MovieRepository();

            //
            //create a distinct list of total gross for the total gross filter
            //

            ViewBag.totalGross = ListOfTotalGross();

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
                case "Director":
                    movies = movies.OrderBy(movie => movie.Director);
                    break;
                default:
                    movies = movies.OrderBy(movie => movie.Title);
                    break;
            }

            //
            //set parameters and paginate the total gross list
            //
            int pageSize = 50;
            int pageNumber = (page ?? 1);
            movies = movies.ToPagedList(pageNumber, pageSize);

            return View(movies);
         
        }
        [HttpPost]
        public ActionResult Index(string searchCriteria, string totalGrossFilter, int? page)
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

            //
            //if posted with a filter by total gross
            //
            if (totalGrossFilter != "" || totalGrossFilter == null)
            {
                movies = movies.Where(movie => movie.TotalGross == totalGrossFilter);
            }

            //
            //set parameters and paginate the total gross list
            //
            int pageSize = 50;
            int pageNumber = (page ?? 1);
            movies = movies.ToPagedList(pageNumber, pageSize);

            return View(movies);
        }

        // GET: Movie/Details/5
        public ActionResult Details(int id)
        {
            //
            //instantiate a repository
            //
            MovieRepository movieRepository = new MovieRepository();
            Movie movie = new Movie();

            //
            //get a movie that has the matching id
            //
            using (movieRepository)
            {
                movie = movieRepository.SelectOne(id);
            }
            return View(movie);
        }

        // GET: Movie/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Movie/Create
        [HttpPost]
        public ActionResult Create(Movie movie)
        {
            try
            {
                MovieRepository movieRepository = new MovieRepository();

                using (movieRepository)
                {
                    movieRepository.Insert(movie);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            MovieRepository movieRepository = new MovieRepository();
            Movie movie = new Movie();

            using (movieRepository)
            {
                movie = movieRepository.SelectOne(id);
            }
            return View(movie);
        }

        
        [HttpPost]
        public ActionResult Edit(Movie movie)
        {
            try
            {
                MovieRepository movieRepository = new MovieRepository();

                using (movieRepository)
                {
                    movieRepository.Update(movie);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            MovieRepository movieRepository = new MovieRepository();
            Movie movie = new Movie();

            using (movieRepository)
            {
                movie = movieRepository.SelectOne(id);
            }

            return View(movie);
        }

        
        [HttpPost]
        public ActionResult Delete(int id, Movie movie)
        {
            try
            {
                MovieRepository movieRepository = new MovieRepository();

                using (movieRepository)
                {
                    movieRepository.Delete(id);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }        
        [NonAction]
        private IEnumerable<string> ListOfTotalGross()
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
            //get a distinct list of total gross
            //
            var totalGross = movies.Select(movie => movie.TotalGross).Distinct().OrderBy(x => x);

            return totalGross;
        }
    }
}
