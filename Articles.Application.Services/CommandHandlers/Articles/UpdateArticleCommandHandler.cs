using MediatR;
using Articles.Application.Abstractions.Commands.Articles;
using Articles.Domain;
using Articles.Domain.Articles;
using Articles.Domain.Catalogs;
using Microsoft.EntityFrameworkCore;
using SmartBot.Abstractions.Interfaces.Utils;
using Articles.Abstractions.Interfaces.Utils.Exceptions;

namespace Articles.Application.Services.CommandHandlers.Articles;

/// <summary>
/// Обработчик команды для изменения статьи.
/// </summary>
/// <param name="uow">Единица работы</param>
public class UpdateArticleCommandHandler(
    IUnitOfWork uow,
    IDateTimeProvider timeProvider) : IRequestHandler<UpdateArticleCommand>
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
            throw new ArticleNotFoundException(request.Id);

        article.UpdatedAt = timeProvider.Now;
        
        if (request.Title != null)
            article.Title = request.Title;
        
        if (request.Content != null)
            article.Content = request.Content;

        if (request.Tags != null)
        {
            article.SetTags(request.Tags);

            var containsSection = await uow.Query<SectionAggregate>()
                .AnyAsync(s => s.TagsHash == article.TagsHash, cancellationToken: cancellationToken);

            if (!containsSection)
            {
                var section = new SectionAggregate();
                section.SetTags(article.Tags);
                await uow.AddAsync(section, cancellationToken);
            }
        }
        
        uow.Update(article);
        
        await  uow.SaveChangesAsync(cancellationToken);
    }
}