namespace Articles.Start.Exceptions;

/// <summary>
/// Исключение, вызывается при отсутствии значения в конфигурации
/// </summary>
public class ConfigurationException : Exception
{
    /// <summary>
    /// Путь конфигурации
    /// </summary>
    public required string Path { get; set; }

    
    /// <summary>
    /// Конструктор исключения
    /// </summary>
    /// <param name="path">Путь конфигурации</param>
    public ConfigurationException(string path) : base($"The configuration does not contain a path for {path}")
    {
        Path = path;
    }
};