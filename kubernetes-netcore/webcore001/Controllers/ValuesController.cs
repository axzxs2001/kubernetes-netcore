using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace webcore001.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {

        [HttpGet]
        public ActionResult Get()
        {
            var osName = Environment.MachineName;
            var version = Environment.OSVersion.VersionString;
            return new JsonResult(new { OsName = osName, Version = version });
        }       

        [HttpGet("/health")]
        public IActionResult Health()
        {
            return Ok();
        }
    }
}
