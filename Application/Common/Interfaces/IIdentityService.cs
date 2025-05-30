﻿using Application.Common.Models;

namespace Application.Common.Interfaces;

public interface IIdentityService
{
    Task<string?> GetUserNameAsync(string userId);

    Task<bool> IsInRoleAsync(string userId, string role);

    Task<bool> AuthorizeAsync(string userId, string policyName);

    Task<(Result Result, string UserId)> CreateDoctorUserAsync(string email,
        string password);

    Task<(Result Result, string UserId)> CreatePatientUserAsync(string email,
        string password);

    Task<Result> DeleteUserAsync(string userId);
}