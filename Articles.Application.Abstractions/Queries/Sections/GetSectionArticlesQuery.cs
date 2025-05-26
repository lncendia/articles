using Articles.Application.Abstractions.DTOs.Articles;
using Articles.Application.Abstractions.DTOs.Common;
using MediatR;

namespace Articles.Application.Abstractions.Queries.Sections;

/// <summary>
/// Команда запрашивающая.
/// </summary>
public class GetSectionArticlesQuery : IRequest<CountResult<ArticleDto>>
{
    /// <summary>
    /// Коллекция уникальных тегов.
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