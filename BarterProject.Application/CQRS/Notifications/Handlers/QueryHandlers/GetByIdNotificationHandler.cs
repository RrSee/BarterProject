using AutoMapper;
using BarterProject.Application.CQRS.Notifications.Queries.Requests;
using BarterProject.Application.CQRS.Notifications.Queries.Responses;
using BarterProject.Repository.Common;
using BarterProject.Repository.Repositories;
using MediatR;

namespace BarterProject.Application.CQRS.Notifications.Handlers.QueryHandlers;

public class GetByIdNotificationHandler : IRequestHandler<GetByIdNotificationQuery, GetByIdNotificationResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetByIdNotificationHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<GetByIdNotificationResponse> Handle(GetByIdNotificationQuery request, CancellationToken cancellationToken)
    {
        var notification = await _unitOfWork.NotificationRepository.GetByIdAsync(request.NotificationId);

        if (notification == null)
        {
            return null; 
        }
        var response = _mapper.Map<GetByIdNotificationResponse>(notification);

        return response; 
    }
}
