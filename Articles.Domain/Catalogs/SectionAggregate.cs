using System.Security.Cryptography;
using System.Text;

namespace Articles.Domain.Catalogs;

/// <summary>
/// Агрегат раздела.
/// </summary>
public class SectionAggregate
{
    /// <summary>
    /// Идентификатор раздела.
    /// </summary>
    public Guid Id { get; init; } = Guid.NewGuid();

    /// <summary>
    /// Название раздела.
    /// </summary>
    public string Name => string.Join(", ", Tags);

    /// <summary>
    /// Коллекция уникальных тэгов.
    /// </summary>
    public HashSet<string> Tags { get; private set; } = null!;

    /// <summary>
    /// 
    /// </summary>
    public string TagsHash { get; private set; } = null!;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tags"></param>
    public void SetTags(IEnumerable<string> tags)
    {
        var uniqueSortedTags = tags
            .Select(tag => tag.Trim())
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .OrderBy(tag => tag, StringComparer.Ordinal)
            .ToList();

        Tags = new HashSet<string>(uniqueSortedTags);

        // Склеиваем теги с разделителем (например, \0, чтобы исключить слияния)
        var concatenated = string.Join("\0", uniqueSortedTags);
        var hashBytes = SHA256.HashData(Encoding.UTF8.GetBytes(concatenated));

        TagsHash = Convert.ToBase64String(hashBytes);
    }
}