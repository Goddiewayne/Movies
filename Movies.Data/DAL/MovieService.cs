using Movies.Framework.CustomExceptions;
using Movies.Framework.Models.Movies.Request;
using Movies.Framework.Models.Movies.Response;
using Movies.Framework.Models.Query;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System;
using Movies.Framework.Data.Models;
using Movies.Framework.Data;
using Movies.Framework.Constants;
using System.Diagnostics.Metrics;
using Movies.Framework.Models.Movies;
using Movies.Framework.DAL.Contracts;

namespace Movies.Framework.DAL
{
    public class MovieService : IMovieService
    {
        private readonly MovieContext _context;

        public MovieService(MovieContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <inheritdoc />
        public async Task<int> CreateMovieAsync(MovieRequestModel movieModel, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var movie = new Movie
            {
                Name = movieModel.Name,
                Description = movieModel.Description,
                ReleaseDate = movieModel.ReleaseDate,
                Rating = movieModel.Rating,
                TicketPrice = movieModel.TicketPrice,
                Country = movieModel.Country,
                Genres = movieModel.Genres,
                Photo = movieModel.Photo
            };

            _context.Movies.Add(movie);
            await _context.SaveChangesAsync(cancellationToken);

            return movie.Id;
        }

        /// <inheritdoc />
        public async Task UpdateMovieAsync(int id, MovieRequestModel request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var movie = await GetMovieAsync(id, cancellationToken);

            SetMovieProperties(movie, request);
            await _context.SaveChangesAsync(cancellationToken);
        }

        /// <inheritdoc />
        public async Task<QueryResult<MovieResponseModel>> GetMoviesGridAsync(BasicQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Movies.Select(
                x => new MovieResponseModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    ReleaseDate = x.ReleaseDate,
                    Rating = x.Rating,
                    TicketPrice = x.TicketPrice,
                    Country = x.Country,
                    Genres = x.Genres,
                    Photo = x.Photo
                });

            if (request.Filters != null)
            {
                query = ApplyFilters(request.Filters, query);
            }

            var totalCount = await query.CountAsync(cancellationToken);

            if (request.Paging != null)
            {
                query = ApplyPagination(request.Paging, query);
            }

            if (request.Sort != null)
            {
                query = ApplySorting(request.Sort, query);
            }

            var data = await query.ToListAsync(cancellationToken);

            var queryResult = new QueryResult<MovieResponseModel>
            {
                TotalCount = totalCount,
                Data = data,
                TotalPages = CountTotalPages(totalCount, request.Paging.PageSize)
            };

            return queryResult;
        }

        /// <inheritdoc />
        public async Task<MovieResponseModel> GetMovieDetailsAsync(int id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var movie = await GetMovieAsync(id, cancellationToken);

            var movieDetails = new MovieResponseModel
            {
                Id = movie.Id,
                Name = movie.Name,
                Description = movie.Description,
                ReleaseDate = movie.ReleaseDate,
                Rating = movie.Rating,
                TicketPrice = movie.TicketPrice,
                Country = movie.Country,
                Genres = movie.Genres,
                Photo = movie.Photo
            };

            return movieDetails;
        }

        /// <inheritdoc />
        public async Task DeleteMovieAsync(int id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var movie = await GetMovieAsync(id, cancellationToken);

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync(cancellationToken);
        }

        private async Task<Movie> GetMovieAsync(int id, CancellationToken cancellationToken)
        {
            var movie = await _context.Movies.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (movie == null)
            {
                throw new NotFoundException(string.Format(ErrorMessages.MovieNotFound, id));
            }

            return movie;
        }

