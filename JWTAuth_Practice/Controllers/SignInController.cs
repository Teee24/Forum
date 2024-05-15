using JWTAuth_Practice.JwtToken;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using JWTAuth_Practice.LogIn;
using System.Security.Claims;

namespace JWTAuth_Practice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignInController : ControllerBase
    {
        private readonly JWTHelper _jwt;
        public SignInController(JWTHelper jwt)
        {
            _jwt = jwt;
        }

        [AllowAnonymous]
        [HttpPost]
        public IResult SignIn([FromBody] LogInRequest request)
        {
            if (!String.IsNullOrEmpty( request.Username)&& !String.IsNullOrEmpty(request.Password))
            {
                var token = _jwt.GenerateToken(request.Username);
                return Results.Ok(new {token});
            }
            else
            {
                return Results.BadRequest();
            }
        }
       
    }
}
