using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.SqlQuery
{
    public class GetDataForMovie
    {
        public int Id { get; private set; }
        public string Title { get; private set; }
        public int Year { get; private set; }
        public double? UsersRating { get; private set; }
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public string GenreName { get; private set; }
        public string Comment { get; private set; }
        public int Rate { get; private set; }
        public string Username { get; private set; }
    }
}
