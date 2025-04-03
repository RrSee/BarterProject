using AutoMapper;
using BarterProject.Application.CQRS.Notifications.Commands.Requests;
using BarterProject.Application.CQRS.Notifications.Commands.Responses;
using BarterProject.Common.GlobalResponses.Generics;
using BarterProject.Domain.Entites;
using BarterProject.Repository.Common;
using FluentValidation;
using MediatR;

namespace BarterProject.Application.CQRS.Notifications.Handlers.CommandsHandlers;

public class CreateNotificationHandler : IRequestHandler<CreateNotificationCommand, Result<CreateNotificationResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateNotificationCommand> _validator;

    public CreateNotificationHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<CreateNotificationCommand> validator)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<Result<CreateNotificationResponse>> Handle(CreateNotificationCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return new Result<CreateNotificationResponse>(validationResult.Errors.Select(e => e.ErrorMessage).ToList());
        }
        var notification = _mapper.Map<Notification>(request);
        notification.CreatedDate = DateTime.UtcNow;

        await _unitOfWork.NotificationRepository.AddAsync(notification);
        await _unitOfWork.CommitAsync();

        var response = _mapper.Map<CreateNotificationResponse>(notification);
        response.IsSuccess = true;

        return new Result<CreateNotificationResponse> { Data = response };
    }
}

