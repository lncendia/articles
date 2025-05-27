using MediatR;
using Articles.Application.Abstractions.Commands.Articles;
using Articles.Domain;
using Articles.Domain.Articles;
using Articles.Domain.Catalogs;
using Microsoft.EntityFrameworkCore;

namespace Articles.Application.Services.CommandHandlers.Articles;

/// <summary>
/// Обработчик команды для создания статьи.
/// </summary>
/// <param name="uow">Единица работы</param>
public class CreateArticleCommandHandler(IUnitOfWork uow) : IRequestHandler<CreateArticleCommand, Guid>
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
            // Устанавливаем текущую дату и время создания
            CreatedAt = DateTime.UtcNow,

            // Устанавливаем заголовок из запроса
            Title = request.Title,
        };

        // Устанавливаем теги для статьи
        article.SetTags(request.Tags);

        // Проверяем существование раздела с такими же тегами
        var containsSection = await uow.Query<SectionAggregate>()
            .AnyAsync(s => s.TagsHash == article.TagsHash, cancellationToken: cancellationToken);

        // Если подходящего раздела нет
        if (!containsSection)
        {
            // Создаем новый раздел
            var section = new SectionAggregate();

            // Устанавливаем теги для раздела (такие же, как у статьи)
            section.SetTags(article.Tags);

            // Добавляем раздел в контекст базы данных
            await uow.AddAsync(section, cancellationToken);
        }

        // Добавляем агрегат статьи в контекст базы данных
        await uow.AddAsync(article, cancellationToken);

        // Сохраняем изменения в базе данных
        await uow.SaveChangesAsync(cancellationToken);

        // Возвращаем идентификатор созданной статьи
        return article.Id;
    }
}