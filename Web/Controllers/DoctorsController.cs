using Application.Doctor.Commands.CreateDoctor;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("api/[controller]")]
[AllowAnonymous]
public class DoctorsController(ISender sender) : ControllerBase
{
    [HttpPost]
    public async Task<Created<int>> CreateDoctor(CreateDoctorCommand command)
    {
        var id = await sender.Send(command);

        return TypedResults.Created($"/{nameof(Doctor)}/{id}", id);
    }
}