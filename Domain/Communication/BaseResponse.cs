using WebApi.Domain.Models;

namespace WebApi.Domain.Communication
{
    public abstract class BaseResponse<Model>
        where Model:AbstractModel
    {
        public bool Success { get; protected set; }
        public string Message { get; protected set; }
        public Model model { get; protected set; }

        public BaseResponse(bool success, string message, Model model)
        {
            this.model = model;
            Success = success;
            Message = message;
        }
    }
}
