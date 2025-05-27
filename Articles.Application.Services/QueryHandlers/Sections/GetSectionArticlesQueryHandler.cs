using Articles.Application.Abstractions.DTOs.Articles;
using Articles.Application.Abstractions.DTOs.Common;
using Articles.Application.Abstractions.Queries.Sections;
using Articles.Domain;
using Articles.Domain.Articles;
using Articles.Domain.Catalogs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Articles.Application.Services.QueryHandlers.Sections;

/// <summary>
/// Обработчик запроса на получение статей по заданным тэгам.
/// </summary>
/// <param name="uow">Единица работы</param>
public class GetSectionArticlesQueryHandler(IUnitOfWork uow) : IRequestHandler<GetSectionArticlesQuery, CountResult<ArticleDto>>
{
    /// <summary>
    /// Обрабатывает запрос и возвращает статьи, содержащие указанные тэги.
    /// </summary>
    /// <param name="request">Запрос, содержащий список тегов</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    public async Task<CountResult<ArticleDto>> Handle(GetSectionArticlesQuery request, CancellationToken cancellationToken)
    {
        // Создаем запрос для SectionAggregate
        var articlesQuery = uow.Query<SectionAggregate>()

            // Фильтруем разделы по идентификатору из запроса
            .Where(s => s.Id == request.SectionId)

            // Выполняем групповое соединение с ArticleAggregate
            .GroupJoin(
                // Источник для соединения - статьи
                uow.Query<ArticleAggregate>(),

                // Ключ для соединения из SectionAggregate - хэш тегов
                s => s.TagsHash,

                // Ключ для соединения из ArticleAggregate - хэш тегов
                a => a.TagsHash,
                
                // Результирующий селектор - выбираем статьи
                (s, a) => a
            )
            
            // Преобразуем групповое соединение в плоскую последовательность
            .SelectMany(a => a);

        // Получаем общее количество статей
        var articlesCount = await articlesQuery.CountAsync(cancellationToken: cancellationToken);

        // Если статей нет, возвращаем пустой результат
        if (articlesCount == 0)
            return CountResult<ArticleDto>.NoValues();

        // Получаем статьи с пагинацией
        var articles = await articlesQuery

            // Сортируем по дате обновления (или создания, если обновления нет) в порядке убывания
            .OrderByDescending(a => a.UpdatedAt ?? a.CreatedAt)

            // Проецируем в DTO
            .Select(a => new ArticleDto
            {
                Id = a.Id,
                Title = a.Title,
                Tags = a.Tags.ToArray(),
                CreatedAt = a.CreatedAt,
                UpdatedAt = a.UpdatedAt
            })

            // Пропускаем указанное количество элементов
            .Skip(request.Skip)

            // Берем указанное количество элементов
            .Take(request.Take)

            // Выполняем запрос и получаем массив
            .ToArrayAsync(cancellationToken: cancellationToken);

        // Возвращаем найденные статьи и их количество
        return new CountResult<ArticleDto>
        {
            List = articles,
            TotalCount = articlesCount
        };
    }
}