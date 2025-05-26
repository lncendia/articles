using MediatR;
using Articles.Application.Abstractions.DTOs.Articles;
using Articles.Application.Abstractions.DTOs.Common;
using Articles.Application.Abstractions.Queries.Catalogs;
using Articles.Domain;
using Articles.Domain.Articles;
using Articles.Domain.Catalogs;
using Microsoft.EntityFrameworkCore;

namespace Articles.Application.Services.QueryHandlers.Catalogs;

/// <summary>
/// Обработчик запроса на получение статей по заданным тэгам.
/// </summary>
/// <param name="uow">Единица работы</param>
public class ArticlesQueryHandler(IUnitOfWork uow) : IRequestHandler<ArticlesQuery, CountResult<ArticleDto>>
{
    /// <summary>
    /// Обрабатывает запрос и возвращает статьи, содержащие указанные тэги.
    /// </summary>
    /// <param name="request">Запрос, содержащий список тэгов</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    public async Task<CountResult<ArticleDto>> Handle(ArticlesQuery request, CancellationToken cancellationToken)
    {
        var articlesQuery = uow.Query<ArticleAggregate>()
            .Where(a => a.TagsHash == uow.Query<SectionAggregate>()
                .Where(s => s.Id == request.SectionId)
                .Select(s => s.TagsHash)
                .FirstOrDefault());

        var articlesCount = await articlesQuery.CountAsync(cancellationToken: cancellationToken);

        if (articlesCount == 0)
            return CountResult<ArticleDto>.NoValues();

        var articles = await articlesQuery
            .OrderByDescending(a => a.UpdatedAt ?? a.CreatedAt)
            .Select(a => new ArticleDto
            {
                Id = a.Id,
                Title = a.Title,
                Content = a.Content,
                Tags = a.Tags.ToList(),
                CreatedAt = a.CreatedAt,
                UpdatedAt = a.UpdatedAt
            })
            .Skip(request.Skip)
            .Take(request.Take)
            .ToArrayAsync(cancellationToken: cancellationToken);

        // Возвращаем найденные статьи и их количество
        return new CountResult<ArticleDto>
        {
            List = articles,
            TotalCount = articlesCount
        };
    }
}