using System.Net;
using Articles.Application.Abstractions.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Articles.Start.Exceptions;

/// <summary>
/// Обработчик исключений, реализующий интерфейс IExceptionHandler.
/// </summary>
public class ExceptionHandler : IExceptionHandler
{
    /// <summary>
    /// Метод обработки исключения.
    /// </summary>
    /// <param name="context">Контекст HTTP-запроса.</param>
    /// <param name="exception">Исключение, которое необходимо обработать.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Асинхронная задача, возвращающая true, если исключение обработано.</returns>
    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception,
        CancellationToken cancellationToken)
    {
        // Переменная для хранения сообщения об ошибке, которое будет отправлено клиенту
        string? message;

        // Устанавливаем статус код по умолчанию (400 - Bad Request)
        var statusCode = HttpStatusCode.BadRequest;

        // Создаем словарь для хранения дополнительных данных, которые будут включены в ответ
        var extensions = new Dictionary<string, object?>
        {
            // Добавляем идентификатор запроса (traceId) для отслеживания ошибки
            ["traceId"] = context.TraceIdentifier
        };

        // Обработка исключения в зависимости от его типа
        switch (exception)
        {
            // Если исключение типа ArticleNotFound
            case ArticleNotFoundException ex:

                // Устанавливаем статус код 404 (Not Found)
                statusCode = HttpStatusCode.NotFound;
                
                // Формируем сообщение об ошибке с указанием текущего состояния клиента
                message = "Article not found.";

                // Добавляем состояние клиента в дополнительные данные для дальнейшего анализа
                extensions["articleId"] = ex.ArticleId;
                break;
            
            // Если исключение не относится к указанным выше типам
            default:

                // Устанавливаем статус код 500 (Internal Server Error)
                statusCode = HttpStatusCode.InternalServerError;

                // Формируем общее сообщение об ошибке для клиента
                message = "Unexpected error.";
                break;
        }

        // Устанавливаем статус код ответа в HTTP-контексте
        context.Response.StatusCode = (int)statusCode;

        // Создаем объект ProblemDetails для формирования ответа клиенту
        var problemDetails = new ProblemDetails
        {
            // Заголовок ошибки
            Title = "Error occurred.",

            // Тип ошибки (название исключения без суффикса "Exception")
            Type = exception.GetType().Name.Replace("Exception", ""),

            // Детали ошибки (сообщение, сформированное выше)
            Detail = message,

            // Путь запроса, на котором произошла ошибка
            Instance = context.Request.Path,

            // Статус код ответа
            Status = context.Response.StatusCode,

            // Дополнительные данные (например, traceId, telegramError, hint и т.д.)
            Extensions = extensions
        };

        // Отправляем ответ в формате JSON клиенту
        await context.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        // Возвращаем true, чтобы указать, что исключение успешно обработано
        return true;
    }
}