using MoviesAPI.DbModels;
using MoviesAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Interfaces 
{
    public interface IDirectorRepository
    {
        DirectorResponse GetDirector(int id);
        IEnumerable<DirectorResponse> GetAllDirectors();
        DirectorResponse Add(DirectorRequest director);
        DirectorResponse Update(DirectorUpdateVM updatedDirector);
        DirectorResponse Delete(int id);
    }
}
