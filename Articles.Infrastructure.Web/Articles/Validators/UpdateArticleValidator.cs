using Articles.Infrastructure.Web.Articles.InputModels;
using FluentValidation;

namespace Articles.Infrastructure.Web.Articles.Validators;

/// <summary>
/// Валидатор для UpdateArticleValidator
/// </summary>
public class UpdateArticleValidator : AbstractValidator<UpdateArticleRequest>
{
    /// <summary>
    /// Конструктор валидатора
    /// </summary>
    public UpdateArticleValidator()
    {
        // Правило для Title
        RuleFor(c => c.Title)
            
            // Максимальная длина 256 символов
            .MaximumLength(256)

            // С сообщением
            .WithMessage("Title must not exceed 256 characters.");
        
        // Правило для Tags
        RuleFor(c => c.Tags)

            // Не больше 15 тегов
            .Must(c => c.Length <= 256)

            // С сообщением
            .WithMessage("The number of tags cannot exceed 256.");

        // Правило для Tags
        RuleForEach(c => c.Tags)
            
            // Максимальная длина 256 символов
            .MaximumLength(256)

            // С сообщением
            .WithMessage("Tags must not exceed 256 characters.");
    }
}