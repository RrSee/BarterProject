using AutoMapper;
using BarterProject.Application.CQRS.Notifications.Commands.Requests;
using BarterProject.Application.CQRS.Notifications.Commands.Responses;
using BarterProject.Domain.Entites;
using BarterProject.Repository.Common;
using MediatR;

namespace BarterProject.Application.CQRS.Notifications.Handlers.CommandsHandlers;

public class CreateNotificationHandler : IRequestHandler<CreateNotificationCommand, CreateNotificationResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateNotificationHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<CreateNotificationResponse> Handle(CreateNotificationCommand request, CancellationToken cancellationToken)
    {
        var notification = _mapper.Map<Notification>(request); 
        notification.CreatedDate = DateTime.UtcNow; 

        await _unitOfWork.NotificationRepository.AddAsync(notification);
        await _unitOfWork.CommitAsync();

        var response = _mapper.Map<CreateNotificationResponse>(notification);
        response.IsSuccess = true; 
        return response;
    }
}

