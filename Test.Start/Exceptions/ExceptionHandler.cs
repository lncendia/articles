using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using UserAgent.Application.Abstractions.Exceptions;
using UserAgent.Domain.Instances.Exceptions;
using UserAgent.Domain.Workspaces.Exceptions;

namespace UserAgent.Start.Exceptions;

/// <summary>
/// Обработчик исключений, реализующий интерфейс IExceptionHandler.
/// </summary>
public class ExceptionHandler(IStringLocalizer<ExceptionHandler> localizer) : IExceptionHandler
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
            // Если исключение типа InstanceNotFoundException
            case InstanceNotFoundException ex:

                // Формируем сообщение об ошибке с указанием текущего состояния клиента
                message = localizer["InstanceNotFound"];

                // Добавляем состояние клиента в дополнительные данные для дальнейшего анализа
                extensions["instanceId"] = ex.InstanceId;
                break;

            // Если исключение типа NoServersAvailableException
            case NoServersAvailableException ex:

                // Формируем сообщение об ошибке для клиента
                message = localizer["NoServersAvailable"];

                // Добавляем сообщение об ошибке ServerType в дополнительные данные
                extensions["serverType"] = ex.ServerType;
                break;

            // Если исключение типа UserNotFoundException
            case UserNotFoundException ex:

                // Формируем сообщение об ошибке для клиента
                message = localizer["UserNotFound"];

                // Добавляем сообщение об ошибке UserId в дополнительные данные
                extensions["userId"] = ex.UserId;
                break;

            // Если исключение типа WorkspaceNotFoundException
            case WorkspaceNotFoundException ex:

                // Формируем сообщение об ошибке для клиента
                message = localizer["WorkspaceNotFound"];

                // Добавляем сообщение об ошибке WorkspaceId в дополнительные данные
                extensions["workspaceId"] = ex.WorkspaceId;
                break;

            // Если исключение типа ServerOverloadedException
            case ServerOverloadedException ex:

                // Формируем сообщение об ошибке для клиента
                message = localizer["ServerOverloaded"];

                // Добавляем сообщение об ошибке ServerId в дополнительные данные
                extensions["serverId"] = ex.ServerId;
                break;

            // Если исключение типа DuplicateInvitationIdException
            case DuplicateInvitationIdException ex:

                // Формируем сообщение об ошибке для клиента
                message = localizer["DuplicateInvitationId"];

                // Добавляем сообщение об ошибке InvitationId в дополнительные данные
                extensions["invitationId"] = ex.InvitationId;

                break;

            // Если исключение типа DuplicateRoleNameException
            case DuplicateRoleNameException ex:

                // Формируем сообщение об ошибке для клиента
                message = localizer["DuplicateRoleName"];

                // Добавляем сообщение об ошибке RoleName в дополнительные данные
                extensions["roleName"] = ex.RoleName;

                break;

            // Если исключение типа DuplicateGroupNameException
            case DuplicateGroupNameException ex:

                // Формируем сообщение об ошибке для клиента
                message = localizer["DuplicateGroupName"];

                // Добавляем сообщение об ошибке Name в дополнительные данные
                extensions["name"] = ex.Name;

                break;
            
            // Если исключение типа UserAlreadyInWorkspaceException
            case UserAlreadyInWorkspaceException ex:

                // Формируем сообщение об ошибке для клиента
                message = localizer["UserAlreadyInWorkspace"];

                // Добавляем сообщение об ошибке RoleName в дополнительные данные
                extensions["userId"] = ex.UserId;

                break;
            
            // Если исключение типа WorkspaceRoleCannotBeDeletedException
            case WorkspaceRoleCannotBeDeletedException ex:

                // Формируем сообщение об ошибке для клиента
                message = localizer["WorkspaceRoleCannotBeDeleted"];

                // Добавляем сообщение об ошибке RoleName в дополнительные данные
                extensions["roleName"] = ex.RoleName;

                break;
            
            // Если исключение типа InvalidPermissionTypeException
            case InvalidPermissionTypeException ex:

                // Формируем сообщение об ошибке для клиента
                message = localizer["InvalidPermissionType"];

                // Добавляем сообщение об ошибке PermissionKey в дополнительные данные
                extensions["permissionKey"] = ex.PermissionKey;

                // Добавляем сообщение об ошибке ExpectedType в дополнительные данные
                extensions["expectedType"] = ex.ExpectedType;

                // Добавляем сообщение об ошибке ActualType в дополнительные данные
                extensions["actualType"] = ex.ActualType;

                break;

            // Если исключение типа WorkspaceGroupNotFoundException
            case WorkspaceGroupNotFoundException ex:

                // Формируем сообщение об ошибке для клиента
                message = localizer["WorkspaceGroupNotFound"];

                // Добавляем сообщение об ошибке Name в дополнительные данные
                extensions["name"] = ex.Name;

                break;

            // Если исключение типа WorkspacePermissionDeniedException
            case WorkspacePermissionDeniedException ex:

                // Формируем сообщение об ошибке для клиента
                message = localizer["WorkspacePermissionDenied"];

                // Добавляем сообщение об ошибке RoleName в дополнительные данные
                extensions["roleName"] = ex.RoleName;

                // Добавляем сообщение об ошибке RequiredPermission в дополнительные данные
                extensions["requiredPermission"] = ex.RequiredPermission;

                break;

            // Если исключение типа WorkspaceRoleNotFoundException
            case WorkspaceRoleNotFoundException ex:

                // Формируем сообщение об ошибке для клиента
                message = localizer["WorkspaceRoleNotFound"];

                // Добавляем сообщение об ошибке RoleName в дополнительные данные
                extensions["roleName"] = ex.RoleName;

                break;

            // Если исключение типа WorkspaceUserNotFoundException
            case WorkspaceUserNotFoundException ex:

                // Формируем сообщение об ошибке для клиента
                message = localizer["WorkspaceUserNotFound"];

                // Добавляем сообщение об ошибке RoleName в дополнительные данные
                extensions["userId"] = ex.UserId;

                break;

            // Если исключение типа CannotSpecifyBothAccessException
            case CannotSpecifyBothAccessException:

                // Формируем сообщение об ошибке для клиента
                message = localizer["CannotSpecifyBothAccess"];

                break;

            // Если исключение типа InvalidValuesInAccessibleListException
            case InvalidValuesInAccessibleListException:

                // Формируем сообщение об ошибке для клиента
                message = localizer["InvalidValuesInAccessibleList"];

                break;
            
            // Если исключение не относится к указанным выше типам
            default:

                // Устанавливаем статус код 500 (Internal Server Error)
                statusCode = HttpStatusCode.InternalServerError;

                // Формируем общее сообщение об ошибке для клиента
                message = localizer["default"];
                break;
        }

        // Устанавливаем статус код ответа в HTTP-контексте
        context.Response.StatusCode = (int)statusCode;

        // Создаем объект ProblemDetails для формирования ответа клиенту
        var problemDetails = new ProblemDetails
        {
            // Заголовок ошибки
            Title = localizer["title"],

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