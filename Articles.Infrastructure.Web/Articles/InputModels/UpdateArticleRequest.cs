namespace Articles.Infrastructure.Web.Articles.InputModels;

/// <summary>
/// Модель входных данных для изменения статьи.
/// </summary>
public class UpdateArticleRequest
{
    /// <summary>
    /// Заголовок статьи.
    /// </summary>
    public string? Title { get; init; }

    /// <summary>
    /// Список тегов статьи.
    /// </summary>
    public string[] Tags { get; init; } = [];
}