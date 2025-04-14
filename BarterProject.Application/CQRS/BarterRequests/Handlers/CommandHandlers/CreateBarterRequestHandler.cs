using AutoMapper;
using BarterProject.Application.CQRS.BarterRequests.Commands.Requests;
using BarterProject.Application.CQRS.BarterRequests.Commands.Responses;
using BarterProject.Common.Exceptions;
using BarterProject.Common.GlobalResponses.Generics;
using BarterProject.Domain.Entites;
using BarterProject.Repository.Common;
using BarterProject.Services.SignalR;
using FluentValidation;
using MediatR;

namespace BarterProject.Application.CQRS.BarterRequests.Handlers.CommandHandlers;

public class CreateBarterRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<CreateBarterRequestRequest> validator, ISignalRService signalRService) : IRequestHandler<CreateBarterRequestRequest, Result<CreateBarterRequestResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly IValidator<CreateBarterRequestRequest> _validator = validator;
    private readonly ISignalRService _signalRService = signalRService;

    public async Task<Result<CreateBarterRequestResponse>> Handle(CreateBarterRequestRequest request, CancellationToken cancellationToken)
    {
        //---
        var user = await _unitOfWork.UserRepository.GetByIdAsync(request.SenderUserId);

        var item = await _unitOfWork.ItemRepository.GetByIdAsync(request.SenderItemId);
        if (item.UserId == request.SenderUserId)
        {
            throw new BadRequestException("You cannot send a barter request for your own item.");
        }

        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return new Result<CreateBarterRequestResponse>(validationResult.Errors.Select(e => e.ErrorMessage).ToList());
        }
        var newBarterRequest = _mapper.Map<BarterRequest>(request);

        await _unitOfWork.BarterRequestRepository.AddAsync(newBarterRequest);
        await _unitOfWork.CommitAsync();

        var response = _mapper.Map<CreateBarterRequestResponse>(newBarterRequest);

        // SignalR notification
        await _signalRService.SendMessageAsync($"You have a new barter request from user {newBarterRequest.SenderUserId} Telephone: {user.Telephone} RequestId: {newBarterRequest.Id}", request.ReceiverUserId);

        await _unitOfWork.NotificationRepository.AddAsync(new Notification
        {
            UserId = request.ReceiverUserId,
            SendedUserId = request.SenderUserId,
            Description = $"You have a new barter request from user {newBarterRequest.SenderUserId} Telephone: {user.Telephone} RequestId: {newBarterRequest.Id}",
            CreatedDate = DateTime.Now,
            CreatedBy = request.SenderUserId
        });

        return new Result<CreateBarterRequestResponse>
        {
            Data = response,
            Errors = new List<string>(),
            IsSuccess = true
        };
    }
}
