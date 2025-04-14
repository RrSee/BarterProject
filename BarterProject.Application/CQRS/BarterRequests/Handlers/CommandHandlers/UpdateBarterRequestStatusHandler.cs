using AutoMapper;
using BarterProject.Application.CQRS.BarterRequests.Commands.Requests;
using BarterProject.Application.CQRS.BarterRequests.Commands.Responses;
using BarterProject.Common.Exceptions;
using BarterProject.Common.GlobalResponses.Generics;
using BarterProject.Domain.Entites;
using BarterProject.Domain.Enums;
using BarterProject.Repository.Common;
using BarterProject.Services.SignalR;
using MediatR;

namespace BarterProject.Application.CQRS.BarterRequests.Handlers.CommandHandlers;

public class UpdateBarterRequestStatusHandler(IUnitOfWork unitOfWork, IMapper mapper, ISignalRService signalRService) : IRequestHandler<UpdateBarterRequestStatusRequest, Result<UpdateBarterRequestStatusResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly ISignalRService _signalRService = signalRService;

    public async Task<Result<UpdateBarterRequestStatusResponse>> Handle(UpdateBarterRequestStatusRequest request, CancellationToken cancellationToken)
    {
        var barterRequest = await _unitOfWork.BarterRequestRepository.GetByIdAsync(request.RequestId);
        if (barterRequest == null)
        {
            throw new BadRequestException("Barter request not found.");
        }
        barterRequest.Status = request.Status;

        string message = request.Status switch
        {
            BarterStatus.Accepted => "Barter request accepted.",
            BarterStatus.Rejected => "Barter request rejected.",
            _ => "Barter request status updated."
        };

        await _signalRService.SendMessageAsync(message, barterRequest.SenderUserId);

        var response  = _mapper.Map<UpdateBarterRequestStatusResponse>(barterRequest);
        response.RequestId = request.RequestId;

        await _unitOfWork.NotificationRepository.AddAsync(new Notification
        {
            UserId = barterRequest.ReceiverUserId,
            SendedUserId = barterRequest.SenderUserId,
            Description = message,
            CreatedDate = DateTime.Now,
            CreatedBy = barterRequest.SenderUserId
        });

        return new Result<UpdateBarterRequestStatusResponse>
        {
            Data = response,
            Errors = new List<string>(),
            IsSuccess = true
        };

    }
}
