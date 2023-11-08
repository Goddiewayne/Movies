using Movies.Framework.Data.Models;
using System;

namespace Movies.Framework.Data
{
    public static class DataSeederHelper
    {
        public static Movie TestMovie1() => new Movie
        {
            Id = 1,
            Name = "TestMovie1",
            Description = "TestDescription1",
            ReleaseDate = DateTime.Now,
            Rating = 1,
            TicketPrice = 500,
            Country = "Nigeria",
            Genres =  new List<Genre>() { },
            Photo = ""
        };

        public static Movie TestMovie2()
        {
            return new Movie
            {
                Id = 2,
                Name = "TestMovie2",
                Description = "TestDescription2",
                ReleaseDate = DateTime.Now,
                Rating = 2,
                TicketPrice = 500,
                Country = "Nigeria",
                Genres = new List<Genre>() { },
                Photo = ""
            };
        }

        public static Movie TestMovie3()
        {
            return new Movie
            {
                Id = 3,
                Name = "TestMovie3",
                Description = "TestDescription3",
                ReleaseDate = DateTime.Now,
                Rating = 3,
                TicketPrice = 500,
                Country = "Nigeria",
                Genres = new List<Genre>() { },
                Photo = ""
            };
        }

        public static Movie TestMovie4()
        {
            return new Movie
            {
                Id = 4,
                Name = "TestMovie4",
                Description = "TestDescription4",
                ReleaseDate = DateTime.Now,
                Rating = 4,
                TicketPrice = 500,
                Country = "Nigeria",
                Genres = new List<Genre>() { },
                Photo = ""
            };
        }
    }
}
