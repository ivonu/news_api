namespace news_api.Services;

public interface INewsService {
    public Task<List<NewsArticle>> GetNewsArticles(int n);

    public Task<List<NewsArticle>> GetNewsArticlesByKeywords(List<string> keywords);

    public Task<List<NewsArticle>> GetNewsArticlesByDate(DateTime date);
}