using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace JWTAuth_Practice.Controllers
{
    //[Authorize(Roles = "HRManager,Finance")] //¾ã­Ócontroller
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
       // private readonly ClaimsPrincipal _user; 

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
            
        }
        [Authorize]//  only action
        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            Console.WriteLine(User.Identity?.Name);
            if (User.Identity?.Name == "Tina")
            {
                return Enumerable.Range(1, 5).Select(index => new WeatherForecast
                            {
                                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                                TemperatureC = Random.Shared.Next(-20, 55),
                                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                            })
                            .ToArray();
            }
            else
            {
                return Enumerable.Empty<WeatherForecast>();
            }

        }
    }
}
