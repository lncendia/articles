using MediatR;
using Articles.Application.Abstractions.Commands.Articles;
using Articles.Domain;
using Articles.Domain.Articles;
using Articles.Domain.Catalogs;
using Microsoft.EntityFrameworkCore;
using SmartBot.Abstractions.Interfaces.Utils;

namespace Articles.Application.Services.CommandHandlers.Articles;

/// <summary>
/// Обработчик команды для создания статьи.
/// </summary>
/// <param name="uow">Единица работы</param>
public class CreateArticleCommandHandler(
    IUnitOfWork uow,
    IDateTimeProvider timeProvider) : IRequestHandler<CreateArticleCommand, Guid>
{
    /// <summary>
    /// Метод обработчик команды для создания статьи.
    /// </summary>
    /// <param name="request">Запрос на создание статьи</param>
    /// <param name="cancellationToken">Токен отмены</param>
    public async Task<Guid> Handle(CreateArticleCommand request, CancellationToken cancellationToken)
    {
        // Создаём агрегат статьи с новыми данными
        var article = new ArticleAggregate
        {
            CreatedAt = timeProvider.Now,
            Title = request.Title,
            Content = request.Content
        };

        //
        article.SetTags(request.Tags);

        var containsSection = await uow.Query<SectionAggregate>()
            .AnyAsync(s => s.TagsHash == article.TagsHash, cancellationToken: cancellationToken);

        if (!containsSection)
        {
            var section = new SectionAggregate();
            section.SetTags(article.Tags);
            await uow.AddAsync(section, cancellationToken);
        }
        
        // Добавляем агрегат в контекст базы данных
        await uow.AddAsync(article, cancellationToken);

        // Сохраняем изменения в базе данных
        await uow.SaveChangesAsync(cancellationToken);

        // Возвращаем идентификатор созданной статьи
        return article.Id;
    }
}