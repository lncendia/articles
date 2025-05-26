using MediatR;
using Articles.Application.Abstractions.Commands.Articles;

namespace Articles.Application.Services.CommandHandlers.Articles;

// todo
public class UpdateArticleCommandHandler : IRequestHandler<UpdateArticleCommand>
{
    public async Task Handle(UpdateArticleCommand request, CancellationToken cancellationToken)
    {
        
    }
}