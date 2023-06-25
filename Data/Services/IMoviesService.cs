using MovieHub.Data.Base;
using MovieHub.Data.ViewModels;
using MovieHub.Models;

namespace MovieHub.Data.Services
{
    public interface IMoviesService:IEntityBaseRepository<Movie>
    {
        Task<Movie> GetMovieByIdAsync(int id);

        Task<NewMovieDropDownsVM> GetNewMovieDropDownsValues();

        Task AddNewMovieAsync(NewMovieVM data);
        Task UpdateMovieAsync(NewMovieVM data);

    }
}
