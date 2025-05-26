using UserAgent.Application.Abstractions.InviteTokenGenerator;
using UserAgent.Application.Abstractions.Localizers;
using UserAgent.Application.Abstractions.NameGenerator;
using UserAgent.Application.Abstractions.PermissionFactory;
using UserAgent.Application.Services.Localizers;
using UserAgent.Infrastructure.Services.InviteTokenGenerator;
using UserAgent.Infrastructure.Services.NameGenerator;
using UserAgent.Infrastructure.Services.PermissionFactory;

namespace UserAgent.Start.Extensions;

///<summary>
/// Статический класс сервисов приложения.
///</summary>
public static class ApplicationServices
{
    ///<summary>
    /// Расширяющий метод для добавления сервисов приложения в коллекцию служб.
    ///</summary>
    ///<param name="services">Коллекция служб.</param>
    public static void AddApplicationServices(this IServiceCollection services)
    {
        // Добавляем сервис генерации имен инстансов
        services.AddSingleton<IInstanceNameGenerator, InstanceNameGenerator>();

        //
        services.AddSingleton<IInviteTokenGenerator, InviteTokenGenerator>(sp=>new InviteTokenGenerator(new InviteTokenOptions
        {
            SecretKey = "fddddsdfgsdfdsfsdfdsdfsdggeas344334ewtsdgffsg3434435545342were",
            ExpirationMinutes = 360
        }));
        
        // Добавляем сервис для локализации разрешений
        services.AddSingleton<IPermissionLocalizer, PermissionLocalizer>();
        
        // Добавляем сервис для локализации ролей рабочего пространства
        services.AddSingleton<IRoleLocalizer, RoleLocalizer>();
        
        // Добавляем сервис для локализации ролей рабочего пространства
        services.AddSingleton<IPermissionFactory, PermissionFactory>();
    }
}