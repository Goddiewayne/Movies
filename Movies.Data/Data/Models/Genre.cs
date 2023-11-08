using System;
using System.ComponentModel.DataAnnotations;

namespace Movies.Framework.Data.Models
{
    public class Genre
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required List<Movie> Movies { get; set; }
    }
}
