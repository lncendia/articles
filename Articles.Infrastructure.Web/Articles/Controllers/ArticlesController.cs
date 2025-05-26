using Articles.Infrastructure.Web.Articles.InputModels;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Articles.Application.Abstractions.Commands.Articles;

namespace Articles.Infrastructure.Web.Articles.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArticlesController(ISender mediator, IMapper mapper) : ControllerBase
    {
        // Получить статью по идентификатору
        [HttpGet("{id:guid}")]
        public IActionResult GetArticleById(Guid id)
        {
            return Ok(); // Реализация не требуется
        }

        // Создать статью
        [HttpPost]
        [HttpPut]
        public async Task<IActionResult> CreateArticle([FromBody] CreateArticleRequest request, CancellationToken token)
        {
            var command = mapper.Map<CreateArticleCommand>(request);
            
            await mediator.Send(command, token);
            
            return Ok();
        }

        // Изменить статью
        [HttpPost("{id:guid}")]
        public async Task<IActionResult> UpdateArticle(Guid id, [FromBody] UpdateArticleRequest request, CancellationToken token)
        {
            var command = mapper.Map<UpdateArticleCommand>(request, options =>
            {
                options.Items.Add("Id", id);
            });
            
            await mediator.Send(command, token);
            
            return Ok();
        }
    }
}