using AutoMapper;
using BarterProject.Application.CQRS.Notifications.Commands.Requests;
using BarterProject.Application.CQRS.Notifications.Commands.Responses;
using BarterProject.Common.GlobalResponses.Generics;
using BarterProject.Repository.Common;
using FluentValidation;
using MediatR;

namespace BarterProject.Application.CQRS.Notifications.Handlers.CommandsHandlers;

public class UpdateNotificationHandler : IRequestHandler<UpdateNotificationCommand, Result<UpdateNotificationResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IValidator<UpdateNotificationCommand> _validator;

    public UpdateNotificationHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<UpdateNotificationCommand> validator)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<Result<UpdateNotificationResponse>> Handle(UpdateNotificationCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return new Result<UpdateNotificationResponse>(validationResult.Errors.Select(e => e.ErrorMessage).ToList());
        }

        var notification = await _unitOfWork.NotificationRepository.GetByIdAsync(request.NotificationId);
        if (notification == null || notification.IsDeleted)
        {
            return new Result<UpdateNotificationResponse> { Errors = new List<string> { "Notification not found or has been deleted." } };
        }

        _mapper.Map(request, notification);
        await _unitOfWork.NotificationRepository.UpdateAsync(notification);
        await _unitOfWork.CommitAsync();
        var response = _mapper.Map<UpdateNotificationResponse>(notification);
        response.IsSuccess = true;

        return new Result<UpdateNotificationResponse> { Data = response };
    }
}
