using Articles.Infrastructure.Web.Catalogs.InputModels;
using Articles.Application.Abstractions.Queries.Catalogs;

namespace Articles.Infrastructure.Web.Catalogs.Mappers;

public class CatalogsMapperProfile : AutoMapper.Profile
{
    /// <summary>
    /// Маппинг входных моделей в команды
    /// </summary>
    public CatalogsMapperProfile()
    {
        // Карта для CreateArticleRequest в CreateArticleCommand
        CreateMap<ArticlesRequest, ArticlesQuery>();
    }
}