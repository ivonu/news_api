using Microsoft.AspNetCore.Mvc;
using news_api.Services;

namespace news_api.Controllers;

[ApiController]
[Route("[controller]")]
public class NewsController(INewsService newsService) : ControllerBase
{
    [HttpGet("top")]
    public async Task<IActionResult> GetNArticles([FromQuery] int n) {
        try {
            var newsArticles = await newsService.GetNewsArticles(n);
            return StatusCode(StatusCodes.Status200OK, newsArticles);

        } catch (Exception e) {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpGet("search")]
    public async Task<IActionResult> GetArticlesByKeyword([FromQuery] List<string> keywords) {
        try {
            var newsArticles = await newsService.GetNewsArticlesByKeywords(keywords);
            return StatusCode(StatusCodes.Status200OK, newsArticles);
            
        } catch (Exception e) {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpGet("date")]
    public async Task<IActionResult> GetArticlesByDate([FromQuery] DateTime date) {
        try {
            var newsArticles = await newsService.GetNewsArticlesByDate(date);
            return StatusCode(StatusCodes.Status200OK, newsArticles);
            
        } catch (Exception e) {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }
}
