namespace Articles.Start.Extensions;

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