namespace Test.Infrastructure.Web.Articles.InputModels;

/// <summary>
/// Модель входных данных для создания статьи.
/// </summary>
public class CreateArticleRequest
{
    /// <summary>
    /// Заголовок статьи.
    /// </summary>
    public string? Title { get; set; }
    
    /// <summary>
    /// Список тэгов статьи.
    /// </summary>
    public List<string>? Tags { get; set; }
    
    /// <summary>
    /// Содержание статьи.
    /// </summary>
    public string? Content { get; set; }
}