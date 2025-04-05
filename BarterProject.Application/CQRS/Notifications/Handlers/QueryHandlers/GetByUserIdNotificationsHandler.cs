using AutoMapper;
using BarterProject.Application.CQRS.Notifications.Queries.Requests;
using BarterProject.Application.CQRS.Notifications.Queries.Responses;
using BarterProject.Repository.Common;
using BarterProject.Repository.Repositories;
using MediatR;

namespace BarterProject.Application.CQRS.Notifications.Handlers.QueryHandlers;

public class GetByUserIdNotificationsHandler : IRequestHandler<GetByUserIdNotificationsQuery, List<GetByUserIdNotificationsResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetByUserIdNotificationsHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<GetByUserIdNotificationsResponse>> Handle(GetByUserIdNotificationsQuery request, CancellationToken cancellationToken)
    {
        var notifications = await _unitOfWork.NotificationRepository.GetByUserIdAsync(request.UserId);

        var response = _mapper.Map<List<GetByUserIdNotificationsResponse>>(notifications);

        return response; 
    }
}
