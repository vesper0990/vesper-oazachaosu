using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OazachaosuCore.Controllers
{
    [Route("Groups")]
    public class GroupsController : ApiControllerBase
    {
        private IWordkiRepo Repository { get; set; }

        public GroupsController(IWordkiRepo wordkiRepo) : base()
        {
            Repository = wordkiRepo;
        }

        [HttpGet("")]
        public IActionResult Get()
        {
            var list = Repository.GetGroups();
            IActionResult result = new JsonResult(list);
            return result;
        }

        [HttpGet("{userId}")]
        public IActionResult Get(long userId)
        {
            IActionResult result = new JsonResult(Repository.GetGroups(userId));
            return result;
        }

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            string content = await GetContnet();
            IList<Group> groups = JsonConvert.DeserializeObject<List<Group>>(content);
            IQueryable<Group> dbGroups = Repository.GetGroups();
            foreach (Group group in groups)
            {
                if (dbGroups.Any(x => x.Id == group.Id))
                {
                    Repository.UpdateGroup(group);
                }
                else
                {
                    Repository.AddGroup(group);
                }
            }
            await Repository.SaveChangesAsync();
            return new ContentResult()
            {
                Content = "Ok",
            };
        }
    }
}
