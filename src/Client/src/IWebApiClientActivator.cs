// -------------------------------------------------------
// Copyright (c) BlazorFocused All rights reserved.
// Licensed under the MIT License
// -------------------------------------------------------

namespace BlazorFocused.Http.Client;

/// <summary>
/// Activates <see cref="IWebApiClient"/> instances
/// </summary>
public interface IWebApiClientActivator
{
    /// <summary>
    /// Creates a new <see cref="IWebApiClient"/> instance
    /// </summary>
    /// <param name="name">Name of registered <see cref="IWebApiClient"/> set during dependency injection</param>
    /// <returns>Named instance of <see cref="IWebApiClient"/> registered in dependency injection</returns>
    IWebApiClient Create(string name);
}
