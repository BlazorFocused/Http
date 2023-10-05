// -------------------------------------------------------
// Copyright (c) BlazorFocused All rights reserved.
// Licensed under the MIT License
// -------------------------------------------------------

using BlazorFocused.Http.Client.Client;
using Bogus;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace BlazorFocused.Http.Client.Test;

public class ServiceCollectionExtensionsTests
{
    [Fact]
    public void AddWebApiClient_ShouldRegisterServices()
    {
        // Arrange
        IServiceCollection services = new ServiceCollection();

        // Act
        services.AddWebApiClient("Test");

        // Assert
        IServiceProvider serviceProvider = services.BuildServiceProvider();

        Assert.NotNull(serviceProvider.GetService<IHttpClientFactory>());
        Assert.NotNull(serviceProvider.GetService<IWebApiClientActivator>());

        // During testing scenarios, found that IConfiguration is needed for the
        // .AddOptions<WebApiClientOptions>(name) to work properly.
        // This will only be available if the IConfiguration is added to the service collection.
        Assert.Throws<InvalidOperationException>(() =>
            serviceProvider.GetService<IOptions<WebApiClientOptions>>());
    }

    [Fact]
    public void AddWebApiClient_ShouldProvideRegisteredClientActivator()
    {
        // Arrange
        IServiceCollection services = new ServiceCollection();
        string expectedBaseAddress = new Faker().Internet.Url();
        string webApiClientName = new Faker().Random.AlphaNumeric(5);

        // Act
        services.AddWebApiClient(webApiClientName)
            .ConfigureHttpClient(client => client.BaseAddress = new Uri(expectedBaseAddress));

        // Assert
        IServiceProvider serviceProvider = services.BuildServiceProvider();
        IWebApiClientActivator webApiClientActivator = serviceProvider.GetService<IWebApiClientActivator>();
        IWebApiClient actualWebApiClient = webApiClientActivator.Create(webApiClientName);

        Assert.Equal(expectedBaseAddress, actualWebApiClient.HttpClient.BaseAddress.OriginalString);
    }

#if NET8_0_OR_GREATER
    [Fact]
    public void AddWebApiClient_ShouldProvideKeyedWebApiClient()
    {
        // Arrange
        IServiceCollection services = new ServiceCollection();
        string expectedBaseAddress = new Faker().Internet.Url();
        string webApiClientName = new Faker().Random.AlphaNumeric(5);

        // Act
        services.AddWebApiClient(webApiClientName)
            .ConfigureHttpClient(client => client.BaseAddress = new Uri(expectedBaseAddress));

        // Assert
        IServiceProvider serviceProvider = services.BuildServiceProvider();
        IWebApiClient actualWebApiClient = serviceProvider.GetKeyedService<IWebApiClient>(webApiClientName);

        Assert.Equal(expectedBaseAddress, actualWebApiClient.HttpClient.BaseAddress.OriginalString);
    }
#endif

    [Fact]
    public void AddWebApiClient_ShouldAllowConfigureHttpClientThroughConfiguration()
    {
        // Arrange
        string expectedBaseAddress = new Faker().Internet.Url();
        string webApiClientName = new Faker().Random.AlphaNumeric(5);

        var expectedRequestHeaders = new Dictionary<string, string[]>()
        {
            ["Accept"] = new string[] { "application/json" },
            ["Accept-Encoding"] = new string[] { "gzip" },
            ["Cache-Control"] = new string[] { "max-age=0" }
        };

        var appSettings = new Dictionary<string, string>()
        {
            [$"BlazorFocused:WebApiClient:{webApiClientName}:baseAddress"] = expectedBaseAddress,
            [$"BlazorFocused:WebApiClient:{webApiClientName}:defaultRequestHeaders:Accept"] = "application/json",
            [$"BlazorFocused:WebApiClient:{webApiClientName}:defaultRequestHeaders:Accept-Encoding"] = "gzip",
            [$"BlazorFocused:WebApiClient:{webApiClientName}:defaultRequestHeaders:Cache-Control"] = "max-age=0",
            [$"BlazorFocused:WebApiClient:{webApiClientName}:maxResponseContentBufferSize"] = "500000",
            [$"BlazorFocused:WebApiClient:{webApiClientName}:timeout"] = "300000"
        };

        IServiceCollection services = new ServiceCollection();

        // Add in memory configuration with app settings
        // Establishes in IConfiguration
        AddConfiguration(services, appSettings);

        // Act
        services.AddWebApiClient(webApiClientName);

        // Assert
        IServiceProvider serviceProvider = services.BuildServiceProvider();
        IWebApiClientActivator webApiClientActivator = serviceProvider.GetService<IWebApiClientActivator>();
        IWebApiClient actualWebApiClient = webApiClientActivator.Create(webApiClientName);
        HttpClient httpClient = actualWebApiClient.HttpClient;

        httpClient.DefaultRequestHeaders.Should().BeEquivalentTo(expectedRequestHeaders);

        Assert.Equal(expectedBaseAddress, httpClient.BaseAddress.OriginalString);
        Assert.Equal(3, httpClient.DefaultRequestHeaders.Count());
        Assert.Equal(500000, httpClient.MaxResponseContentBufferSize);
        Assert.Equal(300000, httpClient.Timeout.TotalMilliseconds);
    }

    private static IServiceCollection AddConfiguration(
        IServiceCollection services,
        Dictionary<string, string> appSettings)
    {
        var configurationBuilder = new ConfigurationBuilder();

        configurationBuilder.AddInMemoryCollection(appSettings);

        return services.AddSingleton<IConfiguration>(configurationBuilder.Build());
    }
}
