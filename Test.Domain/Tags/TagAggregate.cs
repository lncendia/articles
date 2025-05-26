namespace Test.Domain.Tags;

/// <summary>
/// Агрегат тэга.
/// </summary>
public class TagAggregate
{ 
    /// <summary>
    /// Название тэга.
    /// </summary>
    public required string Name { get; set; }
}