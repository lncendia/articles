namespace Articles.Infrastructure.Web.Articles.InputModels;

/// <summary>
/// Модель входных данных для создания статьи.
/// </summary>
public class CreateArticleRequest
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