using AutoMapper;
using BarterProject.Application.CQRS.Notifications.Queries.Requests;
using BarterProject.Application.CQRS.Notifications.Queries.Responses;
using BarterProject.Repository.Common;
using BarterProject.Repository.Repositories;
using MediatR;

namespace BarterProject.Application.CQRS.Notifications.Handlers.QueryHandlers;

public class GetAllNotificationsHandler : IRequestHandler<GetAllNotificationsQuery, List<GetAllNotificationsResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllNotificationsHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<GetAllNotificationsResponse>> Handle(GetAllNotificationsQuery request, CancellationToken cancellationToken)
    {
       
        var notifications = await _unitOfWork.NotificationRepository.GetAllAsync();

        
        var response = _mapper.Map<List<GetAllNotificationsResponse>>(notifications);

        return response;
    }
}
