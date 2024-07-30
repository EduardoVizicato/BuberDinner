using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuberDinner.Application.Services.Authentication;
using BurberDinner.Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.API.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthenticationController(IAuthenticationService authenticationService) : ControllerBase
    {
        private readonly IAuthenticationService _service = authenticationService;

        [HttpPost("register")]
        public IActionResult Register(RegisterRequest request){

            var authResult = _service.Register(
                request.FirstName,
                request.LastName,
                request.Email,
                request.Password
            );
            var response = new AuthenticationResponse(
                authResult.Id,
                authResult.FirstName,
                authResult.LastName,
                authResult.Email,
                authResult.Token

            );
            return Ok(response);
        }

        [HttpPost("login")]
        public IActionResult Login(LoginRequest request){

                var authResult = _service.Login(
                request.Email,
                request.Password
            );
            var response = new AuthenticationResponse(
                authResult.Id,
                authResult.FirstName,
                authResult.LastName,
                authResult.Email,
                authResult.Token

            );
            return Ok(response);;
        }
    }
}