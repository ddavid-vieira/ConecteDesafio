using Application.Scheduling.Commands.CreateScheduling;
using Application.Scheduling.Commands.DeleteScheduling;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SchedulingController(ISender sender) : ControllerBase
{
    [HttpPost]
    public async Task<Created<int>> CreateScheduling(CreateSchedulingCommand command)
    {
        var id = await sender.Send(command);

        return TypedResults.Created($"/Scheduling/{id}", id);
    }

    public async Task<NoContent> DeleteScheduling(int id)
    {
        await sender.Send(new DeleteSchedulingCommand(id));
        return TypedResults.NoContent();
    }
}