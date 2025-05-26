using MediatR;
using Articles.Application.Abstractions.Commands.Catalogs;
using Articles.Application.Abstractions.DTOs.Articles;
using Articles.Application.Abstractions.DTOs.Common;

namespace Articles.Application.Services.CommandHandlers.Catalogs;

// todo
public class SectionTagsCommandHandler : IRequestHandler<SectionTagsCommand, CountResult<ArticleDto>>
{
    public async Task<CountResult<ArticleDto>> Handle(SectionTagsCommand request, CancellationToken cancellationToken)
    {
        
        return new CountResult<ArticleDto>
        {
            List = new List<ArticleDto>(),
            TotalCount = 0
        };
    }
}