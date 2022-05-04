using Newtonsoft.Json;

namespace GrasscutterInstaller;

public class GithubRepository
{
    #region DataModels
    public class Data
    {
        public class LicenseModel
        {
            [JsonProperty("key")] public string Key { get; set; } = null!;
            [JsonProperty("name")] public string Name { get; set; } = null!;
            [JsonProperty("url")] public string Url { get; set; } = null!;
        }

        /// <summary>
        /// Error message;
        /// </summary>
        [JsonProperty("message")] public string? Message { get; set; } = null;
        [JsonProperty("description")] public string Description { get; set; } = null!;
        [JsonProperty("branches_url")] public string BranchesUrl { get; set; } = null!;
        [JsonProperty("trees_url")] public string TreesUrl { get; set; } = null!;
        [JsonProperty("commits_url")] public string CommitsUrl { get; set; } = null!;
        [JsonProperty("license")] public LicenseModel? License { get; set; } = null;

        public Data()
        {
        }
    }
    public class BranchModel
    {
        public string Name { get; set; } = null!;

        public BranchModel() { }
    }
    public class CommitModel
    {

    }
    #endregion

    public readonly Data data;

    public LazyGetJsonObject<BranchModel[]> Branches { get; private init; }

    public GithubRepository(Data data)
    {
        this.data = data;
        Branches = new LazyGetJsonObject<BranchModel[]>(data.BranchesUrl.Replace("{/branch}", ""));
    }

    /// <summary>
    /// Get repository from github async
    /// </summary>
    /// <param name="owner">repository owner</param>
    /// <param name="repo">repository name</param>
    /// <returns>返回一个</returns>
    public static async Task<GithubRepository> GetFromGithubAsync(string owner, string repo)
    {
        var repoData = await NetworkHelper.RequestJsonAsync<Data>(HttpMethod.Get, $"https://api.github.com/repos/{owner}/{repo}");
        return new GithubRepository(repoData);
    }

    public async Task<CommitModel[]> GetCommits(string? branchName)
    {
        var uri = data.BranchesUrl.Replace("{/commits}", branchName != null ? "/" + branchName : "");
        return await NetworkHelper.RequestJsonAsync<CommitModel[]>(HttpMethod.Get, uri);
    }
}
