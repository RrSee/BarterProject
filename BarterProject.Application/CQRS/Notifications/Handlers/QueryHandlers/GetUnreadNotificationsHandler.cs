using AutoMapper;
using BarterProject.Application.CQRS.Notifications.Queries.Requests;
using BarterProject.Application.CQRS.Notifications.Queries.Responses;
using BarterProject.Repository.Common;
using BarterProject.Repository.Repositories;
using MediatR;

namespace BarterProject.Application.CQRS.Notifications.Handlers.QueryHandlers;

public class GetUnreadNotificationsHandler : IRequestHandler<GetUnreadNotificationsQuery, List<GetUnreadNotificationsResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetUnreadNotificationsHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<GetUnreadNotificationsResponse>> Handle(GetUnreadNotificationsQuery request, CancellationToken cancellationToken)
    {
        var unreadNotifications = await _unitOfWork.NotificationRepository.GetUnreadNotificationsAsync(request.UserId);

        var response = _mapper.Map<List<GetUnreadNotificationsResponse>>(unreadNotifications);

        return response; 
    }
}
