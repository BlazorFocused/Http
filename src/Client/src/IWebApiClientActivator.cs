// -------------------------------------------------------
// Copyright (c) BlazorFocused All rights reserved.
// Licensed under the MIT License
// -------------------------------------------------------

namespace BlazorFocused.Http.Client;

internal interface IWebApiClientActivator
{
    IWebApiClient Create(string name);
}
