using AutoMapper;
using BarterProject.Application.CQRS.Notifications.Queries.Requests;
using BarterProject.Application.CQRS.Notifications.Queries.Responses;
using BarterProject.Repository.Common;
using BarterProject.Repository.Repositories;
using MediatR;

namespace BarterProject.Application.CQRS.Notifications.Handlers.QueryHandlers;

public class GetNotificationByIdHandler : IRequestHandler<GetNotificationByIdQuery, GetNotificationByIdResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetNotificationByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<GetNotificationByIdResponse> Handle(GetNotificationByIdQuery request, CancellationToken cancellationToken)
    {
        var notification = await _unitOfWork.NotificationRepository.GetByIdAsync(request.NotificationId);

        if (notification == null)
        {
            return null; 
        }
        var response = _mapper.Map<GetNotificationByIdResponse>(notification);

        return response; 
    }
}
