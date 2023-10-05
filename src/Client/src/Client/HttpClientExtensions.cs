// -------------------------------------------------------
// Copyright (c) BlazorFocused All rights reserved.
// Licensed under the MIT License
// -------------------------------------------------------

namespace BlazorFocused.Http.Client.Client;

internal static class HttpClientExtensions
{
    public static void ConfigureWebApiClientOptions(this HttpClient httpClient, WebApiClientOptions webApiOptions)
    {
        if (!string.IsNullOrWhiteSpace(webApiOptions.BaseAddress))
        {
            httpClient.BaseAddress = new Uri(webApiOptions.BaseAddress);
        }

        foreach (KeyValuePair<string, string> header in webApiOptions.DefaultRequestHeaders)
        {
            httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
        }

        if (webApiOptions.MaxResponseContentBufferSize.HasValue)
        {
            httpClient.MaxResponseContentBufferSize = webApiOptions.MaxResponseContentBufferSize.Value;
        }

        if (webApiOptions.Timeout.HasValue)
        {
            httpClient.Timeout = TimeSpan.FromMilliseconds(webApiOptions.Timeout.Value);
        }
    }
}
