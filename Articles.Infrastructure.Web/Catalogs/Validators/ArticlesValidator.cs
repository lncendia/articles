using Articles.Infrastructure.Web.Catalogs.InputModels;
using FluentValidation;

namespace Articles.Infrastructure.Web.Catalogs.Validators;

/// <summary>
/// Валидатор для ArticlesRequest
/// </summary>
public class ArticlesValidator : AbstractValidator<ArticlesRequest>
{
    /// <summary>
    /// Конструктор валидатора
    /// </summary>
    public ArticlesValidator()
    { 
        // Правило для Tags
        RuleForEach(c => c.Tags)
            
            // Максимальная длина 256 символов
            .MaximumLength(256)

            // С сообщением
            .WithMessage("Tags must not exceed 256 characters.");
    }
}