using AutoMapper;
using BarterProject.Application.CQRS.Users.Commads.Requests;
using BarterProject.Application.CQRS.Users.Commads.Responses;
using BarterProject.Common.GlobalResponses.Generics;
using BarterProject.Domain.Entites;
using BarterProject.Repository.Common;
using MediatR;

namespace BarterProject.Application.CQRS.Users.Handlers.CommandHandlers;

public class AddFavoriteItemHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<AddFavoriteItemRequest, Result<AddFavoriteItemResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<AddFavoriteItemResponse>> Handle(AddFavoriteItemRequest request, CancellationToken cancellationToken)
    {
        var favoriteItem = _mapper.Map<UsersFavoriteItems>(request);
        favoriteItem.CreatedDate = DateTime.Now;

        favoriteItem.CreatedBy = request.UserId;

        await _unitOfWork.UserRepository.AddFavoriteItemAsync(favoriteItem);

        var result = _mapper.Map<AddFavoriteItemResponse>(favoriteItem);

        return new Result<AddFavoriteItemResponse>
        {
            Data = result,
            IsSuccess = true,
            Errors = []
        };
    }
}
