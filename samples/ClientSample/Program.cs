// -------------------------------------------------------
// Copyright (c) BlazorFocused All rights reserved.
// Licensed under the MIT License
// -------------------------------------------------------

using BlazorFocused.Http.Client;
using Microsoft.Extensions.DependencyInjection;

namespace ClientSample;

internal class Program
{
    public static void Main(string[] args) =>
        Console.WriteLine("Hello, World! Arguments: {0}", string.Join("->", args));

    public static void Test()
    {
        IServiceCollection services = new ServiceCollection();

        services.AddWebApiClient("Program");
    }
}
