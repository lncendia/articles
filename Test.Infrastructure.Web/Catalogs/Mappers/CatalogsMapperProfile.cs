using Test.Application.Abstractions.Commands.Catalogs;
using Test.Infrastructure.Web.Catalogs.InputModels;

namespace Test.Infrastructure.Web.Catalogs.Mappers;

public class CatalogsMapperProfile : AutoMapper.Profile
{
    /// <summary>
    /// Маппинг входных моделей в команды
    /// </summary>
    public CatalogsMapperProfile()
    {
        // Карта для CreateArticleRequest в CreateArticleCommand
        CreateMap<SectionTagsRequest, SectionTagsCommand>();
    }
}