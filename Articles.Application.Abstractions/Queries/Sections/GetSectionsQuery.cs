using Articles.Application.Abstractions.DTOs.Common;
using Articles.Application.Abstractions.DTOs.Sections;
using MediatR;

namespace Articles.Application.Abstractions.Queries.Sections;

/// <summary>
/// 
/// </summary>
public class GetSectionsQuery : IRequest<CountResult<SectionDto>>
{
    /// <summary>
    /// 
    /// </summary>
    public required int Skip { get; init; }
    
    /// <summary>
    /// 
    /// </summary>
    public required int Take { get; init; }
}