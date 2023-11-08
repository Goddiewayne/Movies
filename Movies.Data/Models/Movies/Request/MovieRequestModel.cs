using Movies.Framework.Data.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace Movies.Framework.Models.Movies.Request
{
    public class MovieRequestModel
    {
        [Required]
        public required string Name { get; set; }

        [Required]
        public required string Description { get; set; }

        [Required]
        public required DateTime ReleaseDate { get; set; }

        [Required]
        [Range(1, 5)]
        public required int Rating { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public required decimal TicketPrice { get; set; }

        [Required]
        public required string Country { get; set; }

        [Required]
        public required List<Genre> Genres { get; set; }

        [Required]
        public required string Photo { get; set; }
    }
}
