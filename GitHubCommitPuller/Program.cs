using GitHubCommitPuller.Builders;
using GitHubCommitPuller.Helpers;
using GitHubCommitPuller.Validators;
using Microsoft.Extensions.DependencyInjection;
using GitHubCommitPuller.Services.Interfaces;

try
{
    ArgumentsValidator.Validate(args);

    var owner = args[0];
    var repo = args[1];

    var serviceProvider = ServiceProviderBuilder.Build();

    var gitHubService = serviceProvider.GetRequiredService<IGitHubService>();
    var commits = await gitHubService.GetCommitsAsync(owner, repo);
    
    DisplayHelper.DisplayCommits(commits, repo);

    await gitHubService.SaveCommitsAsync(commits, owner, repo);

}
catch (Exception ex)
{
    Console.WriteLine($"An error occurred: {ex.Message}");
}