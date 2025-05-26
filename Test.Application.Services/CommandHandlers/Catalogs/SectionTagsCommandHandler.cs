using MediatR;
using Test.Application.Abstractions.Commands.Catalogs;
using Test.Application.Abstractions.DTOs.Articles;
using Test.Application.Abstractions.DTOs.Common;

namespace Test.Application.Services.CommandHandlers.Catalogs;

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