using WebApi.Domain.Models;

namespace WebApi.Domain.Communication
{
    public class ProductResponse : BaseResponse<Product>
    {
        public Product Product { get; private set; }

        /// <summary>
        /// Creates a success response.
        /// </summary>
        /// <param name="product">Saved category.</param>
        /// <returns>Response.</returns>
        public ProductResponse(Product product) : base(true, string.Empty, product)
        { }

        /// <summary>
        /// Creates am error response.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <returns>Response.</returns>
        public ProductResponse(string message) : base(false, message, null)
        { }
    }
}
