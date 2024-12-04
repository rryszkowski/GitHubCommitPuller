using Marten.Schema;

namespace GitHubCommitPuller.Models.Entities;

public class Commit
{
    [Identity]
    public string Sha { get; set; }
    public string Username { get; set; }
    public string Repository { get; set; }
    public string Message { get; set; }
    public string Committer { get; set; }
}