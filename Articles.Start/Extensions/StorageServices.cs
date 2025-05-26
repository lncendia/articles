using Articles.Domain;
using Articles.Infrastructure.Storage;
using Articles.Infrastructure.Storage.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Articles.Start.Extensions;

///<summary>
/// Статический класс сервисов хранилища.
///</summary>
public static class StorageServices
{
    /// <summary>
    /// Расширяющий метод для регистрации сервисов хранилища в коллекции служб.
    /// Метод настраивает зависимости для работы с базами данных, файловым хранилищем и другими компонентами системы.
    /// </summary>
    /// <param name="services">Коллекция служб, в которую будут добавлены новые сервисы.</param>
    /// <param name="configuration">Конфигурация приложения, содержащая параметры подключения и пути к хранилищам.</param>
    public static void AddStorageServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Получаем строку подключения к базе данных
        var database = configuration.GetRequiredValue<string>("ConnectionStrings:Database");

        // Добавляем контекст базы данных
        services.AddDbContext<ApplicationDbContext>(opt => { opt.UseNpgsql(database); });

        // Добавляем сервис для работы с единицей работы
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}