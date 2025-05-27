using Articles.Infrastructure.Storage.DatabaseInitialization;
using Articles.Start.Exceptions;
using Articles.Start.Extensions;

// Создаем объект-строитель приложения ASP.NET Core
var builder = WebApplication.CreateBuilder(args);

// Регистрируем сервисы логгирования
builder.AddLoggingServices();

// Добавляем в приложение сервисы для работы с хранилищами
builder.AddStorageServices();

// Добавление сервисов swagger
builder.Services.AddSwaggerServices();

// Добавляем сервисы CORS
builder.Services.AddCorsServices();

// Добавляем в приложение сервисы для валидации моделей
builder.Services.AddValidationServices();

// Добавляем в приложение сервисы для работы с медиатором
builder.Services.AddMediatorServices();

// Добавляем в приложение сервисы для работы с AutoMapper
builder.Services.AddMappingServices();

// Добавляет сервисы для использования формата сведений о проблеме
builder.Services.AddProblemDetails();

// Добавляем обработчик исключений
builder.Services.AddExceptionHandler<ExceptionHandler>();

// Регистрация контроллеров с поддержкой сериализации JSON
builder.Services.AddControllers();

// Создаем экземпляр приложения ASP.NET Core
var app = builder.Build();

// Создаем область для инициализации баз данных
using (var scope = app.Services.CreateScope())
{
    // Инициализация начальных данных в базу данных
    await DatabaseInitializer.InitAsync(scope.ServiceProvider);
}

// Преобразует необработанные исключения в ответы с подробной информацией о проблеме
app.UseExceptionHandler();

// Включение CORS
app.UseCors("DefaultPolicy");

// Добавляем в приложение middleware для генерации документации API по стандарту OpenAPI
app.UseSwagger();

// Добавляем в приложение middleware для отображения документации API в формате Swagger UI
app.UseSwaggerUI();

// Добавляем в приложение маршрутизацию запросов на контроллеры MVC
app.MapControllers();

// Запускаем приложение ASP.NET Core
await app.RunAsync();