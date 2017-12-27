using Microsoft.AspNetCore.Mvc;
using OazachaosuCore.Data;
using System.Linq;

namespace OazachaosuCore.Controllers.WordkiApi
{
    public class WordsController : Controller
    {

        public IActionResult Get()
        {
            IActionResult result = null;
            using (var context = new ApplicationDbContext())
            {
                context.Groups.ToList();
                result = new JsonResult(context.Words.ToList());
            }
            return result;
        }

    }
}
