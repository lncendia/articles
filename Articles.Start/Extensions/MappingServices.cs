using Articles.Infrastructure.Web.Sections.Mappers;

namespace Articles.Start.Extensions;

///<summary>
/// Статический класс сервисов мапинга.
///</summary>
public static class MappingServices
{
    ///<summary>
    /// Расширяющий метод для добавления сервисов мапинга в коллекцию служб.
    ///</summary>
    ///<param name="services">Коллекция служб.</param>
    public static void AddMappingServices(this IServiceCollection services)
    {
        // Добавляем AutoMapper в сервисы
        services.AddAutoMapper(cfg =>
        {
            // Регистрируем карты для контроллеров
            cfg.AddMaps(typeof(SectionsMapperProfile).Assembly);
        });
    }
}