        private IQueryable<MovieResponseModel> ApplyFilters(IEnumerable<FilterModel> requestFilters, IQueryable<MovieResponseModel> query)
        {
            foreach (var filter in requestFilters)
            {
                if (filter.Field.ToLower() == EntityColumnNamesConstants.Name.ToLower() && filter.Value != null)
                {
                    query = query.Where(x => x.Name.ToLower().Contains(filter.Value.ToLower()));
                }
                if (filter.Field.ToLower() == EntityColumnNamesConstants.ReleaseDate.ToLower() && filter.Value != null)
                {
                    query = query.Where(x => x.ReleaseDate.Year.ToString().Equals(filter.Value));
                }
                if (filter.Field.ToLower() == EntityColumnNamesConstants.Rating.ToLower() && filter.Value != null)
                {
                    query = query.Where(x => x.Rating.ToString().Equals(filter.Value));
                }
                if (filter.Field.ToLower() == EntityColumnNamesConstants.TicketPrice.ToLower() && filter.Value != null)
                {
                    query = query.Where(x => x.TicketPrice.ToString().Equals(filter.Value));
                }
                if (filter.Field.ToLower() == EntityColumnNamesConstants.Country.ToLower() && filter.Value != null)
                {
                    query = query.Where(x => x.Country.ToString().Equals(filter.Value));
                }
            }

            return query;
        }

        private IQueryable<MovieResponseModel> ApplySorting(SortModel sort, IQueryable<MovieResponseModel> query)
        {
            if (sort.IsDescending)
            {
                if (sort.Field.ToLower() == EntityColumnNamesConstants.Id.ToLower())
                {
                    query = query.OrderByDescending(x => x.Id);
                }
                else if (sort.Field.ToLower() == EntityColumnNamesConstants.Name.ToLower())
                {
                    query = query.OrderByDescending(x => x.Name);
                }
                else if (sort.Field.ToLower() == EntityColumnNamesConstants.ReleaseDate.ToLower())
                {
                    query = query.OrderByDescending(x => x.ReleaseDate);
                }
                else if (sort.Field.ToLower() == EntityColumnNamesConstants.Rating.ToLower())
                {
                    query = query.OrderByDescending(x => x.Rating);
                }
                else if (sort.Field.ToLower() == EntityColumnNamesConstants.TicketPrice.ToLower())
                {
                    query = query.OrderByDescending(x => x.TicketPrice);
                }
                else if (sort.Field.ToLower() == EntityColumnNamesConstants.Country.ToLower())
                {
                    query = query.OrderByDescending(x => x.Country);
                }
            }

            else
            {
                if (sort.Field.ToLower() == EntityColumnNamesConstants.Id.ToLower())
                {
                    query = query.OrderBy(x => x.Id);
                }

                else if (sort.Field.ToLower() == EntityColumnNamesConstants.Name.ToLower())
                {
                    query = query.OrderBy(x => x.Name);
                }
                else if (sort.Field.ToLower() == EntityColumnNamesConstants.ReleaseDate.ToLower())
                {
                    query = query.OrderBy(x => x.ReleaseDate);
                }
                else if (sort.Field.ToLower() == EntityColumnNamesConstants.Rating.ToLower())
                {
                    query = query.OrderBy(x => x.Rating);
                }
                else if (sort.Field.ToLower() == EntityColumnNamesConstants.TicketPrice.ToLower())
                {
                    query = query.OrderBy(x => x.TicketPrice);
                }
                else if (sort.Field.ToLower() == EntityColumnNamesConstants.Country.ToLower())
                {
                    query = query.OrderBy(x => x.Country);
                }
            }

            return query;
        }

        private IQueryable<MovieResponseModel> ApplyPagination(PagingModel pageModel, IQueryable<MovieResponseModel> query)
        {
            return query.Skip((pageModel.CurrentPage - 1) * pageModel.PageSize).Take(pageModel.PageSize);
        }

        private void SetMovieProperties(Movie movie, MovieRequestModel requestModel)
        {
            movie.Name = requestModel.Name;
            movie.Description = requestModel.Description;
            movie.ReleaseDate = requestModel.ReleaseDate;
            movie.Rating = requestModel.Rating;
            movie.TicketPrice = requestModel.TicketPrice;
            movie.Country = requestModel.Country;
            movie.Genres = requestModel.Genres;
            movie.Photo = requestModel.Photo;
        }

        private int CountTotalPages(int totalCount, int pageSize)
        {
            int totalPageCount = totalCount / pageSize;

            if (totalPageCount < 1)
            {
                totalPageCount = 1;
            }

            return totalPageCount;
        }
    }
}
