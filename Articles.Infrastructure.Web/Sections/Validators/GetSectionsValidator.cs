using Articles.Infrastructure.Web.Sections.InputModels;
using FluentValidation;

namespace Articles.Infrastructure.Web.Sections.Validators;

/// <summary>
/// Валидатор для GetSectionsRequest
/// </summary>
public class GetSectionsValidator : AbstractValidator<GetSectionsRequest>
{
    /// <summary>
    /// Конструктор валидатора
    /// </summary>
    public GetSectionsValidator()
    {
        // Правило для Skip
        RuleFor(c => c.Skip)

            // Больше либо равно нулю
            .GreaterThanOrEqualTo(0)

            // С сообщением
            .WithMessage("Skip value must be greater than or equal to 0.");
        
        // Правило для Take
        RuleFor(c => c.Take)

            // Больше нуля
            .GreaterThan(0)

            // С сообщением
            .WithMessage("Take value must be greater than 0.")

            // Меньше или равно 100
            .LessThanOrEqualTo(100)

            // С сообщением
            .WithMessage("Take value must be less than or equal to 100.");
    }
}