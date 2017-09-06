namespace Oazachaosu.Controllers.Api.Response {
  public interface IApiResponse {
    bool IsError { get; set; }
    string Message { get; set; }
  }
}