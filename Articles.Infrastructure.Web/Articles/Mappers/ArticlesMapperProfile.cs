using Articles.Infrastructure.Web.Articles.InputModels;
using Articles.Application.Abstractions.Commands.Articles;

namespace Articles.Infrastructure.Web.Articles.Mappers;

/// <summary>
/// Класс для маппинга входных моделей в команды
/// </summary>
public class ArticlesMapperProfile : AutoMapper.Profile
{
    /// <summary>
    /// Маппинг входных моделей в команды
    /// </summary>
    public ArticlesMapperProfile()
    {
        // Карта для CreateArticleRequest в CreateArticleCommand
        CreateMap<CreateArticleRequest, CreateArticleCommand>();

        // Карта для UpdateArticleRequest в UpdateArticleCommand
        CreateMap<UpdateArticleRequest, UpdateArticleCommand>().ForMember(dest => dest.Id,
            opt => opt.MapFrom((_, _, _, context) => context.Items["Id"]));
    }
}