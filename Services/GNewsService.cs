
using System.Globalization;
using System.Net.Mime;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Web;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.WebUtilities;

namespace news_api.Services;

public class GNewsService : INewsService {

    private HttpClient client;
    private string? apiKey;

    public GNewsService(string? apiKey) {
        client = new HttpClient();
        client.BaseAddress = new Uri("https://gnews.io/api/v4/");
        this.apiKey = apiKey;
    }   

    private async Task<List<NewsArticle>> MakeRequest(string request) {
        var response = await client.GetAsync(request);

        if (!response.IsSuccessStatusCode) {
            throw new Exception(response.ReasonPhrase);
        }

        var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse>();
        if (apiResponse is null) {
            throw new Exception("Parsing of response failed");
        }

        return apiResponse.Articles.Select(
            n => new NewsArticle {
                Title = n.Title, 
                Description = n.Description,
                Content = n.Content
            }
        ).ToList();
    }

    public async Task<List<NewsArticle>> GetNewsArticles(int n) {
        var q = new QueryString()
            .Add("apikey", apiKey)
            .Add("max", n.ToString());
            
        return await MakeRequest($"top-headlines?{q}");
    } 

    public async Task<List<NewsArticle>> GetNewsArticlesByKeywords(List<string> keywords) {
        
        var keywordsString = String.Join(' ', keywords.Select(k => $"\"{k}\""));

        var q = new QueryString()
            .Add("apikey", apiKey)
            .Add("q", keywordsString)
            .Add("sortby", "relevance");

        return await MakeRequest($"search{q}");
    }

    public async Task<List<NewsArticle>> GetNewsArticlesByDate(DateTime date) {
        var fromDateString = date.ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture);
        var toDateString = date.AddDays(1).ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture);

        var q = new QueryString()
            .Add("apikey", apiKey)
            .Add("q", "a") // TODO: fix
            .Add("from", fromDateString)
            .Add("to", toDateString);

        return await MakeRequest($"search{q}");
    }
}


class ApiResponse
{
  public int TotalArticles { get; set; }
  public List<Article> Articles { get; set; } = new List<Article>();
}

class Article
{
  public string Title { get; set; } = "";
  public string Description { get; set; } = "";
  public string Content { get; set; } = "";
  public string Url { get; set; } = ""; 
  public DateTime PublishedAt { get; set; } 
  public Source Source { get; set; } = new Source();
}

class Source
{
  public string Name { get; set; } = "";
  public string Url { get; set; } = "";
}