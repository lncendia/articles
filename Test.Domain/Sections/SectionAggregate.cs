using Test.Domain.Tags;

namespace Test.Domain.Sections;

/// <summary>
/// Агрегат раздела.
/// </summary>
public class SectionAggregate
{
    /// <summary>
    /// Название раздела.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Коллекция уникальных тэгов.
    /// </summary>
    public HashSet<string>? Tags { get; set; }
}