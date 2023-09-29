// -------------------------------------------------------
// Copyright (c) BlazorFocused All rights reserved.
// Licensed under the MIT License
// -------------------------------------------------------

namespace BlazorFocused.Http.Client.Client;

internal class WebApiClient : IWebApiClient
{
    public WebApiClient(HttpClient httpClient)
    {
        this.Client = httpClient;
    }

    internal HttpClient Client { get; }

    public IWebApiClientMethod Delete(string relativeUrl) => throw new NotImplementedException();
    public IWebApiClientMethod Get(string relativeUrl) => throw new NotImplementedException();
    public IWebApiClientContent Patch(string relativeUrl) => throw new NotImplementedException();
    public IWebApiClientContent Post(string relativeUrl) => throw new NotImplementedException();
    public IWebApiClientContent Put(string relativeUrl) => throw new NotImplementedException();
    public Task<HttpResponseMessage> SendAsync(HttpRequestMessage httpRequestMessage, CancellationToken cancellationToken = default) => throw new NotImplementedException();
}
