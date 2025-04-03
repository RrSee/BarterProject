using BarterProject.Application.CQRS.Notifications.Commands.Requests;
using BarterProject.Application.CQRS.Notifications.Commands.Responses;
using BarterProject.Common.GlobalResponses.Generics;
using BarterProject.Repository.Common;
using BarterProject.Repository.Repositories;
using FluentValidation;
using MediatR;

namespace BarterProject.Application.CQRS.Notifications.Handlers.CommandsHandlers;

public class DeleteNotificationHandler : IRequestHandler<DeleteNotificationCommand, Result<DeleteNotificationResponse>>
{
    private readonly IUnitOfWork _unitOfWork;


    public DeleteNotificationHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;

    }

    public async Task<Result<DeleteNotificationResponse>> Handle(DeleteNotificationCommand request, CancellationToken cancellationToken)
    {


        var notification = await _unitOfWork.NotificationRepository.GetByIdAsync(request.NotificationId);

        if (notification == null || notification.IsDeleted)
        {
            return new Result<DeleteNotificationResponse>(new List<string> { "Notification not found or already deleted." });
        }

        notification.IsDeleted = true;
        notification.DeletedBy = request.UserId;
        notification.DeletedDate = DateTime.Now;

        await _unitOfWork.NotificationRepository.UpdateAsync(notification);
        await _unitOfWork.CommitAsync();

        var response = new DeleteNotificationResponse
        {
            IsSuccess = true,
            Message = "Notification successfully deleted."
        };

        return new Result<DeleteNotificationResponse> { Data = response };
    }
}
