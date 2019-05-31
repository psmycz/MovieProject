using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.DbModels;
using MoviesAPI.Interfaces;
using MoviesAPI.Models;

namespace MoviesAPI.Services
{
    public class DirectorRepository : IDirectorRepository
    {
        private readonly MovieAPIDbContext context;

        public DirectorRepository(MovieAPIDbContext context)
        {
            this.context = context;
        }

        public DirectorResponse Add(DirectorRequest directorRequest)
        {

            Director director = new Director()
            {
                Name = directorRequest.Name,
                Surname = directorRequest.Surname,
            };

            context.Directors.Add(director);
            context.SaveChanges();

            DirectorResponse directorResponse = GetDirector(director.DirectorId);
            
            return directorResponse;
        }

        public DirectorResponse Delete(int id)
        {
            Director director = context.Directors.Find(id);
            if (director == null)
                return null;

            foreach(var m in director.DirectorMovies)
            {
                m.DirectorId = null;
            }

            DirectorResponse directorResponse = GetDirector(id);

            context.Directors.Remove(director);
            context.SaveChanges();

            return directorResponse;
        }

        public IEnumerable<DirectorResponse> GetAllDirectors()
        {
            IEnumerable<DirectorResponse> directors = (from d in context.Directors
                                                       select new DirectorResponse
                                                       {
                                                           DirectorId = d.DirectorId,
                                                           Name = d.Name,
                                                           Surname = d.Surname,
                                                           Movies = d.DirectorMovies.ToList()
                                                       });
            return directors;
        }

        public DirectorResponse GetDirector(int id)
        {
            DirectorResponse directorResponse = (from d in context.Directors
                                                 where d.DirectorId == id
                                                 select new DirectorResponse
                                                 {
                                                     DirectorId = d.DirectorId,
                                                     Name = d.Name,
                                                     Surname = d.Surname,
                                                     Movies = d.DirectorMovies.ToList()
                                                 }).SingleOrDefault();
            return directorResponse;
        }

        public DirectorResponse Update(DirectorUpdateVM updatedDirector)
        {
            var director = context.Directors.Find(updatedDirector.DirectorId);
            if (director == null)
                return null;

            director.Name = updatedDirector.Name;
            director.Surname = updatedDirector.Surname;

            context.SaveChanges();

            return GetDirector(director.DirectorId);
        }
    }
}
