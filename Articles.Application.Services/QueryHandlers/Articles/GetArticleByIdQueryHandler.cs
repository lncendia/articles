using Articles.Application.Abstractions.DTOs.Articles;
using Articles.Application.Abstractions.Queries.Articles;
using Articles.Domain;
using Articles.Domain.Articles;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Articles.Abstractions.Interfaces.Utils.Exceptions;

namespace Articles.Application.Services.QueryHandlers.Articles;

/// <summary>
/// Обработчик запроса на получение статьи по идентификатору.
/// </summary>
/// <param name="uow">Единица работ</param>
public class GetArticleByIdQueryHandler(IUnitOfWork uow) : IRequestHandler<GetArticleByIdQuery, ArticleDto>
{
    /// <summary>
    /// Обрабатывает запрос на получение статьи по идентификатору.
    /// </summary>
    /// <param name="request">Запрос, с идентификатором статьи</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    public async Task<ArticleDto> Handle(GetArticleByIdQuery request, CancellationToken cancellationToken)
    {
        var article = await uow.Query<ArticleAggregate>().FirstOrDefaultAsync(a => a.Id == request.ArticleId, cancellationToken);

        if (article == null)
            throw new ArticleNotFoundException(request.ArticleId);

        return new ArticleDto
        {
            Id = article.Id,
            Title = article.Title,
            Content = article.Content,
            CreatedAt = article.CreatedAt,
            UpdatedAt = article.UpdatedAt,
            Tags = article.Tags.ToList()
        };
    }
}