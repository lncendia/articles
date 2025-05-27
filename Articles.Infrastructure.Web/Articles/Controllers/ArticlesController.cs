using Articles.Infrastructure.Web.Articles.InputModels;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Articles.Application.Abstractions.Commands.Articles;
using Articles.Application.Abstractions.DTOs.Articles;
using Articles.Application.Abstractions.Queries.Articles;

namespace Articles.Infrastructure.Web.Articles.Controllers;

/// <summary>
/// Контроллер для работы со статьями
/// </summary>
/// <remarks>
/// Предоставляет API для создания, получения и обновления статей
/// </remarks>
/// <param name="mediator">Медиатор для обработки CQRS-команд</param>
/// <param name="mapper">Автомаппер для преобразования объектов</param>
[ApiController]
[Route("api/[controller]")]
public class ArticlesController(ISender mediator, IMapper mapper) : ControllerBase
{
    /// <summary>
    /// Получение статьи по идентификатору
    /// </summary>
    /// <remarks>
    /// Возвращает полную информацию о статье, включая заголовок, содержимое и теги
    /// </remarks>
    /// <param name="id">Идентификатор статьи (GUID)</param>
    /// <param name="token">Токен отмены операции</param>
    /// <returns>Информация о статье</returns>
    /// <response code="200">Статья успешно найдена</response>
    /// <response code="404">Статья с указанным идентификатором не найдена</response>
    /// <response code="500">Внутренняя ошибка сервера</response>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ArticleDto), 200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ArticleDto> GetArticleById(Guid id, CancellationToken token)
    {
        // Преобразуем DTO запроса в CQRS-команду
        var query = new GetArticleQuery { ArticleId = id };
        
        // Отправляем команду через медиатор и возвращаем результат
        return await mediator.Send(query, token);
    }

    /// <summary>
    /// Создание новой статьи
    /// </summary>
    /// <remarks>
    /// Создает новую статью с указанными заголовком, содержимым и тегами.
    /// При необходимости создает новый раздел (section) для указанных тегов.
    /// </remarks>
    /// <param name="request">Данные для создания статьи</param>
    /// <param name="token">Токен отмены операции</param>
    /// <returns>Идентификатор созданной статьи и ссылка на метод получения</returns>
    /// <response code="201">Статья успешно создана</response>
    /// <response code="400">Некорректные данные статьи</response>
    /// <response code="500">Внутренняя ошибка сервера</response>
    [HttpPut]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> CreateArticle([FromBody] CreateArticleRequest request, CancellationToken token)
    {
        // Преобразуем DTO запроса в CQRS-команду
        var command = mapper.Map<CreateArticleCommand>(request);
            
        // Отправляем команду через медиатор
        var id = await mediator.Send(command, token);
        
        // Возвращаем Created
        return CreatedAtAction(nameof(GetArticleById), new { id }, id);
    }

    /// <summary>
    /// Обновление существующей статьи
    /// </summary>
    /// <remarks>
    /// Обновляет заголовок, содержимое и/или теги статьи.
    /// При изменении тегов может измениться принадлежность к разделу (section).
    /// </remarks>
    /// <param name="id">Идентификатор статьи для обновления</param>
    /// <param name="request">Новые данные статьи</param>
    /// <param name="token">Токен отмены операции</param>
    /// <returns>Результат выполнения операции</returns>
    /// <response code="200">Статья успешно обновлена</response>
    /// <response code="400">Некорректные данные статьи</response>
    /// <response code="404">Статья не найдена</response>
    /// <response code="500">Внутренняя ошибка сервера</response>
    [HttpPost("{id:guid}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> UpdateArticle(Guid id, [FromBody] UpdateArticleRequest request, CancellationToken token)
    {
        // Преобразуем DTO запроса в CQRS-команду
        var command = mapper.Map<UpdateArticleCommand>(request, options =>
        {
            options.Items.Add("Id", id);
        });
            
        // Отправляем команду через медиатор
        await mediator.Send(command, token);
            
        // Возвращаем Ok
        return Ok();
    }
}