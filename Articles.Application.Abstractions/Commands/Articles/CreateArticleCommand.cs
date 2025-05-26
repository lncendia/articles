using System.Windows.Input;
using MediatR;

namespace Articles.Application.Abstractions.Commands.Articles;

/// <summary>
/// Команда для создания статьи.
/// </summary>
public class CreateArticleCommand : IRequest
{
    /// <summary>
    /// Уникальный идентификатор статьи.
    /// </summary>
    public required Guid Id { get; init; } = Guid.NewGuid();
    
    /// <summary>
    /// Заголовок статьи.
    /// </summary>
    public required string Title { get; set; }
    
    /// <summary>
    /// Список тэгов статьи.
    /// </summary>
    public List<string>? Tags { get; set; }
    
    /// <summary>
    /// Содержание статьи.
    /// </summary>
    public required string Content { get; set; }
}