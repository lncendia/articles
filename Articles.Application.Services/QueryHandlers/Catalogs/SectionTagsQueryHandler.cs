using MediatR;
using Articles.Application.Abstractions.DTOs.Articles;
using Articles.Application.Abstractions.DTOs.Common;
using Articles.Application.Abstractions.Queries.Catalogs;

namespace Articles.Application.Services.QueryHandlers.Catalogs;

// todo
public class SectionTagsQueryHandler : IRequestHandler<SectionTagsQuery, CountResult<ArticleDto>>
{
    public async Task<CountResult<ArticleDto>> Handle(SectionTagsQuery request, CancellationToken cancellationToken)
    {
        
        return new CountResult<ArticleDto>
        {
            List = new List<ArticleDto>(),
            TotalCount = 0
        };
    }
}