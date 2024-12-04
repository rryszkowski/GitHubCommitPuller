using GitHubCommitPuller.Helpers;
using GitHubCommitPuller.Services.Interfaces;
using GitHubCommitPuller.Services;
using Marten;
using Microsoft.Extensions.DependencyInjection;
using Weasel.Core;

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
            .AddMarten(opts => opts.Connection("Host=localhost;Database=commits_db;Username=postgres;Password=password1"));

        return services.BuildServiceProvider();
    }
}