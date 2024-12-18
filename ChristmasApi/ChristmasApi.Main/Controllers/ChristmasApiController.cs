namespace ChristmasApi.Main.Controllers;

using ChristmasApi.Data.Models;
using ChristmasApi.Main.Contracts;
using ChristmasApi.Main.Requests;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/")]
[EnableCors("AllowSpecificOrigin")]
public class ChristmasApiController : Controller
{
    private readonly ILightFactory lightFactory;

    public ChristmasApiController(ILightFactory lightFactory)
    {
        this.lightFactory = lightFactory;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return this.Ok(new { message = "Christmas API is live!" });
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] DescriptionRequest request)
    {
        if (string.IsNullOrWhiteSpace(request?.Desc))
        {
            return this.BadRequest(new { error = "Description cannot be empty" });
        }

        var response = new
        {
            message = "Success!",
            descr = request.Desc,
        };

        /*var christmasToken = this.Request.Headers["Christmas-Token"].ToString();

        if (string.IsNullOrWhiteSpace(christmasToken))
        {
            return this.Unauthorized(new { error = "Missing Christmas-Token header" });
        }

        var light = await this.lightFactory.CreateLightAsync(request.Desc, christmasToken);

        if (light == null)
        {
            return this.BadRequest(new { error = "Failed to create light due to validation errors" });
        }

        return this.Created(
            "/",
            new
            {
                message = "Light created successfully",
                light,
            });*/
        
        return Ok(response);
    }
}