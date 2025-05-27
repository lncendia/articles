using Articles.Application.Abstractions.DTOs.Common;
using Articles.Application.Abstractions.DTOs.Sections;
using MediatR;

namespace Articles.Application.Abstractions.Queries.Sections;

/// <summary>
/// Запрос на получение списка разделов с пагинацией
/// </summary>
/// <remarks>
/// Содержит параметры для постраничной выборки всех разделов,
/// отсортированных по количеству статей
/// </remarks>
public class GetSectionsQuery : IRequest<CountResult<SectionDto>>
{
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