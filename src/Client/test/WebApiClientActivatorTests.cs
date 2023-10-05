// -------------------------------------------------------
// Copyright (c) BlazorFocused All rights reserved.
// Licensed under the MIT License
// -------------------------------------------------------

using Bogus;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorFocused.Http.Client.Test;

public class WebApiClientActivatorTests
{
    [Fact]
    public void Create_ShouldReturnWebApiClientFromRegisteredClient()
    {
        // Arrange
        IServiceCollection services = new ServiceCollection();
        string expectedBaseAddress = new Faker().Internet.Url();
        string webApiClientName = new Faker().Random.AlphaNumeric(5);

        services.AddWebApiClient(webApiClientName)
            .ConfigureHttpClient(client => client.BaseAddress = new Uri(expectedBaseAddress));

        IServiceProvider serviceProvider = services.BuildServiceProvider();
        IWebApiClientActivator webApiClientActivator = serviceProvider.GetService<IWebApiClientActivator>();

        // Act
        IWebApiClient actualWebApiClient = webApiClientActivator.Create(webApiClientName);

        // Assert
        Assert.Equal(expectedBaseAddress, actualWebApiClient.HttpClient.BaseAddress.OriginalString);
    }

    [Fact]
    public void Create_ShouldReturnWebApiClientFromInputHttpClient()
    {
        // Arrange
        string expectedBaseAddress = new Faker().Internet.Url();
        var httpClient = new HttpClient() { BaseAddress = new Uri(expectedBaseAddress) };

        // Act
        IWebApiClient actualWebApiClient = WebApiClientActivator.Create(httpClient);

        // Assert
        Assert.Equal(expectedBaseAddress, actualWebApiClient.HttpClient.BaseAddress.OriginalString);
    }
}
