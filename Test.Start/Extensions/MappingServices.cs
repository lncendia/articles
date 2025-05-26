using UserAgent.Infrastructure.Bus.Users.Mapper;
using UserAgent.Infrastructure.Web.Instances.Mappers;

namespace UserAgent.Start.Extensions;

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
            cfg.AddMaps(typeof(InstancesMapperProfile).Assembly);
            
            // Регистрируем карты для событий
            cfg.AddMaps(typeof(UsersMapperProfile).Assembly);
        });
    }
}