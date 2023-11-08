namespace Movies.Framework.Models.Query
{
    /// <summary>
    /// Model for filtering.
    /// </summary>
    public class FilterModel
    {
        /// <summary>
        /// Gets or sets the name of filter field.
        /// </summary>
        public string Field { get; set; }

        /// <summary>
        /// Gets or sets the filter value.
        /// </summary>
        public string Value { get; set; }
    }
}
