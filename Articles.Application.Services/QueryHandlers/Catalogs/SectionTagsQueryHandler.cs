using System.ComponentModel.Design;
using MediatR;
using Articles.Application.Abstractions.DTOs.Articles;
using Articles.Application.Abstractions.DTOs.Common;
using Articles.Application.Abstractions.Queries.Catalogs;
using Articles.Domain;
using Articles.Domain.Articles;
using Microsoft.EntityFrameworkCore;

namespace Articles.Application.Services.QueryHandlers.Catalogs;

/// <summary>
/// Обработчик запроса на получение статей по заданным тэгам.
/// </summary>
/// <param name="uow">Единица работы</param>
public class SectionTagsQueryHandler(
    IUnitOfWork uow) : IRequestHandler<SectionTagsQuery, CountResult<ArticleDto>>
{
    /// <summary>
    /// Обрабатывает запрос и возвращает статьи, содержащие указанные тэги.
    /// </summary>
    /// <param name="request">Запрос, содержащий список тэгов</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    public async Task<CountResult<ArticleDto>> Handle(SectionTagsQuery request, CancellationToken cancellationToken)
    {
        // Выполняем запрос к базе
        var articleDtos = await uow.Query<ArticleAggregate>()
            
            // Фильруем статьи по тэгам
            .Where(a => a.Tags.Equals(request.Tags))
        
            // Преобразуем сущности в DTO-объекты
            .Select(a => new ArticleDto
            {
                Id = a.Id,
                Title = a.Title,
                Content = a.Content,
                Tags = a.Tags.ToList(),
                Created = a.Created,
                LastEdited = a.LastEdited
            })
            
            // Преобразуем результат в список
            .ToListAsync(cancellationToken);
        
        // Если совпадений не найдено — возвращаем пустой результат
        if (articleDtos.Count == 0) 
            return  CountResult<ArticleDto>.NoValues();
        
        // Возвращаем найденные статьи и их количество
        return new CountResult<ArticleDto>
        {
            List = articleDtos,
            TotalCount = articleDtos.Count
        };
    }
}