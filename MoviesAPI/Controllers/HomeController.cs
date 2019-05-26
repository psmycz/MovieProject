using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MoviesAPI.Common;
using MoviesAPI.Interfaces;
using MoviesAPI.Models;

namespace MoviesAPI.Controllers
{
    
    public class HomeController : Controller
    {
        private IMovieRepository MovieRepository;
        private IDirectorRepository DirectorRepository;
        private IGenreRepository GenreRepository;

        public HomeController(IMovieRepository movieRepository, IDirectorRepository directorRepository, IGenreRepository genreRepository)
        {
            MovieRepository = movieRepository;
            DirectorRepository = directorRepository;
            GenreRepository = genreRepository;
        }

        [HttpGet]
        public IActionResult Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewBag.Title = "Home";
            ViewData["CurrentSort"] = sortOrder;
            ViewData["TitleSortParm"] = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var Movies = MovieRepository.GetAllMovies();

            if (!String.IsNullOrEmpty(searchString))
            {
                Movies = Movies.Where(m => m.Title.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "title_desc":
                    Movies = Movies.OrderByDescending(m => m.Title);
                    break;

                default:
                    Movies = Movies.OrderBy(m => m.Title);
                    break;
            }

            int pageSize = 8;
            return View(PaginatedList<MovieResponse>.Create(Movies.AsQueryable(), pageNumber ?? 1, pageSize));
        }

        [HttpGet]
        [Route("[controller]/View/{movieId}")]
        public IActionResult View(int movieId)
        {
            var movie = MovieRepository.GetMovie(movieId);
            if (movie == null)
                return View("ErrorPage");

            ViewBag.Director = movie.Director;
            ViewBag.Users = movie.Users.ToList();
            ViewBag.Reviews = movie.Reviews.ToList();

            return View(movie);
        }

        [HttpGet]
        [Route("[controller]/Edit/{movieId}")]
        public IActionResult Edit(int movieId)
        {
            var movie = MovieRepository.GetMovie(movieId);
            if (movie == null)
                return View("ErrorPage");

            ViewBag.Director = movie.Director;

            SelectList selectList = new SelectList(DirectorRepository.GetAllDirectors(), "DirectorId", "Name");
            ViewBag.SelectDirector = selectList;

            return View("EditMovie", movie);
        }

        [HttpPost]
        [Route("[controller]/Edit/{movieId}")]
        public IActionResult Edit(MovieUpdateVM movieVM)
        {
            
                var updatedMovie = MovieRepository.Update(movieVM);
                if (updatedMovie == null)
                    return View("ErrorPage");
            
            return View("Index", MovieRepository.GetAllMovies());
        }


        [HttpPost]
        public IActionResult Delete(int movieId)
        {
            var movie = MovieRepository.Delete(movieId);
            if (movie == null)
                return View("ErrorPage");

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult About()
        {
            ViewBag.Title = "About";

            return View();
        }
    }
}