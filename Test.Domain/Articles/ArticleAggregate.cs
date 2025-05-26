using Test.Domain.Tags;

namespace Test.Domain.Articles;

/// <summary>
/// Агрегат статьи.
/// </summary>
public class ArticleAggregate
{
    /// <summary>
    /// Идентификатор статьи.
    /// </summary>
    public required Guid Id { get; set; }
    
    /// <summary>
    /// Название статьи.
    /// </summary>
    public required string Name { get; set; }
    
    /// <summary>
    /// Дата создания статьи.
    /// </summary>
    public required DateTime Created { get; set; }
    
    /// <summary>
    /// Дата последнего обновления статьи.
    /// </summary>
    public DateTime? LastEdited { get; set; }

    /// <summary>
    /// Коллекция уникальных тэгов.
    /// </summary>
    public HashSet<string>? Tags { get; set; }
}