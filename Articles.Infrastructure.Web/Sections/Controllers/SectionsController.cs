using Articles.Application.Abstractions.DTOs.Articles;
using Articles.Application.Abstractions.DTOs.Common;
using Articles.Application.Abstractions.DTOs.Sections;
using Articles.Application.Abstractions.Queries.Sections;
using Articles.Infrastructure.Web.Sections.InputModels;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Articles.Infrastructure.Web.Sections.Controllers;

/// <summary>
/// Контроллер для работы с разделами статей
/// </summary>
/// <remarks>
/// Предоставляет API для получения списка разделов и статей в конкретном разделе
/// </remarks>
/// <param name="mediator">Медиатор для обработки CQRS-запросов</param>
/// <param name="mapper">Автомаппер для преобразования DTO</param>
[ApiController]
[Route("api/[controller]")]
public class SectionsController(ISender mediator, IMapper mapper) : ControllerBase
{
    /// <summary>
    /// Получение списка разделов с пагинацией
    /// </summary>
    /// <remarks>
    /// Возвращает список всех разделов, отсортированных по количеству статей в каждом разделе (по убыванию).
    /// Каждый элемент содержит идентификатор раздела, список тегов и количество статей.
    /// </remarks>
    /// <param name="request">Параметры запроса</param>
    /// <param name="token">Токен отмены операции</param>
    /// <returns>Список разделов с общим количеством</returns>
    /// <response code="200">Успешное выполнение запроса</response>
    /// <response code="400">Некорректные параметры запроса</response>
    /// <response code="500">Внутренняя ошибка сервера</response>
    [HttpGet]
    [ProducesResponseType(typeof(CountResult<SectionDto>), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<CountResult<SectionDto>> GetCatalogs([FromQuery] GetSectionsRequest request, CancellationToken token)
    {
        // Преобразуем DTO запроса в CQRS-команду
        var query = mapper.Map<GetSectionsQuery>(request);
        
        // Отправляем команду через медиатор и возвращаем результат
        return await mediator.Send(query, token);
    }

    /// <summary>
    /// Получение статей в конкретном разделе
    /// </summary>
    /// <remarks>
    /// Возвращает статьи, принадлежащие указанному разделу (по SectionId).
    /// Статьи сортируются по дате обновления (сначала новые).
    /// </remarks>
    /// <param name="id">Идентификатор раздела</param>
    /// <param name="request">Параметры запроса</param>
    /// <param name="token">Токен отмены операции</param>
    /// <returns>Список статей с общим количеством</returns>
    /// <response code="200">Успешное выполнение запроса</response>
    /// <response code="400">Некорректные параметры запроса</response>
    /// <response code="500">Внутренняя ошибка сервера</response>
    [HttpGet("Articles/{id:guid}")]
    [ProducesResponseType(typeof(CountResult<ArticleDto>), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<CountResult<ArticleDto>> GetArticlesInSection(Guid id, [FromQuery] GetArticlesRequest request, CancellationToken token)
    {
        // Преобразуем DTO запроса в CQRS-команду
        var query = mapper.Map<GetSectionArticlesQuery>(request, options =>
        {
            options.Items.Add("Id", id);
        });
        
        // Отправляем команду через медиатор и возвращаем результат
        return await mediator.Send(query, token);
    }
}