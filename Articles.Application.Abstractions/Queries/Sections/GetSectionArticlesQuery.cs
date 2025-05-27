using Articles.Application.Abstractions.DTOs.Articles;
using Articles.Application.Abstractions.DTOs.Common;
using MediatR;

namespace Articles.Application.Abstractions.Queries.Sections;

/// <summary>
/// Запрос на получение статей в разделе с пагинацией
/// </summary>
/// <remarks>
/// Содержит параметры для постраничной выборки статей конкретного раздела
/// </remarks>
public class GetSectionArticlesQuery : IRequest<CountResult<ArticleDto>>
{
    /// <summary>
    /// Идентификатор раздела
    /// </summary>
    public required Guid SectionId { get; init; }
    
    /// <summary>
    /// Количество пропускаемых элементов
    /// </summary>
    /// <value>Минимальное значение: 0</value>
    public required int Skip { get; init; }
    
    /// <summary>
    /// Количество получаемых элементов
    /// </summary>
    /// <value>Минимальное значение: 1, Максимальное значение: 100</value>
    public required int Take { get; init; }
}