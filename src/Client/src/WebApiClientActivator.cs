// -------------------------------------------------------
// Copyright (c) BlazorFocused All rights reserved.
// Licensed under the MIT License
// -------------------------------------------------------

using BlazorFocused.Http.Client.Client;

namespace BlazorFocused.Http.Client;

/// <summary>
/// Activates <see cref="IWebApiClient"/> instances
/// </summary>
public class WebApiClientActivator : IWebApiClientActivator
{
    private readonly IHttpClientFactory httpClientFactory;

    /// <summary>
    /// Creates a new <see cref="WebApiClientActivator"/> instance
    /// </summary>
    /// <param name="httpClientFactory">Client Factory existing in dependency injection</param>
    public WebApiClientActivator(IHttpClientFactory httpClientFactory)
    {
        this.httpClientFactory = httpClientFactory;
    }

    /// <inheritdoc/>
    public IWebApiClient Create(string name) => new WebApiClient(httpClientFactory.CreateClient(name));

    /// <summary>
    /// Creates a new <see cref="IWebApiClient"/> instance based on corresponding <see cref="HttpClient"/>
    /// </summary>
    /// <param name="httpClient">Base <see cref="HttpClient"/> used for request sending</param>
    /// <returns><see cref="IWebApiClient"/> wrapper based on input <paramref name="httpClient"/></returns>
    public static IWebApiClient Create(HttpClient httpClient) => new WebApiClient(httpClient);
}
