using GitHubCommitPuller.Models;

namespace GitHubCommitPuller.Helpers;

public static class DisplayHelper
{
    public static void DisplayCommits(IReadOnlyList<CommitResponse> commits, string repo)
    {
        foreach (var commit in commits)
        {
            Console.WriteLine($"[{repo}]/[{commit.Sha}]: {commit.Commit.Message} [{commit.Commit.Committer.Name}]");
        }
    }
}