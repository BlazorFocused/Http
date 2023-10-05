// -------------------------------------------------------
// Copyright (c) BlazorFocused All rights reserved.
// Licensed under the MIT License
// -------------------------------------------------------

using BlazorFocused.Http.Client.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

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
        services.TryAddScoped<IWebApiClientActivator, WebApiClientActivator>();

        // Detect/bind configurations settings
        services.AddOptions<WebApiClientOptions>(name)
            .BindConfiguration($"BlazorFocused:WebApiClient:{name}");

#if NET8_0_OR_GREATER
        services.AddKeyedScoped<IWebApiClient, WebApiClient>(name, (serviceProvider, test) =>
        {
            IHttpClientFactory httpClientFactory =
                serviceProvider.GetRequiredService<IHttpClientFactory>();

            return new WebApiClient(httpClientFactory.CreateClient(name));
        });
#endif

        return services.AddHttpClient(name)
            .ConfigureHttpClient((serviceProvider, httpClient) =>
            {
                IConfiguration configuration = serviceProvider.GetService<IConfiguration>();

                // During testing scenarios, found that IConfiguration is not always available
                // when building off basic service collection. This causes an error when
                // retrieving IOptionsMonitor<WebApiClientOptions> from service provider.
                // Checking configuration first to prevent this error.

                // Error Found:  'No constructor for type 'Microsoft.Extensions.Options.ConfigurationChangeTokenSource
                // can be instantiated using services from the service container and default values.'

                // If IConfiguration is found, proceed with http client -> options binding
                if (configuration is not null)
                {
                    IOptionsMonitor<WebApiClientOptions> webApiClientOptionsMonitor =
                    serviceProvider.GetService<IOptionsMonitor<WebApiClientOptions>>();

                    WebApiClientOptions webApiClientOptions = webApiClientOptionsMonitor.Get(name);

                    if (webApiClientOptions.IsConfigured)
                    {
                        httpClient.ConfigureWebApiClientOptions(webApiClientOptions);
                    }
                }
            });
    }
}
