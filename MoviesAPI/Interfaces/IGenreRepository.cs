using MoviesAPI.DbModels;
using MoviesAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Interfaces 
{
    public interface IGenreRepository
    {
        GenreResponse GetGenre(int id);
        IEnumerable<GenreResponse> GetAllGenres();
        GenreResponse Add(GenreRequest genre);
        GenreResponse Update(GenreUpdateVM updatedGenre);
        GenreResponse Delete(int id);
    }
}
