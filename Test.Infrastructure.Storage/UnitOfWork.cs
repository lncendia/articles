using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Test.Domain.Abstractions.Interfaces;

namespace Test.Infrastructure.Storage;

public class UnitOfWork : IUnitOfWork
{
    /// <summary>
    /// Контекст базы данных, используемый для взаимодействия с базой данных.
    /// </summary>
    private readonly DbContext _context;
    
    /// <summary>
    /// Медиатор, используемый для отправки сообщений и событий доменной модели.
    /// </summary>
    private readonly IMediator _mediator;

    /// <summary>
    /// Логгер.
    /// </summary>
    private readonly ILogger<UnitOfWork> _logger;
    
    /// <summary>
    /// Инициализирует новый экземпляр класса UnitOfWork.
    /// </summary>
    /// <param name="context">Контекст базы данных.</param>
    /// <param name="mediator">Медиатор.</param>
    /// <param name="logger">Логгер.</param>
    public UnitOfWork(DbContext context, IMediator mediator, ILogger<UnitOfWork> logger)
    {
        // Устанавливаем контекст
        _context = context;

        // Устанавливаем медиатор
        _mediator = mediator;

        // Устанавливаем логгер
        _logger = logger;
    }
    
// todo
}