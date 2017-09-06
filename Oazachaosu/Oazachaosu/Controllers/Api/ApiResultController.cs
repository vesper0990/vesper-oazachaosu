using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using Newtonsoft.Json;
using OazachaosuRepository.IRepo;
using OazachaosuRepository.Model;
using System.Threading.Tasks;

namespace Oazachaosu.Controllers.Api {
  public class ApiResultController : MainApiController {

    public ApiResultController(IWordkiRepository wordkiRepository, DbContext dbContext)
      : base(wordkiRepository, dbContext) {
    }

    [Route("api/result/{dateTime}")]
    [Route("api/result/")]
    public ActionResult Get(string dateTime) {
      if (!CheckAuthorization()) {
        return new ApiJsonResult { Data = ApiResponse };
      }
      DateTime date = string.IsNullOrEmpty(dateTime) ? new DateTime(1990, 9, 24) : DateTime.ParseExact(dateTime, "yyyy-MM-ddTHH-mm-ss", CultureInfo.InvariantCulture);
      ApiResponse.IsError = false;
      ApiResponse.Message = JsonConvert.SerializeObject(WordkiRepository.GetResults(UserId).Where(x => x.LastUpdateTime > date));
      return new ApiJsonResult { Data = ApiResponse };
    }

    [HttpPost]
    [Route("api/result")]
    public async Task<ActionResult> Post() {
      if (!CheckAuthorization() || !CheckPostValue()) {
        return new ApiJsonResult { Data = ApiResponse };
      }
      try {
        List<Result> results = JsonConvert.DeserializeObject<List<Result>>(PostString);
        foreach (Result result in results) {
          result.UserId = UserId;
          WordkiRepository.InsertResult(result);
        }
        await WordkiRepository.SaveChangesAsync();
        ApiResponse.IsError = false;
      } catch (Exception e) {
        ApiResponse.Message = e.Message;
      }
      return new ApiJsonResult { Data = ApiResponse };
    }

    [HttpPut]
    [Route("api/result")]
    public async Task<ActionResult> Put() {
      if (!CheckAuthorization() || !CheckPostValue()) {
        return new ApiJsonResult { Data = ApiResponse };
      }
      try {
        List<Result> results = JsonConvert.DeserializeObject<List<Result>>(PostString);
        foreach (Result result in results) {
          result.UserId = UserId;
          WordkiRepository.UpdateResult(result);
        }
        await WordkiRepository.SaveChangesAsync();
        ApiResponse.IsError = false;
      } catch (Exception e) {
        ApiResponse.Message = string.Format("{0} \n\n {1}",e.StackTrace, e.Message);
      }
      return new ApiJsonResult { Data = ApiResponse };
    }

    [HttpDelete]
    [Route("api/result")]
    public async Task<ActionResult> Delete() {
      if (!CheckAuthorization() || !CheckPostValue()) {
        return new ApiJsonResult { Data = ApiResponse };
      }
      try {
        List<Result> results = JsonConvert.DeserializeObject<List<Result>>(PostString);
        foreach (Result result in results) {
          result.UserId = UserId;
          WordkiRepository.DeleteResult(result);
        }
        await WordkiRepository.SaveChangesAsync();
        ApiResponse.IsError = false;
      } catch (Exception e) {
        ApiResponse.Message = e.Message;
      }
      return new ApiJsonResult { Data = ApiResponse };
    }
  }
}