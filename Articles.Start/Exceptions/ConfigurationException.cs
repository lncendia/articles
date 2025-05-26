namespace Articles.Start.Exceptions;

/// <summary>
/// Исключение, вызывается при отсутствии значения в конфигурации
/// </summary>
/// <param name="path">Путь конфигурации</param>
public class ConfigurationException(string path) : Exception($"The configuration does not contain a path for {path}");