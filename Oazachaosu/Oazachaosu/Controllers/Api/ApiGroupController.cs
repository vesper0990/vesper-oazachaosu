using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Newtonsoft.Json;
using OazachaosuRepository.IRepo;
using System.Data.Entity;
using OazachaosuRepository.Model;

namespace Oazachaosu.Controllers.Api {
  public class ApiGroupController : MainApiController {

    public ApiGroupController(IWordkiRepository wordkiRepository, DbContext dbContext)
      : base(wordkiRepository, dbContext) {
    }

    [HttpGet]
    [Route("api/group/{dateTime}")]
    [Route("api/group")]
    public ActionResult Get(string dateTime) {
      if (!CheckAuthorization()) {
        return new ApiJsonResult { Data = ApiResponse };
      }
      DateTime date = string.IsNullOrEmpty(dateTime) ? new DateTime(1990, 9, 24) : DateTime.ParseExact(dateTime, "yyyy-MM-ddTHH-mm-ss", CultureInfo.InvariantCulture);
      ApiResponse.IsError = false;
      ApiResponse.Message = JsonConvert.SerializeObject(WordkiRepository.GetGroups(UserId).Where(x => x.LastUpdateTime > date));
      return new ApiJsonResult { Data = ApiResponse };
    }

    [HttpPost]
    [Route("api/group")]
    public async Task<ActionResult> Post() {
      if (!CheckAuthorization() || !CheckPostValue()) {
        return new ApiJsonResult { Data = ApiResponse };
      }
      try {
        List<Group> groups = JsonConvert.DeserializeObject<List<Group>>(PostString);
        foreach (var group in groups) {
          if (group.Id == 0) {
            group.Id = DateTime.Now.Ticks;
          }
          group.UserId = UserId;
          WordkiRepository.InsertGroup(group);
        }
        await WordkiRepository.SaveChangesAsync();
        ApiResponse.IsError = false;
        ApiResponse.Message = groups.First().Id.ToString();
      } catch (Exception e) {
        ApiResponse.Message = e.Message;
      }
      return new ApiJsonResult { Data = ApiResponse };
    }

    [HttpPut]
    [Route("api/group")]
    public async Task<ActionResult> Put() {
      if (!CheckAuthorization() || !CheckPostValue()) {
        return new ApiJsonResult { Data = ApiResponse };
      }
      try {
        List<Group> groups = JsonConvert.DeserializeObject<List<Group>>(PostString);
        foreach (var group in groups) {
          group.UserId = UserId;
          WordkiRepository.UpdateGroup(group);
        }
        await WordkiRepository.SaveChangesAsync();
        ApiResponse.IsError = false;
      } catch (Exception e) {
        ApiResponse.Message = e.Message;
      }
      return new ApiJsonResult { Data = ApiResponse };
    }

    [HttpDelete]
    [Route("api/group")]
    public async Task<ActionResult> Delete() {
      if (!CheckAuthorization() || !CheckPostValue()) {
        return new ApiJsonResult { Data = ApiResponse };
      }
      try {
        List<Group> groups = JsonConvert.DeserializeObject<List<Group>>(PostString);
        foreach (var group in groups) {
          group.LastUpdateTime = DateTime.Now;
          group.UserId = UserId;
          WordkiRepository.DeleteGroup(group);
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