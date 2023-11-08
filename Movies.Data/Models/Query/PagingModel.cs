namespace Movies.Framework.Models.Query
{
    /// <summary>
    /// Model for pagination.
    /// </summary>
    public class PagingModel
    {
        /// <summary>
        /// Gets or sets the current page.
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// Gets or sets the page size.
        /// </summary>
        public int PageSize { get; set; }
    }
}
