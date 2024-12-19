using ChristmasApi.Data;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

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
    private readonly ChristmasApiDbContext dbContext;

    public ChristmasApiController(ILightFactory lightFactory, ChristmasApiDbContext dbContext)
    {
        this.lightFactory = lightFactory;
        this.dbContext = dbContext;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return this.Ok(JsonConvert.SerializeObject(this.dbContext.Lights.ToList()));
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] DescriptionRequest request)
    {
        if (string.IsNullOrWhiteSpace(request?.Desc))
        {
            return this.BadRequest(new { error = "Description cannot be empty" });
        }

        var christmasToken = this.Request.Headers["Christmas-Token"].ToString();

        if (string.IsNullOrWhiteSpace(christmasToken))
        {
            return this.Unauthorized(new { error = "Missing Christmas-Token header" });
        }

        var light = await this.lightFactory.CreateLightAsync(request.Desc, christmasToken);

        if (light == null)
        {
            return this.BadRequest(new { error = "Failed to create light due to validation errors" });
        }

        dbContext.Lights.Add(light);
        dbContext.SaveChanges();

        return this.Ok(new { success = true });

    }
}