using AutoMapper;
using BarterProject.Application.CQRS.Users.Commads.Requests;
using BarterProject.Application.CQRS.Users.Commads.Responses;
using BarterProject.Common.GlobalResponses.Generics;
using BarterProject.Repository.Common;
using MediatR;

namespace BarterProject.Application.CQRS.Users.Handlers.CommandHandlers;

public class UpdateUserHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<UpdateUserRequest, Result<UpdateUserResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<UpdateUserResponse>> Handle(UpdateUserRequest request, CancellationToken cancellationToken)
    {
        var currentUser = await _unitOfWork.UserRepository.GetByIdAsync(request.Id);
        if (currentUser == null) throw new Exception($"User does not exist with id : {request.Id}"); //--------------------

        currentUser.Name = request.Name;
        currentUser.Surname = request.Surname;
        currentUser.Telephone = request.Telephone;
        currentUser.Email = request.Email;
        currentUser.Address = request.Address;
        currentUser.UpdatedDate = DateTime.Now;
        currentUser.UpdatedBy = request.Id;

        _unitOfWork.UserRepository.Update(currentUser);

        var result = _mapper.Map<UpdateUserResponse>(currentUser);

        return new Result<UpdateUserResponse>
        {
            Data = result,
            IsSuccess = true,
            Errors = []
        };
    }
}
