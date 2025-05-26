using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using UserAgent.Infrastructure.Storage.DatabaseInitialization;
using UserAgent.Start.Exceptions;
using UserAgent.Start.Extensions;

// Создаем объект-строитель приложения ASP.NET Core
var builder = WebApplication.CreateBuilder(args);

// Добавляем конфигурацию из Vault
builder.AddVaultConfiguration();

// Регистрируем сервисы логгирования
builder.AddLoggingServices();

// Добавление служб авторизации
builder.AddJwtAuthorization();

// Добавление сервисов swagger
builder.AddSwaggerServices();

// Добавление сервисов инфраструктуры
builder.AddInfrastructureServices();

// Добавляем сервисы CORS
builder.Services.AddCorsServices();

// Добавляем в приложение сервисы для работы с хранилищами
builder.Services.AddStorageServices(builder.Configuration);

// Добавляем в приложение сервисы для работы с сообщениями MassTransit
builder.Services.AddMassTransitServices(builder.Configuration);

// Добавляем сервисы приложения
builder.Services.AddApplicationServices();

// Добавляем в приложение сервисы для валидации моделей
builder.Services.AddValidationServices();

// Добавление служб локализации
builder.Services.AddLocalizationServices();

// Добавляем в приложение сервисы для работы с медиатором
builder.Services.AddMediatorServices();

// Добавляем в приложение сервисы для работы с AutoMapper
builder.Services.AddMappingServices();

// Добавление поддержки NewtonsowtJson для swagger
builder.Services.AddSwaggerGenNewtonsoftSupport();

// Добавляет сервисы для использования формата сведений о проблеме
builder.Services.AddProblemDetails();

// Добавляем обработчик исключений
builder.Services.AddExceptionHandler<ExceptionHandler>();

// Регистрация контроллеров с поддержкой сериализации JSON
builder.Services.AddControllers().AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.Converters.Add(new StringEnumConverter());
        options.SerializerSettings.Formatting = Formatting.Indented;
        options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
    }
);

// Создаем экземпляр приложения ASP.NET Core
var app = builder.Build();

// Создаем область для инициализации баз данных
using (var scope = app.Services.CreateScope())
{
    // Инициализация начальных данных в базу данных
    await DatabaseInitializer.InitAsync(scope.ServiceProvider, builder.Environment.EnvironmentName);
}

// Преобразует необработанные исключения в ответы с подробной информацией о проблеме
app.UseExceptionHandler();

// Включение CORS
app.UseCors("DefaultPolicy");

// Добавляем в приложение middleware для аутентификации
app.UseAuthentication();

// Добавляем в приложение middleware для авторизации
app.UseAuthorization();

// Добавляем мидлварь локализации
app.UseRequestLocalization();

// Используется отправка статических файлов (wwwroot)
app.UseStaticFiles();

// Добавляем в приложение middleware для генерации документации API по стандарту OpenAPI
app.UseSwagger();

// Добавляем в приложение middleware для отображения документации API в формате Swagger UI
app.UseSwaggerUI(c =>
{
    // Определяем путь к html файлу с элементом select для выбора языка 
    var select = Path.Combine(builder.Environment.WebRootPath, "swagger/swagger-language-select.html");

    // Устанавливаем select в верхушку файла swagger
    c.HeadContent = File.ReadAllText(select);

    // Внедряем js файл со скриптом к select в html swagger страницу
    c.InjectJavascript("/swagger/swagger-language-select.js");

    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");

    // Настройте Swagger UI для использования OAuth2
    c.OAuthClientId("swagger");
    c.OAuthAppName("Swagger UI");

    // Использование PKCE (Proof Key for Code Exchange) с авторизационным кодом
    c.OAuthUsePkce();
});

// Добавляем в приложение маршрутизацию запросов на контроллеры MVC
app.MapControllers();

// Запускаем приложение ASP.NET Core
await app.RunAsync();