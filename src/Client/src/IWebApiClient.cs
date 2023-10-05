// -------------------------------------------------------
// Copyright (c) BlazorFocused All rights reserved.
// Licensed under the MIT License
// -------------------------------------------------------

namespace BlazorFocused.Http.Client;

/// <summary>
/// Standardizes the handling of HTTP requests/responses within a given application
/// </summary>
public interface IWebApiClient
{
    /// <summary>
    /// Exposes the underlying <see cref="System.Net.Http.HttpClient"/> used by the <see cref="IWebApiClient"/>
    /// </summary>
    HttpClient HttpClient { get; }

    // IWebApiClientMethod Delete(string relativeUrl);

    // IWebApiClientMethod Get(string relativeUrl);

    // IWebApiClientContent Patch(string relativeUrl);

    // IWebApiClientContent Post(string relativeUrl);

    // IWebApiClientContent Put(string relativeUrl);

    /// <summary>
    /// Sends http request for specified <see cref="HttpRequestMessage"/>
    /// </summary>
    /// <param name="httpRequestMessage">Message request for http operation</param>
    /// <param name="cancellationToken">Current cancellation status of overall operation</param>
    /// <returns>Response from http request</returns>
    Task<HttpResponseMessage> SendAsync(HttpRequestMessage httpRequestMessage, CancellationToken cancellationToken = default);
}
