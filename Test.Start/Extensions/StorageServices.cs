using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;
using UserAgent.Domain.Abstractions.Interfaces;
using UserAgent.Infrastructure.Storage;
using UserAgent.Infrastructure.Storage.Context;

namespace UserAgent.Start.Extensions;

///<summary>
/// Статический класс сервисов хранилища.
///</summary>
public static class StorageServices
{
    ///<summary>
    /// Расширяющий метод для добавления сервисов хранилища в коллекцию служб.
    ///</summary>
    ///<param name="services">Коллекция служб.</param>
    ///<param name="configuration">Конфигурация приложения.</param>
    public static void AddStorageServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Получаем секцию конфигурации, содержащую параметры подключения к базе данных.
        var database = configuration.GetSection("ConnectionStrings:Database");

        // Извлекаем строку подключения к базе данных из конфигурации.
        var connectionString = database.GetRequiredValue<string>("ConnectionString");

        // Извлекаем имя базы данных для приложения из конфигурации.
        var applicationDatabaseName = database.GetRequiredValue<string>("ApplicationDb");
        
        // Регистрируем IMongoClient как синглтон в DI-контейнере.
        // IMongoClient — это основной клиент для работы с MongoDB, который управляет подключениями к базе данных.
        services.AddSingleton<IMongoClient, MongoClient>(sp =>
        {
            // Создаем настройки для MongoClient из строки подключения.
            var mongoSettings = MongoClientSettings.FromConnectionString(connectionString);

            // Настраиваем логирование для MongoClient, используя ILoggerFactory из DI-контейнера.
            // Это позволяет логировать операции MongoDB в указанную систему логирования (например, в консоль или файл).
            mongoSettings.LoggingSettings = new LoggingSettings(sp.GetRequiredService<ILoggerFactory>());

            // Создаем и возвращаем новый экземпляр MongoClient с настроенными параметрами.
            return new MongoClient(mongoSettings);
        });
        
        // Регистрируем MongoDbFacade как синглтон.
        // MongoDbFacade — это фасад, который упрощает работу с MongoDB, предоставляя удобный доступ к коллекциям.
        services.AddSingleton<MongoDbContext>(sp => new MongoDbContext(
            
            // Получаем IMongoClient из DI-контейнера.
            sp.GetRequiredService<IMongoClient>(),

            // Передаем имя базы данных.
            applicationDatabaseName
        ));
        
        // Добавление Unit of work
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        // Регистрируем сериализатор для типа Guid, чтобы он использовал стандартное представление (GuidRepresentation.Standard).
        // Это гарантирует корректную работу с GUID в MongoDB.
        BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
    }
}