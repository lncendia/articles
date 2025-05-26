namespace Articles.Application.Abstractions.Exceptions;

/// <summary>
/// Исключение возникающие, если статья не была найдена.
/// </summary>
public class ArticleNotFoundException : Exception
{
    /// <summary>
    /// Идентификатор статьи, который не был найден.
    /// </summary>
    public Guid ArticleId { get; }

    /// <summary>
    /// Конструктор исключения.
    /// </summary>
    /// <param name="articleId">Идентификатор статьи.</param>
    public ArticleNotFoundException(Guid articleId) : base($"Article with ID {articleId} not found.")
    {
        ArticleId = articleId;
    }
}