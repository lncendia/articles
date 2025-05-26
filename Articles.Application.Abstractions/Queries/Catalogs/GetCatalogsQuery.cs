using Articles.Application.Abstractions.DTOs.Common;
using MediatR;
using SmartBot.Abstractions.Interfaces.Utils.DTOs.Catalogs;

namespace Articles.Application.Abstractions.Queries.Catalogs;

public class GetCatalogsQuery : IRequest<CountResult<CatalogDto>>
{
    
}