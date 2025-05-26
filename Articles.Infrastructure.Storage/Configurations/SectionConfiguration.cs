using Articles.Domain.Catalogs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Articles.Infrastructure.Storage.Configurations;

/// <summary>
/// Конфигурация Fluent API для сущности SectionAggregate.
/// </summary>
public class SectionConfiguration : IEntityTypeConfiguration<SectionAggregate>
{
    /// <summary>
    /// Настраивает таблицу разделов.
    /// </summary>
    /// <param name="builder">Строитель для настройки таблицы.</param>
    public void Configure(EntityTypeBuilder<SectionAggregate> builder)
    {
        // Указываем имя таблицы в БД
        builder.ToTable("Sections");
        
        // Настраиваем поле Id как первичный ключ
        builder.HasKey(r => r.Id);
        
        // Конфигурируем хранение коллекции тегов
        builder.PrimitiveCollection(a => a.Tags);
        
        // Устанавливаем максимальную длину для хэша тегов (256 символов)
        builder.Property(a => a.TagsHash).HasMaxLength(256);

        // Создаем уникальный индекс по хэшу тегов
        builder.HasIndex(p => p.TagsHash).IsUnique();
    }
}