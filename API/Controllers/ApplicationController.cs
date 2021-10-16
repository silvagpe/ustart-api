using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UStart.Infrastructure.Helpers;

namespace UStart.API.Controllers
{    
    [ApiController]
    [AllowAnonymous]
    [Route("api/app")]
    public class ApplicationController : ControllerBase
    {
        [HttpGet]
        [Route("info")]
        public IActionResult GetInfo()
        {
            return Ok(
                new
                {
                    Version = InfoHelper<ApplicationController>.AssemblyVersion,
                    BuildDateLocal = InfoHelper<ApplicationController>.LocalDate,
                    BuildDateUTC = InfoHelper<ApplicationController>.Date
                });
        }

        [HttpGet]
        [Route("builddate")]
        public IActionResult GetBuildDateLocal()
        {
            return Ok(InfoHelper<ApplicationController>.LocalDate);
        }

        [HttpGet]
        [Route("builddate/utc")]
        public IActionResult GetBuildDateUTC()
        {
            return Ok(InfoHelper<ApplicationController>.Date);
        }

        [HttpGet]
        [Route("version")]
        public IActionResult GetVersion()
        {
            return Ok(InfoHelper<ApplicationController>.AssemblyVersion);
        }

        [HttpGet]
        [Route("status")]
        public IActionResult GetApplicationStatus()
        {
            return Ok("ok");
        }

        [HttpGet]
        [Route("hello")]
        public IActionResult SayHello([FromQuery] string name)
        {
            return Ok($"Hello {name}!");
        }
    }
}
