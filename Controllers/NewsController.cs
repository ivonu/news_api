using Microsoft.AspNetCore.Mvc;
using news_api.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace news_api.Controllers;

[ApiController]
[Route("[controller]")]
public class NewsController(INewsService newsService) : ControllerBase
{
    [HttpGet("top")]
    [SwaggerOperation(
        Summary = "Get n News Articles"
    )]
    public async Task<IActionResult> GetNArticles([FromQuery, SwaggerParameter("Number of articles")] int n) {
        try {
            var newsArticles = await newsService.GetNewsArticles(n);
            return StatusCode(StatusCodes.Status200OK, newsArticles);

        } catch (Exception e) {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpGet("search")]
    [SwaggerOperation(
        Summary = "Search for News Articles"
    )]
    public async Task<IActionResult> GetArticlesByKeyword([FromQuery, SwaggerParameter("Keywords which all need to be included in title, description, or conten")] List<string> keywords) {
        try {
            var newsArticles = await newsService.GetNewsArticlesByKeywords(keywords);
            return StatusCode(StatusCodes.Status200OK, newsArticles);
            
        } catch (Exception e) {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpGet("date")]
    [SwaggerOperation(
        Summary = "Search News Articles on a date"
    )]
    public async Task<IActionResult> GetArticlesByDate([FromQuery, SwaggerParameter("date in the format yyyy-mm-dd")] DateTime date) {
        try {
            var newsArticles = await newsService.GetNewsArticlesByDate(date);
            return StatusCode(StatusCodes.Status200OK, newsArticles);
            
        } catch (Exception e) {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }
}
