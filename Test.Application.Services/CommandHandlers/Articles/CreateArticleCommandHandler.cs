using MediatR;
using Test.Application.Abstractions.Commands.Articles;

namespace Test.Application.Services.CommandHandlers.Articles;

// todo
public class CreateArticleCommandHandler : IRequestHandler<CreateArticleCommand>
{
    public async Task Handle(CreateArticleCommand request, CancellationToken cancellationToken)
    {
        
    }
}