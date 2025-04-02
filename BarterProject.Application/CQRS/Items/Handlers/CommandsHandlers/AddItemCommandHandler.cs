using AutoMapper;
using BarterProject.Application.CQRS.Items.Commands.Requests;
using BarterProject.Application.CQRS.Items.Commands.Responses;
using BarterProject.Common.GlobalResponses.Generics;
using BarterProject.Domain.Entites;
using BarterProject.Repository.Common;
using MediatR;

namespace BarterProject.Application.CQRS.Items.Handlers.CommandsHandlers;

public class AddItemCommandHandler : IRequestHandler<AddItemCommandRequest, Result<AddItemCommandResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public AddItemCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<AddItemCommandResponse>> Handle(AddItemCommandRequest request, CancellationToken cancellationToken)
    {
        var item = new Item
        {
            Name = request.Name,
            Description = request.Description,
            ImagePath = request.ImagePath,
            UserId = request.UserId,
            CreatedBy = request.UserId,
            CreatedDate = DateTime.Now
        };

        await _unitOfWork.ItemRepository.AddAsync(item);
        await _unitOfWork.CommitAsync();

        var response = _mapper.Map<AddItemCommandResponse>(item);
        return new Result<AddItemCommandResponse> { Data = response };
    }
}