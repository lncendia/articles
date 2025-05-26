using MediatR;
using Test.Application.Abstractions.DTOs.Articles;
using Test.Application.Abstractions.DTOs.Common;

namespace Test.Application.Abstractions.Commands.Catalogs;

public class SectionTagsCommand : IRequest<CountResult<ArticleDto>>
{
    /// <summary>
    /// Коллекция уникальных тэгов.
    /// </summary>
    public List<string>? Tags { get; set; }
}