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

        // Правило для Content
        RuleFor(c => c.Content)
            
            // Максимальная длина 256 символов
            .MaximumLength(256)

            // С сообщением
            .WithMessage("Content must not exceed 256 characters.");

        // Правило для Tags
        RuleForEach(c => c.Tags)
            
            // Максимальная длина 256 символов
            .MaximumLength(256)

            // С сообщением
            .WithMessage("Tags must not exceed 256 characters.");
    }
}