using Articles.Infrastructure.Web.Articles.InputModels;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Articles.Application.Abstractions.Commands.Articles;
using Articles.Application.Abstractions.DTOs.Articles;
using Articles.Application.Abstractions.Queries.Articles;

namespace Articles.Infrastructure.Web.Articles.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ArticleController(ISender mediator, IMapper mapper) : ControllerBase
{
    // Получить статью по идентификатору
    [HttpGet("{id:guid}")]
    public async Task<ArticleDto> GetArticleById(Guid id, CancellationToken token)
    {
        var query = new GetArticleByIdQuery
        {
            ArticleId = id
        };
        
        return await mediator.Send(query, token); // Реализация не требуется
    }

    // Создать статью
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