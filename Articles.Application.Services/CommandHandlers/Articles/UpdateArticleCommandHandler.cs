using MediatR;
using Articles.Application.Abstractions.Commands.Articles;
using Articles.Domain;
using Articles.Domain.Articles;
using Microsoft.EntityFrameworkCore;

namespace Articles.Application.Services.CommandHandlers.Articles;

/// <summary>
/// Обработчик команды для изменения статьи.
/// </summary>
/// <param name="uow">Единица работы</param>
public class UpdateArticleCommandHandler(
    IUnitOfWork uow) : IRequestHandler<UpdateArticleCommand>
{
    /// <summary>
    /// Метод обработчик команды для изменения статьи.
    /// </summary>
    /// <param name="request">Запрос на изменение статьи</param>
    /// <param name="cancellationToken">Токен отмены</param>
    public async Task Handle(UpdateArticleCommand request, CancellationToken cancellationToken)
    {
        var article = await uow.Query<ArticleAggregate>()
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        
        if (article == null)
            throw new NullReferenceException("Article not found");
        
        article.Edit(request.Title, request.Content, request.Tags);
        
        uow.Update(article);
        
        await  uow.SaveChangesAsync(cancellationToken);
    }
}