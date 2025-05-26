namespace Articles.Application.Abstractions.DTOs.Articles;

/// <summary>
/// Класс, представляющий данные статьи.
/// </summary>
public class ArticleDto
{
    /// <summary>
    /// Уникальный идентификатор статьи.
    /// </summary>
    public required Guid Id { get; set; }
    
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
    

    /// <summary>
    /// Дата создания статьи.
    /// </summary>
    public required DateTime CreatedAt { get; init; }

    /// <summary>
    /// Дата последнего обновления статьи.
    /// </summary>
    public DateTime? UpdatedAt { get; init; }
}