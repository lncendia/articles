using Microsoft.AspNetCore.Mvc;

namespace Test.Infrastructure.Web.Articles.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArticlesController : ControllerBase
    {
        // Получить статью по идентификатору
        [HttpGet("{id}")]
        public IActionResult GetArticleById(Guid id)
        {
            return Ok(); // Реализация не требуется
        }

        // Создать статью
        [HttpPost]
        public IActionResult CreateArticle([FromBody] CreateArticleRequest request)
        {
            return Ok();
        }

        // Изменить статью
        [HttpPut("{id}")]
        public IActionResult UpdateArticle(Guid id, [FromBody] UpdateArticleRequest request)
        {
            return Ok();
        }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class CatalogController : ControllerBase
    {
        // Получить список разделов
        [HttpGet("sections")]
        public IActionResult GetSections()
        {
            return Ok();
        }

        // Получить список статей в разделе по тэгам
        [HttpPost("sections/articles")]
        public IActionResult GetArticlesInSection([FromBody] SectionTagsRequest request)
        {
            return Ok();
        }
    }

    // Модели запросов (DTO)

    public class CreateArticleRequest
    {
        public string Title { get; set; }
        public List<string> Tags { get; set; }
        public string Content { get; set; }
    }

    public class UpdateArticleRequest
    {
        public string Title { get; set; }
        public List<string> Tags { get; set; }
        public string Content { get; set; }
    }

    public class SectionTagsRequest
    {
        public List<string> Tags { get; set; }
    }
}