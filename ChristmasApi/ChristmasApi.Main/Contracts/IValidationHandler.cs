// <copyright file="IValidationHandler.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using ChristmasApi.Data.Models;

namespace ChristmasApi.Main.Contracts;

public interface IValidationHandler
{
    IValidationHandler SetNext(IValidationHandler next);

    Task<bool> ValidateAsync(Light light);
}