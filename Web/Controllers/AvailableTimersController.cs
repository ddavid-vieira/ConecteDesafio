using Application.AvailableTime.Commands.CreateAvailableTime;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AvailableTimersController(ISender sender) : ControllerBase
{
    [HttpPost]
    public async Task<Created<int>> CreateAvailableTime(CreateAvailableTimeCommand command)
    {
        var id = await sender.Send(command);

        return TypedResults.Created($"/AvailableTimers/{id}", id);
    }
}