using Application.AvailableTime.Commands.CreateAvailableTime;
using Application.AvailableTime.Commands.DeleteAvailableTime;
using Application.AvailableTime.Queries.GetAvailableTimesWithPaginationGroupedByDoctor;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Common.Models.AvailableTime;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AvailableTimersController(ISender sender, IUser user) : ControllerBase
{
    [HttpGet]
    public async Task<Ok<PaginatedList<AvailableTimeGroupedByDoctorDto>>> GetAvailableTimesWithPagination(
        [FromQuery] GetAvailableTimeGroupedByDoctorQuery query)
    {
        var result = await sender.Send(query);

        return TypedResults.Ok(result);
    }

    [HttpPost]
    public async Task<Created<int>> CreateAvailableTime(CreateAvailableTimeCommand command)
    {
        var id = await sender.Send(command);

        return TypedResults.Created($"/AvailableTimers/{id}", id);
    }

    [HttpDelete("{id:int}")]
    public async Task<NoContent> DeleteAvailableTime(int id)
    {
        await sender.Send(new DeleteAvailableTimeCommand(id, user.Id!));
        return TypedResults.NoContent();
    }
}