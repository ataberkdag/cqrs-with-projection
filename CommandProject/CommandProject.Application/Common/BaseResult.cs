namespace CommandProject.Application.Common
{
    public class BaseResult
    {
        public bool Success { get; set; }
        public string ResponseCode { get; set; }
        public string ResponseMessage { get; set; }

        public BaseResult(bool success, string responseCode, string responseMessage)
        {
            Success = success;
            ResponseCode = responseCode;
            ResponseMessage = responseMessage;
        }

        public static BaseResult Succeeded()
        {
            return new BaseResult(true, "0000", "Succeeded");
        }

        public static BaseResult Failure(string responseCode, string responseMessage)
        {
            return new BaseResult(false, responseCode, responseMessage);
        }
    }


    public class BaseResult<T> : BaseResult
    {
        public T Data { get; set; }

        private BaseResult(bool success, string responseCode, string responseMessage, T data) : base(success, responseCode, responseMessage)
        {
            Data = data;
        }

        public static BaseResult<T> Succeeded(T data)
        {
            return new BaseResult<T>(true, "0000", "Succeeded", data);
        }
    }
}
