using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace webAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        public ErrorController() {}

        [HttpGet]
        public ActionResult<string> Get()
        {
            return "an error has ocurred";
        }
    }
}