using Articles.Infrastructure.Storage.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Articles.Infrastructure.Storage.Contexts;

/// <summary>
/// Контекст базы данных для приложения.
/// </summary>
/// <param name="options">Опции конфигурации для DbContext.</param>
public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    /// <summary>
    /// Конфигурирует модель базы данных.
    /// </summary>
    /// <param name="modelBuilder">Объект для построения модели базы данных.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Применяем конфигурацию для сущности Article
        modelBuilder.ApplyConfiguration(new ArticleConfiguration());

        // Применяем конфигурацию для сущности Section
        modelBuilder.ApplyConfiguration(new SectionConfiguration());
        
        // Вызываем базовую реализацию метода
        base.OnModelCreating(modelBuilder);
    }
}
