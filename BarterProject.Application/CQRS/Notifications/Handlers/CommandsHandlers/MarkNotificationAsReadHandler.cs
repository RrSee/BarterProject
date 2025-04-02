using BarterProject.Application.CQRS.Notifications.Commands.Requests;
using BarterProject.Application.CQRS.Notifications.Commands.Responses;
using BarterProject.Repository.Common;
using MediatR;

namespace BarterProject.Application.CQRS.Notifications.Handlers.CommandsHandlers;

public class MarkNotificationAsReadHandler : IRequestHandler<MarkNotificationAsReadCommand, MarkNotificationAsReadResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public MarkNotificationAsReadHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<MarkNotificationAsReadResponse> Handle(MarkNotificationAsReadCommand request, CancellationToken cancellationToken)
    {
        var notification = await _unitOfWork.NotificationRepository.GetByIdAsync(request.NotificationId);

        if (notification == null || notification.IsDeleted)
        {
            return new MarkNotificationAsReadResponse
            {
                IsSuccess = false,
                Message = "Notification not found or already deleted."
            };
        }

        notification.IsRead = true;

        await _unitOfWork.NotificationRepository.UpdateAsync(notification);

        return new MarkNotificationAsReadResponse
        {
            NotificationId = notification.Id,
            IsRead = notification.IsRead,
            IsSuccess = true,
            Message = "Notification marked as read."
        };
    }
}
