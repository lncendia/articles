using Test.Domain.Tags;

namespace Test.Domain.Catalogs;

/// <summary>
/// Агрегат раздела.
/// </summary>
public class CatalogAggregate
{
    /// <summary>
    /// Идентификатор раздела.
    /// </summary>
    public required Guid Id { get; set; }
    
    /// <summary>
    /// Название раздела.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Коллекция уникальных тэгов.
    /// </summary>
    public HashSet<string>? Tags { get; set; }
}