namespace GitHubCommitPuller.Models;

public sealed record CommitResponse(string Sha, Commit Commit);