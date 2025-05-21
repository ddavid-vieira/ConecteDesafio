using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Common.Models.Scheduling;
using Application.Common.Security;
using Application.Scheduling.Commands.CreateScheduling;
using Application.Scheduling.Commands.DeleteScheduling;
using Application.Scheduling.Queries.GetPatientSchedulingsWithPagination;
using Application.Scheduling.Queries.GetSchedulingsWithPagination;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class SchedulingController(ISender sender, IUser user) : ControllerBase
{
    [HttpGet]
    public async Task<Ok<PaginatedList<DoctorSchedulingsGroupedByDateDto>>> GetSchedulingsWithPagination(
        [FromQuery] GetSchedulingPaginationParams query)
    {
        var result = await sender
            .Send(new GetSchedulingsWithPaginationQuery(user.Id!, query));

        return TypedResults.Ok(result);
    }


    [HttpGet("Patient")]
    public async Task<Ok<PaginatedList<PatientSchedulingsGroupedByDateDto>>> GetPatientSchedulingsWithPagination(
        [FromQuery] GetSchedulingPaginationParams query)
    {
        var result = await sender
            .Send(new GetPatientSchedulingsWithPaginationQuery(user.Id!, query));

        return TypedResults.Ok(result);
    }

    [HttpPost]
    public async Task<Created<int>> CreateScheduling(CreateSchedulingCommand command)
    {
        var id = await sender.Send(command);

        return TypedResults.Created($"/Scheduling/{id}", id);
    }

    [HttpDelete("{id:int}")]
    public async Task<NoContent> DeleteScheduling(int id)
    {
        await sender.Send(new DeleteSchedulingCommand(id));
        return TypedResults.NoContent();
    }
}