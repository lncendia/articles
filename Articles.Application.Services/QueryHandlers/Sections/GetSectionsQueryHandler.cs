using Articles.Application.Abstractions.DTOs.Common;
using Articles.Application.Abstractions.DTOs.Sections;
using Articles.Application.Abstractions.Queries.Sections;
using Articles.Domain;
using Articles.Domain.Articles;
using Articles.Domain.Catalogs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Articles.Application.Services.QueryHandlers.Sections;

/// <summary>
/// Обработчик запроса на получение разделов.
/// </summary>
/// <param name="uow">Единица работы</param>
public class GetSectionsQueryHandler(IUnitOfWork uow) : IRequestHandler<GetSectionsQuery, CountResult<SectionDto>>
{
    /// <summary>
    /// Обрабатывает запрос и возвращает разделы с количеством статей.
    /// </summary>
    /// <param name="request">Запрос с параметрами пагинации</param>
    /// <param name="cancellationToken">Токен отмены</param>
    public async Task<CountResult<SectionDto>> Handle(GetSectionsQuery request, CancellationToken cancellationToken)
    {
        // Получаем общее количество разделов
        var count = await uow.Query<SectionAggregate>().CountAsync(cancellationToken: cancellationToken);

        // Если разделов нет, возвращаем пустой результат
        if (count == 0)
            return CountResult<SectionDto>.NoValues();

        // Формируем запрос для получения разделов
        var sections = await uow.Query<SectionAggregate>()

            // Делаем групповое соединение со статьями
            .GroupJoin(
                // Таблица статей для соединения
                uow.Query<ArticleAggregate>(),

                // Ключ соединения из разделов - хэш тегов
                s => s.TagsHash,

                // Ключ соединения из статей - хэш тегов
                a => a.TagsHash,

                // Формируем DTO с количеством статей
                (s, a) => new SectionDto
                {
                    Id = s.Id,
                    Tags = s.Tags.ToArray(),
                    ArticleCount = a.Count()
                }
            )

            // Сортируем по убыванию количества статей
            .OrderByDescending(s => s.ArticleCount)

            // Пропускаем указанное количество элементов
            .Skip(request.Skip)

            // Берем указанное количество элементов
            .Take(request.Take)

            // Выполняем запрос
            .ToArrayAsync(cancellationToken: cancellationToken);

        // Возвращаем результат с пагинацией
        return new CountResult<SectionDto>
        {
            List = sections,
            TotalCount = count
        };
    }
}