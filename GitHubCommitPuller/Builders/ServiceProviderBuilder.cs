using GitHubCommitPuller.Helpers;
using GitHubCommitPuller.Services.Interfaces;
using GitHubCommitPuller.Services;
using Marten;
using Microsoft.Extensions.DependencyInjection;

namespace GitHubCommitPuller.Builders;

public static class ServiceProviderBuilder
{
    public static IServiceProvider Build()
    {
        var services = new ServiceCollection();

        services
            .AddHttpClient<IGitHubService, GitHubService>()
            .AddPolicyHandler(PolicyHelper.GetRetryPolicy())
            .AddPolicyHandler(PolicyHelper.GetTimeoutPolicy());

        services
            .AddMarten(opts => opts.Connection("Host=localhost;Database=marten_demo;Username=postgres;Password=password"));

        return services.BuildServiceProvider();
    }
}