using System.Text.RegularExpressions;
using ChristmasApi.Data;
using ChristmasApi.Main.Services;
using ChristmasApi.Main.Validators;
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
    private readonly ICurrentToken currentToken;

    public ChristmasApiController(
        ILightFactory lightFactory, 
        ChristmasApiDbContext dbContext,
        ICurrentToken currentToken)
    {
        this.lightFactory = lightFactory;
        this.dbContext = dbContext;
    }

    [HttpGet]
    public async Task<IActionResult> GetLights()
    {
        return this.Ok(JsonConvert.SerializeObject(this.dbContext.Lights.ToList()));
    }

    [HttpPost]
    public async Task<IActionResult> AddLight([FromBody] DescriptionRequest request, [FromHeader(Name = "Christmas-Token")] string token)
    {
        if (DescriptionValidator.CheckDescription(request.Desc))
        {
            return this.BadRequest(new { error = "Failed to create light" });
        }

        if (string.IsNullOrWhiteSpace(token))
        {
            return this.Unauthorized(new { error = "Missing Christmas-Token header" });
        }

        CurrentToken.UpdateToken(token);

        var light = await this.lightFactory.CreateLightAsync(request.Desc);

        if (light == null)
        {
            return this.BadRequest(new { error = "Failed to create light due to validation errors" });
        }

        this.dbContext.Lights.Add(light);
        this.dbContext.SaveChanges();

        return this.Ok(new { success = true });
    }
}