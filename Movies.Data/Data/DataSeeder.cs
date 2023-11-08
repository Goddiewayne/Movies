using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Movies.Framework.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Movies.Framework.Data
{
    public static class DataSeeder
    {
        public static void SeedData(MovieContext context)
        {
            // Look for any movies.
            if (context.Movies.Any())
            {
                return;   // Data is already seeded
            }

            var movies = new List<Movie>
                {
                    DataSeederHelper.TestMovie1(),
                    DataSeederHelper.TestMovie2(),
                    DataSeederHelper.TestMovie3(),
                    DataSeederHelper.TestMovie4(),
                };

            context.Movies.AddRange(movies);
            context.SaveChanges();
        }
    }
}
