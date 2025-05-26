using Test.Application.Abstractions.Commands.Articles;
using Test.Infrastructure.Web.Articles.InputModels;

namespace Test.Infrastructure.Web.Articles.Mappers;

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
        CreateMap<UpdateArticleRequest, UpdateArticleCommand>();
    }
}