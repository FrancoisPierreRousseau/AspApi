using WebApi.Domain.Models;

namespace WebApi.Domain.Communication
{
    public class CategoryResponse : BaseResponse<Category>
    {
        public Category Category { get; private set; }

        /// <summary>
        /// Creates a success response.
        /// </summary>
        /// <param name="category">Saved category.</param>
        /// <returns>Response.</returns>
        public CategoryResponse(Category category) : base(true, string.Empty, category)
        { }

        /// <summary>
        /// Creates am error response.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <returns>Response.</returns>
        public CategoryResponse(string message) : base(false, message, null)
        { }
    }
}
