namespace Movies.Framework.Models.Query
{
    /// <summary>
    /// Model for sorting.
    /// </summary>
    public class SortModel
    {
        /// <summary>
        /// Gets or sets a flag indicating if the sorting order is descending.
        /// </summary>
        /// /// <value>
        /// True if the sorting order is descending; False otherwise.
        /// </value>
        public bool IsDescending { get; set; }

        /// <summary>
        /// Gets or sets the name of filter field.
        /// </summary>
        public string Field { get; set; }
    }
}
