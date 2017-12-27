using Microsoft.AspNetCore.Mvc;
using System.Linq;
using OazachaosuCore.Data;

namespace OazachaosuCore.Controllers
{
    public class GroupsController : Controller
    {
        public IActionResult Get()
        {
            IActionResult result = null;
            using(var context = new ApplicationDbContext())
            {
                result = new JsonResult(context.Groups.ToList());
            }
            return result;
        }
    }
}
