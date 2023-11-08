using System.Collections.Generic;

namespace Movies.Framework.Models.Query
{
    /// <summary>
    /// Model for filtering, sorting and pagination.
    /// </summary>
    public class BasicQuery
    {
        /// <summary>
        /// Gets or sets pagination model.
        /// </summary>
        public PagingModel Paging { get; set; }

        /// <summary>
        /// Gets or sets sort model.
        /// </summary>
        public SortModel Sort { get; set; }

        /// <summary>
        /// Gets or sets filter model.
        /// </summary>
        public IEnumerable<FilterModel> Filters { get; set; }
    }
}
