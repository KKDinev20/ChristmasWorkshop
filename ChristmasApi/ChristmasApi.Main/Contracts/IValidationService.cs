// <copyright file="IValidationService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using ChristmasApi.Data.Models;

namespace ChristmasApi.Main.Contracts;

public interface IValidationService
{
    Task<bool> ValidateLightAsync(Light light);
}
