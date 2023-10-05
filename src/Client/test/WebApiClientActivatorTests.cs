// -------------------------------------------------------
// Copyright (c) BlazorFocused All rights reserved.
// Licensed under the MIT License
// -------------------------------------------------------

using Bogus;
using Moq;

namespace BlazorFocused.Http.Client.Test;

public class WebApiClientActivatorTests
{
    [Fact]
    public void Create_ShouldReturnWebApiClientFromRegisteredClient()
    {
        // Arrange
        string expectedBaseAddress = new Faker().Internet.Url();
        string webApiClientName = new Faker().Random.AlphaNumeric(5);
        Mock<IHttpClientFactory> mockHttpClientFactory = new();

        mockHttpClientFactory.Setup(factory => factory.CreateClient(webApiClientName))
            .Returns(new HttpClient() { BaseAddress = new Uri(expectedBaseAddress) });

        IWebApiClientActivator webApiClientActivator = new WebApiClientActivator(mockHttpClientFactory.Object);

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
