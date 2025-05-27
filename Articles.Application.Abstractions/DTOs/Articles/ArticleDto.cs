namespace Articles.Application.Abstractions.DTOs.Articles;

/// <summary>
/// Класс, представляющий данные статьи.
/// </summary>
public class ArticleDto
{
    /// <summary>
    /// Уникальный идентификатор статьи.
    /// </summary>
    public required Guid Id { get; init; }
    
    /// <summary>
    /// Заголовок статьи.
    /// </summary>
    public required string Title { get; init; }
    
    /// <summary>
    /// Список тегов статьи.
    /// </summary>
    public required IReadOnlyList<string> Tags { get; init; }
    
    /// <summary>
    /// Дата создания статьи.
    /// </summary>
    public required DateTime CreatedAt { get; init; }

    /// <summary>
    /// Дата последнего обновления статьи.
    /// </summary>
    public DateTime? UpdatedAt { get; init; }
}