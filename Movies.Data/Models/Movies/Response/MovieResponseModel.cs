using Movies.Framework.Data.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace Movies.Framework.Models.Movies.Response
{
    
    public class MovieResponseModel
    {
        public int Id { get; set; }
       
        public required string Name { get; set; }
      
        public required string Description { get; set; }
       
        public required DateTime ReleaseDate { get; set; }
       
        [Range(1, 5)]
        public required int Rating { get; set; }
       
        [DataType(DataType.Currency)]
        public required decimal TicketPrice { get; set; }
        
        public required string Country { get; set; }
       
        public required List<Genre> Genres { get; set; }
       
        public required string Photo { get; set; }
    }
}
