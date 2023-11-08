using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Movies.Framework.DAL.Contracts;
using Movies.Framework.Data;
using Movies.Framework.Data.Models;
using Movies.Framework.Models.Movies;
using Movies.Framework.Models.Movies.Request;
using Movies.Framework.Models.Movies.Response;
using Movies.Framework.Models.Query;

namespace Movies.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;
        private readonly IMemoryCache _cache;
        private readonly ILogger _logger;

        public MoviesController(IMovieService movieService, ILogger<MoviesController> logger, IMemoryCache cache)
        {
            _movieService = movieService ?? throw new ArgumentNullException(nameof(movieService));
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost]
        public async Task<ActionResult> CreateMovieAsync(MovieRequestModel request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Call made to CreateMovieAsync.");

            var id = await _movieService.CreateMovieAsync(request, cancellationToken);

            return new ObjectResult(id) { StatusCode = StatusCodes.Status201Created };
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateMovieAsync(int id, MovieRequestModel request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Call made to UpdateMovieAsync.");

            await _movieService.UpdateMovieAsync(id, request, cancellationToken);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<QueryResult<MovieResponseModel>>> GetMovieGridAsync(BasicQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Call made to GetMovieGridAsync.");

            return await _movieService.GetMoviesGridAsync(request, cancellationToken);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MovieResponseModel>> GetMovieDetailsAsync(int id, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Call made to GetMovieDetailsAsync");

            return await CacheDetailsResponse(id, cancellationToken);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMovieAsync(int id, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Call made to DeleteMovieAsync.");

            await _movieService.DeleteMovieAsync(id, cancellationToken);
            return Ok();
        }

        private async Task<MovieResponseModel> CacheDetailsResponse(int id, CancellationToken cancellationToken)
        {
            var cachedResponse = await _cache.GetOrCreateAsync<MovieResponseModel>("DetailsResponse", async (cacheEntry) =>
            {
                cacheEntry.SlidingExpiration = TimeSpan.FromMinutes(1);
                var response = await _movieService.GetMovieDetailsAsync(id, cancellationToken);
                return response;
            });

            return cachedResponse;
        }
    }
}