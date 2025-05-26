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
        
        // Устанавливаем ограничения для свойства Comment
        builder.Property(r => r.Comment)
            .HasMaxLength(1500); // Максимальная длина 1500 символов
        
        // Настраиваем связь many-to-one с сущностью User
        builder.HasOne(r => r.User) // У отчёта может быть только один пользователь
            .WithMany(u => u.Reports) // У пользователя может быть много отчётов
            .HasForeignKey(r => r.UserId) // Внешний ключ в Report
            .OnDelete(DeleteBehavior.Cascade); // Каскадное удаление
    }
}