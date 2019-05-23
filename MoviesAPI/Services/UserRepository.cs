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
    public class UserRepository : IUserRepository
    {
        private readonly MovieAPIDbContext context;

        public UserRepository(MovieAPIDbContext context)
        {
            this.context = context;
        }

        public UserResponse Add(UserRequest userRequest)
        {

            User user= new User()
            {
                Name = userRequest.Name,
                Surname = userRequest.Surname
            };

            context.Users.Add(user);

            context.SaveChanges();

            if (userRequest.Movies != null)
            {
                var existingMovies = context.Movies.Select(m => m.Id).ToList();
                foreach (var movieId in userRequest.Movies)
                {
                    if (existingMovies.Contains(movieId))
                    {
                        context.MovieUsers.Add(new MovieUser() { MovieId = movieId, UserId = user.UserId });
                        context.SaveChanges();
                    }
                }
            }

            UserResponse userResponse = GetUser(user.UserId);
            
            return userResponse;
        }

        public UserResponse Delete(int id)
        {
            User user = context.Users.Find(id);
            if (user == null)
                return null;
            context.MovieUsers.RemoveRange(user.MovieUsers);

            UserResponse userResponse = GetUser(id);

            context.Users.Remove(user);
            context.SaveChanges();

            return userResponse;
        }

        public IEnumerable<UserResponse> GetAllUsers()
        {
            IEnumerable<UserResponse> users = (from u in context.Users
                                               select new UserResponse
                                               {
                                                   UserId = u.UserId,
                                                   Name = u.Name,
                                                   Surname = u.Surname,
                                                   Movies = u.MovieUsers.Select(mu => mu.Movie).ToList(),
                                                   Reviews = u.UReviews.ToList()
                                               });
            return users;
        }

        public UserResponse GetUser(int id)
        {
            UserResponse userResponse = (from u in context.Users
                                         where u.UserId== id
                                         select new UserResponse
                                         {
                                             UserId = u.UserId,
                                             Name = u.Name,
                                             Surname = u.Surname,
                                             Movies = u.MovieUsers.Select(mu => mu.Movie).ToList(),
                                             Reviews = u.UReviews.ToList()
                                         }).SingleOrDefault();
            return userResponse;
        }

        public UserResponse Update(UserUpdateVM updatedUser)
        {
            var user = context.Users.Find(updatedUser.UserId);
            if (user == null)
                return null;

            user.Name = updatedUser.Name;
            user.Surname = updatedUser.Surname;

            context.MovieUsers.RemoveRange(context.MovieUsers.Where(mu => mu.UserId == updatedUser.UserId));
            context.SaveChanges();

            if (updatedUser.Movies != null)
            {
                var existingMovies = context.Movies.Select(m => m.Id).ToList();
                foreach (var movieId in updatedUser.Movies)
                {
                    if (existingMovies.Contains(movieId))
                    {
                        context.MovieUsers.Add(new MovieUser() { MovieId = movieId, UserId = user.UserId });
                        context.SaveChanges();
                    }
                }
            }

            return GetUser(user.UserId);
        }
    }
}
