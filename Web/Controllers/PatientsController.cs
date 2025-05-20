using Application.Patient.Commands.CreatePatient;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("api/[controller]")]
[AllowAnonymous]
public class PatientsController(ISender sender) : ControllerBase
{
    [HttpPost]
    public async Task<Created<int>> CreatePatient(CreatePatientCommand command)
    {
        var id = await sender.Send(command);

        return TypedResults.Created($"/{nameof(Patient)}/{id}", id);
    }
}