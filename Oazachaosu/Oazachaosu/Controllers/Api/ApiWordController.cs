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

namespace Oazachaosu.Controllers.Api
{
    public class ApiWordController : MainApiController
    {

        public ApiWordController(IWordkiRepository wordkiRepository, DbContext dbContext)
          : base(wordkiRepository, dbContext)
        {
        }

        [Route("api/word/{dateTime}")]
        [Route("api/word/")]
        public ActionResult Get(string dateTime)
        {
            if (!CheckAuthorization())
            {
                return new ApiJsonResult { Data = ApiResponse };
            }
            DateTime date = string.IsNullOrEmpty(dateTime) ? new DateTime(1990, 9, 24) : DateTime.ParseExact(dateTime, "yyyy-MM-ddTHH-mm-ss", CultureInfo.InvariantCulture);
            ApiResponse.IsError = false;
            ApiResponse.Message = JsonConvert.SerializeObject(WordkiRepository.GetWords(UserId).Where(x => x.LastUpdateTime > date));
            return new ApiJsonResult { Data = ApiResponse };
        }


        [Route("api/words/{groupId}")]
        public ActionResult Get(long? groupId)
        {
            if (!groupId.HasValue)
            {
                ApiResponse.IsError = true;
                return new ApiJsonResult { Data = ApiResponse };
            }
            if (!CheckAuthorization())
            {
                return new ApiJsonResult { Data = ApiResponse };
            }
            ApiResponse.IsError = false;
            ApiResponse.Message = JsonConvert.SerializeObject(WordkiRepository.GetWords(UserId).Where(x => x.GroupId == groupId.Value), new FloatToStringConverter());
            return new ApiJsonResult { Data = ApiResponse };
        }

        public class FloatToStringConverter : JsonConverter
        {

            public override bool CanConvert(Type objectType)
            {
                return typeof(long).Equals(objectType);
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                throw new NotImplementedException();
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                writer.WriteValue(value.ToString());
            }
        }

        [HttpPost]
        [Route("api/word")]
        public async Task<ActionResult> Post()
        {
            if (!CheckAuthorization() || !CheckPostValue())
            {
                return new ApiJsonResult { Data = ApiResponse };
            }
            long ticks = DateTime.Now.Ticks;
            try
            {
                List<Word> words = JsonConvert.DeserializeObject<List<Word>>(PostString);
                foreach (Word word in words)
                {
                    word.Id = ticks++;
                    word.UserId = UserId;
                    WordkiRepository.InsertWord(word);
                }
                try
                {
                    await WordkiRepository.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    ApiResponse.Message = e.Message;
                }
                ApiResponse.IsError = false;
            }
            catch (Exception e)
            {
                ApiResponse.Message = e.Message;
            }
            return new ApiJsonResult { Data = ApiResponse };
        }

        [HttpPut]
        [Route("api/word")]
        public async Task<ActionResult> Put()
        {
            if (!CheckAuthorization() || !CheckPostValue())
            {
                return new ApiJsonResult { Data = ApiResponse };
            }
            try
            {
                List<Word> words = JsonConvert.DeserializeObject<List<Word>>(PostString);
                foreach (Word word in words)
                {
                    word.UserId = UserId;
                    WordkiRepository.UpdateWord(word);
                }
                await WordkiRepository.SaveChangesAsync();
                ApiResponse.IsError = false;
            }
            catch (Exception e)
            {
                ApiResponse.Message = string.Format("{0} \n\n {1}", e.StackTrace, e.Message);
            }
            return new ApiJsonResult { Data = ApiResponse };
        }

        [HttpDelete]
        [Route("api/word")]
        public async Task<ActionResult> Delete()
        {
            if (!CheckAuthorization() || !CheckPostValue())
            {
                return new ApiJsonResult { Data = ApiResponse };
            }
            try
            {
                List<Word> words = JsonConvert.DeserializeObject<List<Word>>(PostString);
                foreach (Word word in words)
                {
                    word.UserId = UserId;
                    WordkiRepository.DeleteWord(word);
                }
                await WordkiRepository.SaveChangesAsync();
                ApiResponse.IsError = false;
            }
            catch (Exception e)
            {
                ApiResponse.Message = e.Message;
            }
            return new ApiJsonResult { Data = ApiResponse };
        }
    }
}