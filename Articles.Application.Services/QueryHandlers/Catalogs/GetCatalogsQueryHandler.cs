using Articles.Application.Abstractions.DTOs.Common;
using Articles.Application.Abstractions.Queries.Catalogs;
using Articles.Domain;
using Articles.Domain.Catalogs;
using MediatR;
using SmartBot.Abstractions.Interfaces.Utils.DTOs.Catalogs;

namespace Articles.Application.Services.QueryHandlers.Catalogs;

/// <summary>
/// Обработчик запроса на получение разделов.
/// </summary>
/// <param name="uow">Единица работ</param>
public class GetCatalogsQueryHandler(IUnitOfWork uow) : IRequestHandler<GetCatalogsQuery, CountResult<CatalogDto>>
{
    /// <summary>
    /// Обрабатывает запрос и возвращает статьи, содержащие указанные тэги.
    /// </summary>
    /// <param name="request">Запрос, содержащий список тэгов</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    public async Task<CountResult<CatalogDto>> Handle(GetCatalogsQuery request, CancellationToken cancellationToken)
    {
        var catalogsQuery = uow.Query<SectionAggregate>();
        
        var count = catalogsQuery.Count();

        if (count == 0)
            return CountResult<CatalogDto>.NoValues();
        
        var catalogs = uow.
    }
}