using MediatR;
using Test.Application.Abstractions.Commands.Articles;

namespace Test.Application.Services.CommandHandlers.Articles;

// todo
public class UpdateArticleCommandHandler : IRequestHandler<UpdateArticleCommand>
{
    public async Task Handle(UpdateArticleCommand request, CancellationToken cancellationToken)
    {
        
    }
}