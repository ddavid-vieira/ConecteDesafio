using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Constants;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity;

public class IdentityService(
    UserManager<ApplicationUser> userManager,
    IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory,
    IAuthorizationService authorizationService)
    : IIdentityService
{
    public async Task<string?> GetUserNameAsync(string userId)
    {
        var user = await userManager.FindByIdAsync(userId);

        return user?.UserName;
    }

    public async Task<bool> IsInRoleAsync(string userId, string role)
    {
        var user = await userManager.FindByIdAsync(userId);

        return user != null && await userManager.IsInRoleAsync(user, role);
    }

    public async Task<bool> AuthorizeAsync(string userId, string policyName)
    {
        var user = await userManager.FindByIdAsync(userId);

        if (user == null)
        {
            return false;
        }

        var principal = await userClaimsPrincipalFactory.CreateAsync(user);

        var result = await authorizationService.AuthorizeAsync(principal, policyName);

        return result.Succeeded;
    }

    public async Task<(Result Result, string UserId)> CreateDoctorUserAsync(string email,
        string password)
    {
        var user = new ApplicationUser { UserName = email, Email = email };

        var result = await userManager.CreateAsync(user, password);

        if (result.Succeeded)
            await userManager.AddToRoleAsync(user, Roles.Doctor);

        return (result.ToApplicationResult(), user.Id);
    }

    public async Task<(Result Result, string UserId)> CreatePatientUserAsync(string email,
        string password)
    {
        var user = new ApplicationUser
        {
            UserName = email, Email = email
        };

        var result = await userManager.CreateAsync(user, password);

        if (result.Succeeded)
            await userManager.AddToRoleAsync(user, Roles.Patient);

        return (result.ToApplicationResult(), user.Id);
    }

    public async Task<Result> DeleteUserAsync(string userId)
    {
        var user = await userManager.FindByIdAsync(userId);

        return user != null ? await DeleteUserAsync(user) : Result.Success();
    }

    public async Task<Result> DeleteUserAsync(ApplicationUser user)
    {
        var result = await userManager.DeleteAsync(user);

        return result.ToApplicationResult();
    }
}