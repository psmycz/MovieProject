using MoviesAPI.DbModels;
using MoviesAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Interfaces 
{
    public interface IUserRepository
    {
        UserResponse GetUser(int id);
        IEnumerable<UserResponse> GetAllUsers();
        UserResponse Add(UserRequest user);
        UserResponse Update(UserUpdateVM updatedUser);
        UserResponse Delete(int id);
    }
}
