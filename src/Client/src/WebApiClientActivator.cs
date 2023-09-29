// -------------------------------------------------------
// Copyright (c) BlazorFocused All rights reserved.
// Licensed under the MIT License
// -------------------------------------------------------

namespace BlazorFocused.Http.Client;

internal class WebApiClientActivator : IWebApiClientActivator
{
    public IWebApiClient Create(string name) => throw new NotImplementedException();

    public static IWebApiClient Create(HttpClient httpClient) => throw new NotImplementedException();
}
