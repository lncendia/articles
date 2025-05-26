using Articles.Application.Abstractions.DTOs.Articles;
using Articles.Application.Abstractions.DTOs.Common;
using MediatR;

namespace Articles.Application.Abstractions.Queries.Catalogs;

public class SectionTagsQuery : IRequest<CountResult<ArticleDto>>
{
    /// <summary>
    /// Коллекция уникальных тэгов.
    /// </summary>
    public List<string>? Tags { get; init; }
}