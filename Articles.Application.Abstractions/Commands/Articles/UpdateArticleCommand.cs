using MediatR;

namespace Articles.Application.Abstractions.Commands.Articles;

/// <summary>
/// Команда для обновления статьи.
/// </summary>
public class UpdateArticleCommand : IRequest
{
    /// <summary>
    /// Уникальный идентификатор статьи.
    /// </summary>
    public required Guid Id { get; init; }
    
    /// <summary>
    /// Заголовок статьи.
    /// </summary>
    public string? Title { get; init; }
    
    /// <summary>
    /// Список тэгов статьи.
    /// </summary>
    public List<string>? Tags { get; init; }
    
    /// <summary>
    /// /// Содержание статьи.
    /// </summary>
    public string? Content { get; init; }
}