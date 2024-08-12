using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.API.Controllers
{
    [ApiController]
    [Route("/error")]
    public class ErrorsController : ControllerBase
    {
        public IActionResult Error(){
            Exception? exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
            return Problem();
        }
    }
}