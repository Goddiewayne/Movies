using System.Collections.Generic;

namespace Movies.Framework.Models.Query
{
    public class QueryResult<T>
    {
        public int TotalPages { get; set; }

        public int TotalCount { get; set; }

        public IEnumerable<T> Data { get; set; }

       
    }
}
