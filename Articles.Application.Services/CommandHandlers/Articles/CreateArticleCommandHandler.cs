using MediatR;
using Articles.Application.Abstractions.Commands.Articles;

namespace Articles.Application.Services.CommandHandlers.Articles;

// todo
public class CreateArticleCommandHandler : IRequestHandler<CreateArticleCommand>
{
    public async Task Handle(CreateArticleCommand request, CancellationToken cancellationToken)
    {
        
    }
}