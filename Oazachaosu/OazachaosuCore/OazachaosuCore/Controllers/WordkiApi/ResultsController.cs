using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OazachaosuCore.Data;
using Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OazachaosuCore.Controllers.WordkiApi
{
    [Route("Results")]
    public class ResultsController : ApiControllerBase
    {
        private IWordkiRepo WordkiRepo { get; set; }

        public ResultsController(IWordkiRepo wordkiRepo) : base()
        {
            WordkiRepo = wordkiRepo;
        }

        [HttpGet("")]
        public IActionResult Get()
        {
            IActionResult result = new JsonResult(WordkiRepo.GetResults());
            return result;
        }

        [HttpGet("/{userId}")]
        public IActionResult Get(long userId)
        {
            IActionResult result = new JsonResult(WordkiRepo.GetResults().Where(x => x.Group.UserId == userId));
            return result;
        }

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            string content = await GetContnet();
            IList<Result> results = JsonConvert.DeserializeObject<List<Result>>(content);
            IQueryable<Result> dbGroups = WordkiRepo.GetResults();
            foreach (Result result in results)
            {
                if (dbGroups.Any(x => x.Id == result.Id))
                {
                    WordkiRepo.UpdateResult(result);
                }
                else
                {
                    WordkiRepo.AddResult(result);
                }
            }
            await WordkiRepo.SaveChangesAsync();
            return new ContentResult()
            {
                Content = "Ok",
            };
        }

    }
}
