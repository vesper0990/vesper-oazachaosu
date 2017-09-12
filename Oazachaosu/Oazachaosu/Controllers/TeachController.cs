using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Oazachaosu.Controllers
{
    public class TeachController : Controller
    {

        // GET: Teach
        [Route("teach/{groupId}", Name = "TeachIndex")]
        public ActionResult Index(int? groupId)
        {
            return View();
        }
    }
}