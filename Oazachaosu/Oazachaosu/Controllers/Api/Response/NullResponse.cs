namespace Oazachaosu.Controllers.Api.Response {
  public class NullResponse : IApiResponse {

    private NullResponse Instance { get; set; }

    private NullResponse() {
    }

    public NullResponse GetInstance() {
      return Instance ?? (Instance = new NullResponse());
    }

    public bool IsError {
      get { return false; }
      set { }
    }
    public string Message {
      get { return string.Empty; }
      set { }
    }
  }
}