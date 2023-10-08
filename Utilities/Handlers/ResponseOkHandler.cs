using System.Net;

namespace API.Utilities.Handlers
{

    //class untuk response ok 
    public class ResponseOkHandler<TEntity>

    {
        public int Code { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public TEntity? Data { get; set; }

        //constructor untuk method get
        public ResponseOkHandler (TEntity? data)
        {
            Code = StatusCodes.Status200OK;
            Status = HttpStatusCode.OK.ToString();
            Message = "Success to Retrieve Data";
            Data = data;
        }
        //construktor method update, delete
        public ResponseOkHandler(string message)
        {
            Code = StatusCodes.Status200OK;
            Status = HttpStatusCode.OK.ToString();
            Message = message;
        }

        public ResponseOkHandler(string message, TEntity? data)
        {
            Code = StatusCodes.Status200OK;
            Status = HttpStatusCode.OK.ToString();
            Message = message;
            Data = data;
        }
    }
}
