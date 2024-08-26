using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BuberDinner.Application.Common.Errors;
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

        var (statusCode, message) = exception switch {
            IServiceException serviceException => (serviceException.StatusCode, serviceException.ErrorMessage),
            _ => (HttpStatusCode.InternalServerError, "An unexpected error ocurred."),
        };

            return Problem(statusCode : (int)statusCode, title: message);
        }
    }
}