namespace GitHubCommitPuller.Models;

public sealed record Commit(string Message, Committer Committer);