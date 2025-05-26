using Articles.Application.Abstractions.DTOs.Articles;
using Articles.Application.Abstractions.DTOs.Common;
using MediatR;

namespace Articles.Application.Abstractions.Queries.Catalogs;

/// <summary>
/// Команда запрашивающая.
/// </summary>
public class ArticlesQuery : IRequest<CountResult<ArticleDto>>
{
    /// <summary>
    /// Коллекция уникальных тэгов.
    /// </summary>
    public required Guid SectionId { get; init; }
    
    /// <summary>
    /// 
    /// </summary>
    public required int Skip { get; init; }
    
    /// <summary>
    /// 
    /// </summary>
    public required int Take { get; init; }
}