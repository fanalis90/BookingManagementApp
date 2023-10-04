using System.Net;

namespace API.Utilities.Handlers.Exceptions
{
    //kelas khusus untuk notfound error
    public class ResponseNotFoundHandler : ResponseErrorHandler
    {
        public ResponseNotFoundHandler(string message) {
            Code = StatusCodes.Status404NotFound;
            Status = HttpStatusCode.NotFound.ToString();
            Message = message;
        }
    }
}
