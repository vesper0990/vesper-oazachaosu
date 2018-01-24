namespace Oazachaosu.Api.Helpers.Respone
{
    public class ApiResult
    {
        public object Object { get; set; }
        public ResultCode Code { get; set; }
        public string Message { get; set; }
    }
}
