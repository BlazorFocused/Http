// -------------------------------------------------------
// Copyright (c) BlazorFocused All rights reserved.
// Licensed under the MIT License
// -------------------------------------------------------

namespace BlazorFocused.Http.Client;

internal interface IWebApiClient
{
    IWebApiClientMethod Delete(string relativeUrl);

    IWebApiClientMethod Get(string relativeUrl);

    IWebApiClientContent Patch(string relativeUrl);

    IWebApiClientContent Post(string relativeUrl);

    IWebApiClientContent Put(string relativeUrl);

    Task<HttpResponseMessage> SendAsync(HttpRequestMessage httpRequestMessage, CancellationToken cancellationToken = default);
}
