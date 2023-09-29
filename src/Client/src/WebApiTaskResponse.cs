// -------------------------------------------------------
// Copyright (c) BlazorFocused All rights reserved.
// Licensed under the MIT License
// -------------------------------------------------------

using System.Net;
using System.Net.Http.Headers;

namespace BlazorFocused.Http.Client;

internal class WebApiTaskResponse
{
    public Stream Content { get; init; }

    public HttpStatusCode? StatusCode { get; set; }

    public virtual bool IsSuccess => HasSuccessStatusCode();

    public HttpResponseHeaders Headers { get; init; }

    public string GetContentString() => new StreamReader(Content).ReadToEnd();

    protected bool HasSuccessStatusCode() =>
        StatusCode.HasValue &&
        new HttpResponseMessage(StatusCode.Value).IsSuccessStatusCode;
}

internal class WebApiTaskResponse<T> : WebApiTaskResponse where T : class
{
    public T Value { get; set; }

    public override bool IsSuccess => HasSuccessStatusCode() && Value is not null;
}
