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

        // Настраиваем связь many-to-one с сущностью Report
        builder.HasOne<Report>() // Указывает на последний импортированный отчёт
            .WithMany() // Каждый отчёт может быть последним
            .HasForeignKey(u => u.LastExportedReportId) // Внешний ключ в Exporter
            .OnDelete(DeleteBehavior.SetNull); // Установка внешнего ключа в null при удалении отчёта
    }
}