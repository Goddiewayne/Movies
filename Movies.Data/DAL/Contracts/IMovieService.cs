using Movies.Framework.Models.Movies.Request;
using Movies.Framework.Models.Movies.Response;
using Movies.Framework.Models.Query;
using System.Threading;
using System.Threading.Tasks;

namespace Movies.Framework.DAL.Contracts
{
    public interface IMovieService
    {
        Task<int> CreateMovieAsync(MovieRequestModel request, CancellationToken cancellationToken);

        Task UpdateMovieAsync(int id, MovieRequestModel request, CancellationToken cancellationToken);

        Task<QueryResult<MovieResponseModel>> GetMoviesGridAsync(BasicQuery request, CancellationToken cancellationToken);

        Task<MovieResponseModel> GetMovieDetailsAsync(int id, CancellationToken cancellationToken);

        Task DeleteMovieAsync(int id, CancellationToken cancellationToken);
    }
}
