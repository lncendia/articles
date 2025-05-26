using Articles.Application.Abstractions.DTOs.Articles;
using Articles.Application.Abstractions.DTOs.Common;
using MediatR;

namespace Articles.Application.Abstractions.Commands.Catalogs;

public class SectionTagsCommand : IRequest<CountResult<ArticleDto>>
{
    /// <summary>
    /// Коллекция уникальных тэгов.
    /// </summary>
    public List<string>? Tags { get; set; }
}