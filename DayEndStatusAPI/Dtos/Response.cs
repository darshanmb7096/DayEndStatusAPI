namespace DayEndStatusAPI.Dtos
{
    public class Response
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="status"></param>
        /// <param name="message"></param>
        /// <param name="data"></param>
        public Response(bool status, string message, object data)
        {
            Status = status;
            Message = message;
            Data = data;
        }
        public bool Status { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}

