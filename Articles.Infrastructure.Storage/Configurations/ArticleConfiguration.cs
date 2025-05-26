using Articles.Domain.Articles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Articles.Infrastructure.Storage.Configurations;

/// <summary>
/// Конфигурация Fluent API для сущности ArticleAggregate.
/// </summary>
public class ArticleConfiguration : IEntityTypeConfiguration<ArticleAggregate>
{
    /// <summary>
    /// Настраивает таблицу статей.
    /// </summary>
    /// <param name="builder">Строитель для настройки таблицы.</param>
    public void Configure(EntityTypeBuilder<ArticleAggregate> builder)
    {
        // Указываем имя таблицы в БД для хранения статей
        builder.ToTable("Articles");
        
        // Настраиваем поле Id как первичный ключ статьи
        builder.HasKey(r => r.Id);

        // Конфигурируем хранение коллекции тегов статьи
        builder.PrimitiveCollection(a => a.Tags);

        // Устанавливаем максимальную длину для заголовка статьи (256 символов)
        builder.Property(a => a.Title).HasMaxLength(256);

        // Устанавливаем максимальную длину для хэша тегов (256 символов)
        builder.Property(a => a.TagsHash).HasMaxLength(256);

        // Создаем индекс по хэшу тегов для ускорения поиска
        builder.HasIndex(p => p.TagsHash);
    }
}