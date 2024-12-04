using GitHubCommitPuller.Builders;
using GitHubCommitPuller.Helpers;
using GitHubCommitPuller.Models.Entities;
using GitHubCommitPuller.Validators;
using Microsoft.Extensions.DependencyInjection;
using GitHubCommitPuller.Services.Interfaces;
using Marten;

try
{
    ArgumentsValidator.Validate(args);

    var owner = args[0];
    var repo = args[1];

    var serviceProvider = ServiceProviderBuilder.Build();

    var gitHubService = serviceProvider.GetRequiredService<IGitHubService>();
    var commits = await gitHubService.GetCommitsAsync(owner, repo);
    
    DisplayHelper.DisplayCommits(commits, repo);

    var commitEntities = commits.Select(c => new Commit
    {
        Username = owner,
        Repository = repo,
        Sha = c.Sha,
        Message = c.Commit.Message,
        Committer = c.Commit.Committer.Name
    });

    var session = serviceProvider.GetRequiredService<IDocumentSession>();
    session.Store(commitEntities);
    await session.SaveChangesAsync();

}
catch (Exception ex)
{
    Console.WriteLine($"An error occurred: {ex.Message}");
}

return;