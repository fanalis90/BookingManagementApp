using System.Net;

namespace API.Utilities.Handlers.Exceptions
{
    public class ResponseBadRequestHandler : ResponseErrorHandler
    {
        public ResponseBadRequestHandler(string message)
        {
            Code = StatusCodes.Status400BadRequest;
            Status = HttpStatusCode.BadRequest.ToString();
            Message = message;
        }
    }
}
