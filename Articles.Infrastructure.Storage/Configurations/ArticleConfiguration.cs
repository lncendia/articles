using Articles.Domain.Articles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Articles.Infrastructure.Storage.Configurations;

/// <summary>
/// Конфигурация Fluent API для сущности Exporter.
/// </summary>
public class ArticleConfiguration : IEntityTypeConfiguration<ArticleAggregate>
{
    /// <summary>
    /// Настраивает таблицу экспортера данных.
    /// </summary>
    /// <param name="builder">Строитель для настройки таблицы.</param>
    public void Configure(EntityTypeBuilder<ArticleAggregate> builder)
    {
        // Настраиваем таблицу
        builder.ToTable("Articles");
        
        // Устанавливаем первичный ключ
        builder.HasKey(r => r.Id);

        //
        builder.PrimitiveCollection(a => a.Tags);

        //
        builder.HasIndex(p => p.TagsHash);
    }
}