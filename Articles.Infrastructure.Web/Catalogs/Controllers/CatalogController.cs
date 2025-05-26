using Articles.Application.Abstractions.DTOs.Articles;
using Articles.Application.Abstractions.DTOs.Common;
using Articles.Application.Abstractions.Queries.Catalogs;
using Articles.Infrastructure.Web.Catalogs.InputModels;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Articles.Infrastructure.Web.Catalogs.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CatalogController(ISender mediator, IMapper mapper) : ControllerBase
{
    // Получить список разделов
    [HttpGet]
    public IActionResult GetSections()
    {
        return Ok();
    }

    // Получить список статей в разделе по тэгам
    [HttpGet("Articles")]
    public async Task<CountResult<ArticleDto>> GetArticlesInSection([FromBody] SectionTagsRequest request, CancellationToken token)
    {
        var query = mapper.Map<ArticlesQuery>(request);
        
        return await mediator.Send(query, token);
    }
}