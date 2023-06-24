using Microsoft.EntityFrameworkCore;
using MovieHub.Data.Base;
using MovieHub.Models;

namespace MovieHub.Data.Services
{
    public class MoviesService :EntityBaseRepository<Movie>, IMoviesService
    {
        
        public MoviesService(AppDbContext context):base(context)
        {
           
        }
        
    }
}
