// -------------------------------------------------------
// Copyright (c) BlazorFocused All rights reserved.
// Licensed under the MIT License
// -------------------------------------------------------

using BlazorFocused.Http.Client.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace BlazorFocused.Http.Client;

/// <summary>
/// Extension used to register WebApiClient Services
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Extension used to register WebApiClient Services
    /// </summary>
    /// <param name="services">Current service dependencies</param>
    /// <param name="name">Name of web api client instance</param>
    /// <returns><see cref="IHttpClientBuilder"/> to extend <see cref="HttpClient"/> properties</returns>
    public static IHttpClientBuilder AddWebApiClient(this IServiceCollection services, string name)
    {
        services.TryAddScoped<IWebApiClient, WebApiClient>();
        services.TryAddScoped<IWebApiClientActivator, WebApiClientActivator>();

        // Detect/bind configurations settings
        services.AddOptions<WebApiOptions>(name)
            .BindConfiguration($"BlazorFocused:WebApiClient:{name}");

#if NET8_0_OR_GREATER
        services.AddKeyedScoped<IWebApiClient, WebApiClient>(name, (serviceProvider, test) =>
        {
            IHttpClientFactory httpClientFactory =
                serviceProvider.GetRequiredService<IHttpClientFactory>();

            return new WebApiClient(httpClientFactory.CreateClient(name));
        });
#endif

        return services.AddHttpClient(name);
    }
}
