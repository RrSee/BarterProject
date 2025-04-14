using AutoMapper;
using BarterProject.Application.CQRS.Users.Commads.Requests;
using BarterProject.Application.CQRS.Users.Commads.Responses;
using BarterProject.Common.GlobalResponses.Generics;
using BarterProject.Domain.Entites;
using BarterProject.Repository.Common;
using BarterProject.Services.SignalR;
using MediatR;

namespace BarterProject.Application.CQRS.Users.Handlers.CommandHandlers;

public class AddFavoriteItemHandler(IUnitOfWork unitOfWork, IMapper mapper, ISignalRService signalRService) : IRequestHandler<AddFavoriteItemRequest, Result<AddFavoriteItemResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly ISignalRService _signalRService = signalRService;

    public async Task<Result<AddFavoriteItemResponse>> Handle(AddFavoriteItemRequest request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserRepository.GetByIdAsync(request.UserId);

        var item = await _unitOfWork.ItemRepository.GetByIdAsync(request.ItemId);
        var userOfItem = await _unitOfWork.UserRepository.GetByIdAsync(item.UserId);

        var favoriteItem = _mapper.Map<UsersFavoriteItems>(request);
        favoriteItem.CreatedDate = DateTime.Now;

        favoriteItem.CreatedBy = request.UserId;

        await _unitOfWork.UserRepository.AddFavoriteItemAsync(favoriteItem);

        var result = _mapper.Map<AddFavoriteItemResponse>(favoriteItem);
        // SignalR notification
        await _signalRService.SendMessageAsync($"You have a new barter request from user {request.UserId} Telephone: {user.Telephone}", userOfItem.Id);

        await _unitOfWork.NotificationRepository.AddAsync(new Notification
        {
            UserId = userOfItem.Id,
            SendedUserId = request.UserId,
            Description = $"You have a new barter request from user {request.UserId} Telephone: {user.Telephone}",
            CreatedDate = DateTime.Now,
            CreatedBy = request.UserId
        });


        return new Result<AddFavoriteItemResponse>
        {
            Data = result,
            IsSuccess = true,
            Errors = []
        };
    }
}
