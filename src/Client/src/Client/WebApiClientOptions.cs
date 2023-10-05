// -------------------------------------------------------
// Copyright (c) BlazorFocused All rights reserved.
// Licensed under the MIT License
// -------------------------------------------------------

namespace BlazorFocused.Http.Client.Client;

internal class WebApiClientOptions
{
    public WebApiClientOptions()
    {
    }

    public string BaseAddress { get; set; }

    public Dictionary<string, string> DefaultRequestHeaders { get; set; } = new();

    public long? MaxResponseContentBufferSize { get; set; }

    public double? Timeout { get; set; }

    public bool IsConfigured => !string.IsNullOrEmpty(BaseAddress) ||
                DefaultRequestHeaders.Count > 0 ||
                MaxResponseContentBufferSize > 0 ||
                Timeout > 0;
}
