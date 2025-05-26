using MediatR;
using Articles.Application.Abstractions.Commands.Articles;
using Articles.Domain;
using Articles.Domain.Articles;

namespace Articles.Application.Services.CommandHandlers.Articles;

/// <summary>
/// Обработчик команды для создания статьи.
/// </summary>
/// <param name="uow">Единица работы</param>
public class CreateArticleCommandHandler(
    IUnitOfWork uow) : IRequestHandler<CreateArticleCommand, Guid>
{
    /// <summary>
    /// Метод обработчик команды для создания статьи.
    /// </summary>
    /// <param name="request">Запрос на создание статьи</param>
    /// <param name="cancellationToken">Токен отмены</param>
    public async Task<Guid> Handle(CreateArticleCommand request, CancellationToken cancellationToken)
    {
        // Создаём агрегат статьи с новыми данными
        var articleAggregate = new ArticleAggregate{
            Id = Guid.NewGuid(),
            Created = DateTime.Now,
            Title = request.Title,
            Content = request.Content,
            Tags = request.Tags!.ToHashSet()
        };
        
        // Добавляем агрегат в контекст базы данных
        await uow.AddAsync(articleAggregate, cancellationToken);
        
        // Сохраняем изменения в базе данных
        await uow.SaveChangesAsync(cancellationToken);

        // Возвращаем идентификатор созданной статьи
        return articleAggregate.Id;
    }
}