namespace Articles.Domain.Catalogs;

/// <summary>
/// Агрегат раздела.
/// </summary>
public class SectionAggregate
{
    /// <summary>
    /// Идентификатор раздела.
    /// </summary>
    public required Guid Id { get; set; }

    /// <summary>
    /// Название раздела.
    /// </summary>
    public string Name => string.Join(", ", Tags);

    /// <summary>
    /// Коллекция уникальных тэгов.
    /// </summary>
    public required HashSet<string> Tags { get; init; }
}