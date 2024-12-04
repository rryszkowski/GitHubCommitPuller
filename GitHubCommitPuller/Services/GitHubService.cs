using GitHubCommitPuller.Models;
using GitHubCommitPuller.Services.Interfaces;
using Marten;
using System.Net.Http.Json;
using System.Text.Json;

namespace GitHubCommitPuller.Services;

public class GitHubService : IGitHubService
{
    private readonly HttpClient _httpClient;
    private readonly IDocumentSession _documentSession;

    public GitHubService(
        HttpClient httpClient,
        IDocumentSession documentSession)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("https://api.github.com/");
        _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("GitHubCommitsFetcher/1.0");
        _documentSession = documentSession;
    }

    public async Task<IReadOnlyList<CommitResponse>> GetCommitsAsync(string owner, string repo)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        var requestUri = $"repos/{owner}/{repo}/commits";


        var commits = await _httpClient.GetFromJsonAsync<IReadOnlyList<CommitResponse>>(
            requestUri, options);

        return commits ?? [];
    }

    public async Task SaveCommitsAsync(IReadOnlyList<CommitResponse> commits, string owner, string repo)
    {
        var commitEntities = commits.Select(c => new Models.Entities.Commit
        {
            Username = owner,
            Repository = repo,
            Sha = c.Sha,
            Message = c.Commit.Message,
            Committer = c.Commit.Committer.Name
        });

        _documentSession.Store(commitEntities);
        await _documentSession.SaveChangesAsync();
    }
}