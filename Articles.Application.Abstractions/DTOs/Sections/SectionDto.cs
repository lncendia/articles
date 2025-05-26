namespace Articles.Application.Abstractions.DTOs.Sections;
/// <summary>
/// DTO для представления раздела
/// </summary>
public class SectionDto
{
    /// <summary>
    /// Идентификатор раздела
    /// </summary>
    public required Guid Id { get; init; }
 
    /// <summary>
    /// Название раздела (формируется из тегов, обрезается до 1024 символов)
    /// </summary>
    public string Name => GetTruncatedName();

    /// <summary>
    /// Получает сокращенное название раздела
    /// </summary>
    /// <remarks>
    /// Формирует название из списка тегов, разделенных запятыми.
    /// Если длина превышает 1024 символа, обрезает до 1021 символа и добавляет многоточие.
    /// </remarks>
    /// <returns>Название раздела с ограничением длины</returns>
    private string GetTruncatedName()
    {
        // Формируем полное название из всех тегов
        var fullName = string.Join(", ", Tags);
        const int maxLength = 1024;
    
        // Возвращаем либо полное название (если укладывается в лимит),
        // либо обрезанную версию с многоточием
        return fullName.Length <= maxLength 
            ? fullName 
            : string.Concat(fullName.AsSpan(0, maxLength - 3), "...");
    }
    
    /// <summary>
    /// Теги раздела
    /// </summary>
    public required string[] Tags { get; init; }
    
    /// <summary>
    /// Количество статей в разделе
    /// </summary>
    public required int ArticleCount { get; init; }
}