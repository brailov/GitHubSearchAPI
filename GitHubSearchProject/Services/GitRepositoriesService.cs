using GitHubSearchProject;
using GitHubSearchProject.DTOs;
using GitHubSearchProject.Interfaces;
using Newtonsoft.Json;
using static System.Net.WebRequestMethods;

namespace GitHubSearchProject.Services
{
    public class GitRepositoriesService(HttpClient httpClient) : IGitRepositoriesService
    {
        private readonly HttpClient _httpClient = httpClient;

        public async Task<List<GitRepository>> GetRepositories(string keyWord)
        {
            var url = $"https://api.github.com/search/repositories?q={keyWord}";
           
            try
            {
                _httpClient.DefaultRequestHeaders.Add("User-Agent", "MyApp");
                var response = await _httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {                   
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new HttpRequestException($"HTTP request failed with status code {response.StatusCode}: {errorContent}");                  
                }

                var content = await response.Content.ReadAsStringAsync();

                // Deserialize the JSON response
                var searchResult = JsonConvert.DeserializeObject<RepositoriySearchResult>(content);
                
                if(searchResult is null || searchResult.total_count == 0 || searchResult.items.Count == 0) return [];

                List<GitRepository> repositoryFilteredList = searchResult.items.Select(full => new GitRepository
                {
                    AvatarUrl = full.owner.avatar_url,
                    Name = full.name
                }).ToList();

                return repositoryFilteredList ?? [];
            }
            catch (HttpRequestException ex)
            {               
                Console.WriteLine($"HTTP Request Exception: {ex.Message}");
                throw;
            }
            catch (System.Text.Json.JsonException ex)
            {              
                Console.WriteLine($"JSON Deserialization Exception: {ex.Message}");
                throw;
            }
        }
    }
}
