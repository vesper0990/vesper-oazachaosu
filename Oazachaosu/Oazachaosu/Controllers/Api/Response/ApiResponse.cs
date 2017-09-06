namespace Oazachaosu.Controllers.Api.Response {
  public class PlainApiResponse : IApiResponse {
    public bool IsError { get; set; }
    public string Message { get; set; }
  }
}