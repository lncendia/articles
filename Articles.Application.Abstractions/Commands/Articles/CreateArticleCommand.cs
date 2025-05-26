using MediatR;

namespace Articles.Application.Abstractions.Commands.Articles;

/// <summary>
/// Команда для создания статьи.
/// </summary>
public class CreateArticleCommand : IRequest<Guid>
{
    /// <summary>
    /// Заголовок статьи.
    /// </summary>
    public required string Title { get; init; }
    
    /// <summary>
    /// Список тэгов статьи.
    /// </summary>
    public List<string>? Tags { get; init; }
    
    /// <summary>
    /// Содержание статьи.
    /// </summary>
    public required string Content { get; init; }
}