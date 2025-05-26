using Articles.Infrastructure.Web.Articles.InputModels;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Articles.Infrastructure.Web.Articles.Validators;

/// <summary>
/// Валидатор для CreateArticleRequest
/// </summary>
public class CreateArticleValidator : AbstractValidator<CreateArticleRequest>
{
    /// <summary>
    /// Конструктор валидатора
    /// </summary>
    public CreateArticleValidator()
    {
        // Правило для Title
        RuleFor(c => c.Title)

            // Не пустое
            .NotEmpty()
            
            // С сообщением
            .WithMessage("Title is required.")
            
            // Максимальная длина 256 символов
            .MaximumLength(256)

            // С сообщением
            .WithMessage("Title must not exceed 256 characters.");

        // Правило для Content
        RuleFor(c => c.Content)
            
            // Не пустое
            .NotEmpty()
            
            // С сообщением
            .WithMessage("Content is required.")
            
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