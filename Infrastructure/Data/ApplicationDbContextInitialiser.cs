using Domain.Constants;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data;

public static class InitialiserExtensions
{
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();

        await initialiser.InitialiseAsync();

        await initialiser.SeedAsync();
    }
}

public class ApplicationDbContextInitialiser(
    ILogger<ApplicationDbContextInitialiser> logger,
    ApplicationDbContext context,
    UserManager<ApplicationUser> userManager,
    RoleManager<IdentityRole> roleManager)
{
    public async Task InitialiseAsync()
    {
        try
        {
            await context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        // Default roles
        var doctorRole = new IdentityRole(Roles.Doctor);

        if (roleManager.Roles.All(r => r.Name != doctorRole.Name))
            await roleManager.CreateAsync(doctorRole);

        var patientRole = new IdentityRole(Roles.Patient);
        if (roleManager.Roles.All(r => r.Name != patientRole.Name))
            await roleManager.CreateAsync(patientRole);

        /*// Default users

        var applicationUser1 = new ApplicationUser { UserName = "doctor1@gmail.com", Email = "doctor1@gmail.com" };
        if (userManager.Users.All(u => u.UserName != applicationUser1.UserName))
        {
            await userManager.CreateAsync(applicationUser1, "doctor1");
            if (!string.IsNullOrWhiteSpace(Roles.Doctor))
            {
                await userManager.AddToRolesAsync(applicationUser1, new[] { Roles.Doctor });
            }
        }

        var doctor1 = new Doctor()
        {
            Name = "Doctor 1",
            Crm = "CRM 1",
            ApplicationUserId = applicationUser1.Id
        };
        if (context.Doctors.All(d => d.ApplicationUserId != doctor1.ApplicationUserId))
        {
            context.Doctors.Add(doctor1);
            await context.SaveChangesAsync();
        }

        var applicationUser2 = new ApplicationUser { UserName = "patient@gmail.com", Email = "patient1@gmail.com" };
        if (userManager.Users.All(u => u.UserName != applicationUser2.UserName))
        {
            await userManager.CreateAsync(applicationUser1, "patient1");
            if (!string.IsNullOrWhiteSpace(Roles.Patient))
            {
                await userManager.AddToRolesAsync(applicationUser2, new[] { Roles.Patient });
            }
        }

        var patient1 = new Patient()
        {
            Name = "Patient 1",
            Weight = 90,
            ApplicationUserId = applicationUser2.Id
        };
        if (context.Patients.All(d => d.ApplicationUserId != patient1.ApplicationUserId))
        {
            context.Patients.Add(patient1);
            await context.SaveChangesAsync();
        }*/
    }
}