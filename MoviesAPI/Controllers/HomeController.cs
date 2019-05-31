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
        private IMovieRepository movieRepository;
        private IDirectorRepository directorRepository;
        private IGenreRepository genreRepository;

        public HomeController(IMovieRepository _movieRepository, IDirectorRepository _directorRepository, IGenreRepository _genreRepository)
        {
            movieRepository = _movieRepository;
            directorRepository = _directorRepository;
            genreRepository = _genreRepository;
        }

        [HttpGet]
        public IActionResult Index(string sortOrder, string currentFilter, string searchString, int? pageNumber, double? rateFilter)
        {
            ViewBag.Title = "Home";
            ViewData["CurrentSort"] = sortOrder;
            ViewData["TitleSortParm"] = String.IsNullOrEmpty(sortOrder) ? "title_desc" : String.Empty;

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var Movies = movieRepository.GetAllMovies();

            if (!String.IsNullOrEmpty(searchString))
            {
                Movies = Movies.Where(m => m.Title.Contains(searchString) ||
                                        m.Year.ToString().Contains(searchString)
                                        );
            }
            if(rateFilter != null)
            {
                Movies = Movies.Where(m => m.UsersRating >= rateFilter);
                @ViewData["rateFilter"] = rateFilter;
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
            var movie = movieRepository.GetMovie(movieId);
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
            var movie = movieRepository.GetMovie(movieId);
            if (movie == null)
                return View("ErrorPage");

            ViewBag.Director = movie.Director;

            SelectList selectListGenres = new SelectList(genreRepository.GetAllGenres(), "GenreId", "GenreName");
            ViewBag.SelectGenres = selectListGenres;

            SelectList selectListDirectors = new SelectList(directorRepository.GetAllDirectors(), "DirectorId", "Name");
            ViewBag.SelectDirector = selectListDirectors;

            return View("EditMovie", movie);
        }

        [HttpPost]
        [Route("[controller]/Edit/{movieId}")]
        public IActionResult Edit(MovieUpdateVM movieVM)
        {
            if (ModelState.IsValid)
            {
                var updatedMovie = movieRepository.Update(movieVM);
                if (updatedMovie == null)
                    return View("ErrorPage");
            }
            else
                return RedirectToAction("Edit", movieVM.Id);

            return RedirectToAction("View", movieVM.Id);
        }


        [HttpPost]
        public IActionResult Delete(int movieId)
        {
            var movie = movieRepository.Delete(movieId);
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