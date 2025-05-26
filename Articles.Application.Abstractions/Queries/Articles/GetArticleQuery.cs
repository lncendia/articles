using Articles.Application.Abstractions.DTOs.Articles;
using MediatR;

namespace Articles.Application.Abstractions.Queries.Articles;

/// <summary>
/// Запрос на получение статьи по её идентификатору.
/// </summary>
public class GetArticleQuery : IRequest<ArticleDto>
{
    /// <summary>
    /// Идентификатор статьи.
    /// </summary>
    public required Guid ArticleId { get; init; }
}