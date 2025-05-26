using Articles.Application.Abstractions.DTOs.Articles;
using Articles.Application.Abstractions.Exceptions;
using Articles.Application.Abstractions.Queries.Articles;
using Articles.Domain;
using Articles.Domain.Articles;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Articles.Application.Services.QueryHandlers.Articles;

/// <summary>
/// Обработчик запроса на получение статьи по идентификатору.
/// </summary>
/// <param name="uow">Единица работы (Unit of Work) для доступа к данным</param>
public class GetArticleQueryHandler(IUnitOfWork uow) : IRequestHandler<GetArticleQuery, ArticleDto>
{
    /// <summary>
    /// Обрабатывает запрос на получение статьи по идентификатору.
    /// </summary>
    /// <param name="request">Запрос, содержащий идентификатор статьи</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    /// <returns>DTO статьи с основными данными</returns>
    /// <exception cref="ArticleNotFoundException">Выбрасывается, если статья не найдена</exception>
    public async Task<ArticleDto> Handle(GetArticleQuery request, CancellationToken cancellationToken)
    {
        // Формируем запрос к базе данных для получения статьи
        var article = await uow.Query<ArticleAggregate>()

            // Проецируем данные агрегата в DTO
            .Select(a => new ArticleDto
            {
                Id = a.Id,
                Title = a.Title,
                CreatedAt = a.CreatedAt,
                UpdatedAt = a.UpdatedAt,
                Tags = a.Tags.ToArray()
            })

            // Ищем статью по идентификатору (первая или null)
            .FirstOrDefaultAsync(a => a.Id == request.ArticleId, cancellationToken);

        // Если статья не найдена - выбрасываем исключение, иначе возвращаем результат
        return article ?? throw new ArticleNotFoundException(request.ArticleId);
    }
}