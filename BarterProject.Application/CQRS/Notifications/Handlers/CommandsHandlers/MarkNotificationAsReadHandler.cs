using BarterProject.Application.CQRS.Notifications.Commands.Requests;
using BarterProject.Application.CQRS.Notifications.Commands.Responses;
using BarterProject.Common.Exceptions;
using BarterProject.Common.GlobalResponses;
using BarterProject.Common.GlobalResponses.Generics;
using BarterProject.Repository.Common;
using FluentValidation;
using MediatR;

namespace BarterProject.Application.CQRS.Notifications.Handlers.CommandsHandlers;
public class MarkNotificationAsReadHandler : IRequestHandler<MarkNotificationAsReadCommand, Result<MarkNotificationAsReadResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<MarkNotificationAsReadCommand> _validator;

    public MarkNotificationAsReadHandler(IUnitOfWork unitOfWork, IValidator<MarkNotificationAsReadCommand> validator)
    {
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<Result<MarkNotificationAsReadResponse>> Handle(MarkNotificationAsReadCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return new Result<MarkNotificationAsReadResponse>(validationResult.Errors.Select(e => e.ErrorMessage).ToList());
        }
        var notification = await _unitOfWork.NotificationRepository.GetByIdAsync(request.NotificationId);

        if (notification == null || notification.IsDeleted)
        {
            //return new Result<MarkNotificationAsReadResponse> { Errors = new List<string> { "Notification not found or already deleted." } };
            throw new BadRequestException("Notification not found or already deleted.");
        }
        notification.IsRead = true;
        await _unitOfWork.NotificationRepository.UpdateAsync(notification);
        await _unitOfWork.CommitAsync();
        var response = new MarkNotificationAsReadResponse
        {
            NotificationId = notification.Id,
            IsRead = notification.IsRead
        };

        return new Result<MarkNotificationAsReadResponse> { Data = response };
    }
}

