using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HelloCore.Controllers
{
  [ApiController]
  [Route("/api/[controller]")]
  public class WeatherForecastController : ControllerBase
  {
    private static readonly string[] Summaries = new[]
    {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
      _logger = logger;
    }

    [HttpGet]
    public ActionResult<IEnumerable<WeatherForecast>> Get()
    {
      var rng = new Random();
      return Enumerable.Range(1, 5).Select(index => new WeatherForecast
      {
        Date = DateTime.Now.AddDays(index),
        TemperatureC = rng.Next(-20, 55),
        Summary = Summaries[rng.Next(Summaries.Length)]
      })
      .ToArray();
    }

    // GET /api/hello?name={name}
    /**
     * curl -X GET https://localhost:5001/api/hello
     */
    [HttpGet]
    [Route("/api/hello")]
    public ActionResult<string> GetHelloWorld([FromQuery(Name = "name")] string name)
    {
      string greeting;
      Console.WriteLine("name=" + name);

      if (name != null)
      {
        greeting = "Hello, " + name;
      }
      else
      {
        greeting = "Hello World!";
      }

      return greeting;
    }

    /**
     * curl -d "name=Duke" -X POST https://localhost:5001/api/hello
     */
    [HttpPost]
    [Route("/api/hello")]
    public ActionResult<string> PostHelloWorld([FromForm(Name = "name")] string user)
    {
      Console.WriteLine("Received POST request with user: " + user);

      return "POST SUCCESS! Received: " + user;
    }

    /**
     * curl -X POST https://localhost:5001/api/user -H "Content-Type: application/json" -d @json.txt
     * 
     * json.txt
     * --------
     * {"name":"Brown"}
     * 
     * OR: Use Postman to call
     */
    [HttpPost]
    [Route("/api/user")]
    public ActionResult<User> PostUser([FromBody] User user)
    {
      Console.WriteLine("Received POST request with user: " + user.Name);

      return user;
    }
  }
}
