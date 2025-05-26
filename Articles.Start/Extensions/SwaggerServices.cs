using Articles.Application.Abstractions.DTOs.Articles;
using Articles.Infrastructure.Web.Articles.Controllers;
using Microsoft.OpenApi.Models;

namespace Articles.Start.Extensions;

/// <summary>
/// Класс для настройки Swagger в приложении.
/// </summary>
/// <remarks>
/// Содержит методы конфигурации Swagger UI и генерации документации API.
/// </remarks>
public static class SwaggerServices
{
    /// <summary>
    /// Добавляет и настраивает сервисы Swagger в DI-контейнер.
    /// </summary>
    /// <remarks>
    /// Выполняет следующие настройки:
    /// - Генерацию документации API на основе XML-комментариев
    /// - Добавление информации о версии API
    /// - Подключение XML-документации для всех сборок проекта
    /// </remarks>
    /// <param name="services">Коллекция сервисов</param>
    public static void AddSwaggerServices(this IServiceCollection services)
    {
        // Регистрация генератора Swagger с настройками
        services.AddSwaggerGen(options =>
        {
            // Получение имени XML-файла документации для сборки с контроллерами
            var xmlWebFile = $"{typeof(ArticlesController).Assembly.GetName().Name}.xml";

            // Получение имени XML-файла документации для сборки с DTO
            var xmlAbstractionsFile = $"{typeof(ArticleDto).Assembly.GetName().Name}.xml";

            // Включение XML-комментариев в документацию Swagger
            foreach (var file in new[] { xmlWebFile, xmlAbstractionsFile })
            {
                // Добавление XML-файла документации, если он существует
                var xmlPath = Path.Combine(AppContext.BaseDirectory, file);
                if (File.Exists(xmlPath))
                {
                    options.IncludeXmlComments(xmlPath);
                }
            }

            // Настройка основной информации о API
            options.SwaggerDoc("v1", new OpenApiInfo 
            { 
                Title = "Articles API", 
                Version = "v1",
                Description = "API для работы со статьями и разделами",
                Contact = new OpenApiContact
                {
                    Name = "Sergei Zakharov"
                }
            });
        });
    }
}