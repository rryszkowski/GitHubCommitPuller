using GitHubCommitPuller.Models;

namespace GitHubCommitPuller.Services.Interfaces;

public interface IGitHubService
{
    Task<IReadOnlyList<CommitResponse>> GetCommitsAsync(string owner, string repo);
}