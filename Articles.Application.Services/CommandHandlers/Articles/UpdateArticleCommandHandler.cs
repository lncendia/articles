using MediatR;
using Articles.Application.Abstractions.Commands.Articles;
using Articles.Application.Abstractions.Exceptions;
using Articles.Domain;
using Articles.Domain.Articles;
using Articles.Domain.Catalogs;
using Microsoft.EntityFrameworkCore;

namespace Articles.Application.Services.CommandHandlers.Articles;

/// <summary>
/// Обработчик команды для изменения статьи.
/// </summary>
/// <param name="uow">Единица работы</param>
public class UpdateArticleCommandHandler(IUnitOfWork uow) : IRequestHandler<UpdateArticleCommand>
{
    /// <summary>
    /// Метод обработчик команды для изменения статьи.
    /// </summary>
    /// <param name="request">Запрос на изменение статьи</param>
    /// <param name="cancellationToken">Токен отмены</param>
    public async Task Handle(UpdateArticleCommand request, CancellationToken cancellationToken)
    {
        // Получаем статью по идентификатору из запроса
        var article = await uow.Query<ArticleAggregate>()
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        // Если статья не найдена, выбрасываем исключение
        if (article == null)
            throw new ArticleNotFoundException(request.Id);

        // Обновляем заголовок статьи, если он указан в запросе
        if (request.Title != null)
            article.Title = request.Title;

        // Обновляем теги статьи, если они указаны в запросе
        if (request.Tags.Length != 0)
        {
            // Обновляем теги
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
        }

        // Устанавливаем текущую дату и время как время обновления статьи
        article.UpdatedAt = DateTime.UtcNow;
        
        // Сохраняем изменения в базе данных
        await uow.SaveChangesAsync(cancellationToken);
    }
}