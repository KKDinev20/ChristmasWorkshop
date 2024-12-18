using Microsoft.AspNetCore.Cors;

namespace ChristmasApi.Main.Controllers;

using ChristmasApi.Main.Requests;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/")]
[EnableCors("AllowSpecificOrigin")]
public class ChristmasApiController : Controller
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new { message = "Christmas API" });
    }

    [HttpPost]
    public IActionResult Post(DescriptionRequest request)
    {
        if (string.IsNullOrWhiteSpace(request?.Desc))
        {
            return BadRequest();
        }

        var response = new
        {
            Message = "Description received",
            Description = request.Desc,
        };

        return Ok(response);
    }
}