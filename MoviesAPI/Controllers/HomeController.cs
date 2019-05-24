using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        public IActionResult Index()
        {
            ViewBag.Title = "Home";

            var Movies = MovieRepository.GetAllMovies();

            return View(Movies);
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