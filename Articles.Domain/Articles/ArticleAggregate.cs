namespace Articles.Domain.Articles;

/// <summary>
/// Агрегат статьи.
/// </summary>
public class ArticleAggregate
{
    /// <summary>
    /// Идентификатор статьи.
    /// </summary>
    public required Guid Id { get; init; }

    /// <summary>
    /// Название статьи.
    /// </summary>
    public required string Title { get; set; }

    /// <summary>
    /// Название статьи.
    /// </summary>
    public required string Content { get; set; }

    /// <summary>
    /// Дата создания статьи.
    /// </summary>
    public required DateTime Created { get; init; }

    /// <summary>
    /// Дата последнего обновления статьи.
    /// </summary>
    public DateTime? LastEdited { get; set; }

    /// <summary>
    /// Коллекция уникальных тэгов.
    /// </summary>
    public required HashSet<string> Tags { get; set; }
    
    /// <summary>
    /// Метод редактирования статьи.
    /// </summary>
    /// <param name="title">Новый заголовок статьи, если требуется изменение.</param>
    /// <param name="content">Новое текстовое содержимое статьи, если требуется изменение.</param>
    /// <param name="tags">Новый список тегов, если требуется изменение.</param>
    public void Edit(string? title, string? content, List<string>? tags)
    {
        if(title is not null)
            Title = title;
        
        if(content is not null)
            Content = content;
        
        if(tags is not null)
            Tags = tags.ToHashSet();
        
        LastEdited = DateTime.Now;
    }
}