using Articles.Domain.Catalogs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Articles.Infrastructure.Storage.Configurations;

/// <summary>
/// Конфигурация Fluent API для сущности Report.
/// </summary>
public class SectionConfiguration : IEntityTypeConfiguration<SectionAggregate>
{
    /// <summary>
    /// Настраивает таблицу отчётов.
    /// </summary>
    /// <param name="builder">Строитель для настройки таблицы.</param>
    public void Configure(EntityTypeBuilder<SectionAggregate> builder)
    {
        // Настраиваем таблицу
        builder.ToTable("Sections");
        
        // Устанавливаем первичный ключ
        builder.HasKey(r => r.Id);
        
        //
        builder.PrimitiveCollection(a => a.Tags);

        //
        builder.HasIndex(p => p.TagsHash).IsUnique();
    }
}