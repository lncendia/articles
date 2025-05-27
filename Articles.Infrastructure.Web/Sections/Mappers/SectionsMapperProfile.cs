using Articles.Application.Abstractions.Queries.Sections;
using Articles.Infrastructure.Web.Sections.InputModels;

namespace Articles.Infrastructure.Web.Sections.Mappers;

/// <summary>
/// Класс для маппинга входных моделей в команды
/// </summary>
public class SectionsMapperProfile : AutoMapper.Profile
{
    /// <summary>
    /// Маппинг входных моделей в команды
    /// </summary>
    public SectionsMapperProfile()
    {
        // Карта для CreateArticleRequest в CreateArticleCommand
        CreateMap<GetArticlesRequest, GetSectionArticlesQuery>().ForMember(dest => dest.SectionId,
            opt => opt.MapFrom((_, _, _, context) => context.Items["Id"]));
        
        // Карта для GetSectionsRequest в GetSectionsQuery
        CreateMap<GetSectionsRequest, GetSectionsQuery>();
    }
}