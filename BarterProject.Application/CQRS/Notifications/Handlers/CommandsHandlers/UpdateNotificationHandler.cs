using AutoMapper;
using BarterProject.Application.CQRS.Notifications.Commands.Requests;
using BarterProject.Application.CQRS.Notifications.Commands.Responses;
using BarterProject.Repository.Common;
using MediatR;

namespace BarterProject.Application.CQRS.Notifications.Handlers.CommandsHandlers;

public class UpdateNotificationHandler : IRequestHandler<UpdateNotificationCommand, UpdateNotificationResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateNotificationHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<UpdateNotificationResponse> Handle(UpdateNotificationCommand request, CancellationToken cancellationToken)
    {
        var notification = await _unitOfWork.NotificationRepository.GetByIdAsync(request.NotificationId);

        if (notification == null || notification.IsDeleted) 
        {
            return null; 
        }
        _mapper.Map(request, notification);
        await _unitOfWork.NotificationRepository.UpdateAsync(notification);
        var response = _mapper.Map<UpdateNotificationResponse>(notification);

        return response;
    }
}
