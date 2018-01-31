using Microsoft.AspNetCore.Mvc;
using System;

namespace Oazachaosu.Api.Controllers
{
    [Route("[controller]")]
    public class DateTimeController : ApiControllerBase
    {

        public IActionResult Get()
        {
            return Json(DateTime.Now);
        }

    }
}
