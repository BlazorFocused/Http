// -------------------------------------------------------
// Copyright (c) BlazorFocused All rights reserved.
// Licensed under the MIT License
// -------------------------------------------------------

namespace BlazorFocused.Http.Client;

internal interface IWebApiClientContent
{
    IWebApiClientMethod WithRequestBody(object content);

    IWebApiClientMethod WithHttpContent(HttpContent content);

    IWebApiClientMethod WithNoContent();
}
