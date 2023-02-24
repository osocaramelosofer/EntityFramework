using System.Net;

namespace API.Repository.Classes
{
    public class ApiResponse
    {
        public ApiResponse()
        {
            ErrorMessages = new List<string>();
        }

        public List<string> ErrorMessages { get; set; }
        public bool IsSuccess { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string SuccessResponseMessage { get; set; }
        public object Result { get; set; }

    }
}
