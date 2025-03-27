using BarterProject.Application.CQRS.Notifications.Commands.Requests;
using BarterProject.Application.CQRS.Notifications.Commands.Responses;
using BarterProject.Repository.Common;
using BarterProject.Repository.Repositories;
using MediatR;

namespace BarterProject.Application.CQRS.Notifications.Handlers.CommandsHandlers;

public class DeleteNotificationHandler : IRequestHandler<DeleteNotificationCommand, DeleteNotificationResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteNotificationHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<DeleteNotificationResponse> Handle(DeleteNotificationCommand request, CancellationToken cancellationToken)
    {
        var notification = await _unitOfWork.NotificationRepository.GetByIdAsync(request.NotificationId);

        if (notification == null || notification.IsDeleted) 
        {
            return new DeleteNotificationResponse
            {
                IsSuccess = false,
                Message = "Notification not found or already deleted."
            };
        }

        notification.IsDeleted = true;
        notification.DeletedBy = request.UserId;
        notification.DeletedDate = DateTime.Now;

        await _unitOfWork.NotificationRepository.UpdateAsync(notification);

        return new DeleteNotificationResponse
        {
            IsSuccess = true,
            Message = "Notification successfully deleted."
        };
    }
}



