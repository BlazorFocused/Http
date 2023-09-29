// -------------------------------------------------------
// Copyright (c) BlazorFocused All rights reserved.
// Licensed under the MIT License
// -------------------------------------------------------

using Microsoft.Extensions.DependencyInjection;

namespace BlazorFocused.Http.Client.Test;

public class ServiceCollectionExtensionsTests
{
    [Fact]
    public void SampleMethod_ShouldCallMethod()
    {
        IServiceCollection services = new ServiceCollection();

        services.AddWebApiClient("Test");

        IServiceProvider serviceProvider = services.BuildServiceProvider();

        Assert.NotNull(serviceProvider.GetService<IWebApiClient>());
        Assert.NotNull(serviceProvider.GetService<IWebApiClientActivator>());
    }
}
