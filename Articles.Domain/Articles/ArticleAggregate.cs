using System.Security.Cryptography;
using System.Text;

namespace Articles.Domain.Articles;

/// <summary>
/// Агрегат статьи.
/// </summary>
public class ArticleAggregate
{
    /// <summary>
    /// Идентификатор статьи.
    /// </summary>
    public Guid Id { get; init; } = Guid.NewGuid();

    /// <summary>
    /// Название статьи.
    /// </summary>
    public required string Title { get; set; }

    /// <summary>
    /// Дата создания статьи.
    /// </summary>
    public required DateTime CreatedAt { get; init; }

    /// <summary>
    /// Дата последнего обновления статьи.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Коллекция уникальных тегов.
    /// </summary>
    public string[] Tags { get; private set; } = null!;

    /// <summary>
    /// Хэш-сумма тегов статьи.
    /// </summary>
    public string TagsHash { get; private set; } = null!;

    /// <summary>
    /// Устанавливает теги для статьи и вычисляет их хэш.
    /// </summary>
    /// <param name="tags">Коллекция тегов</param>
    public void SetTags(IEnumerable<string> tags)
    {
        // Обрабатываем входные теги
        var uniqueTags = tags

            // Удаляем пробелы в начале и конце каждого тега
            .Select(tag => tag.Trim())

            // Удаляем дубликаты (без учета регистра)
            .Distinct(StringComparer.OrdinalIgnoreCase)

            // Преобразуем в список
            .ToArray();

        // Создаем HashSet из обработанных тегов
        Tags = uniqueTags;

        // Сортируем теги
        var uniqueSortedTags = uniqueTags.OrderBy(tag => tag, StringComparer.Ordinal);

        // Склеиваем теги с разделителем (например, \0, чтобы исключить слияния)
        var concatenated = string.Join("\0", uniqueSortedTags);

        // Вычисляем SHA256 хэш от объединенных тегов
        var hashBytes = SHA256.HashData(Encoding.UTF8.GetBytes(concatenated));

        // Конвертируем байты хэша в base64 строку
        TagsHash = Convert.ToBase64String(hashBytes);
    }
}