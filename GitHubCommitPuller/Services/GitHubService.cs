using GitHubCommitPuller.Models;
using GitHubCommitPuller.Services.Interfaces;
using System.Net.Http.Json;
using System.Text.Json;

namespace GitHubCommitPuller.Services;

public class GitHubService : IGitHubService
{
    private readonly HttpClient _httpClient;

    public GitHubService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("https://api.github.com/");
        _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("GitHubCommitsFetcher/1.0");
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
}