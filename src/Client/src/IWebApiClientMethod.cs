// -------------------------------------------------------
// Copyright (c) BlazorFocused All rights reserved.
// Licensed under the MIT License
// -------------------------------------------------------

using System.Text.Json;

namespace BlazorFocused.Http.Client;

internal interface IWebApiClientMethod
{
    IWebApiClientMethod AddParameter(string key, string value);

    IWebApiClientMethod AddHeader(string key, string value);

    IWebApiClientMethod AddSerializationOptions(JsonSerializerOptions jsonSerializerOptions);

    Task<T> ExecuteAsync<T>(CancellationToken cancellationToken = default);

    Task<WebApiTaskResponse> ExecuteAsync(CancellationToken cancellationToken = default);

    Task<WebApiTaskResponse<T>> TryExecuteAsync<T>(CancellationToken cancellationToken = default)
        where T : class;

    Task<WebApiTaskResponse> TryExecuteAsync(CancellationToken cancellationToken = default);
}
