using AutoMapper;
using BarterProject.Application.CQRS.Items.Commands.Requests;
using BarterProject.Application.CQRS.Items.Commands.Responses;
using BarterProject.Common.GlobalResponses.Generics;
using BarterProject.Domain.Entites;
using BarterProject.Repository.Common;
using MediatR;

namespace BarterProject.Application.CQRS.Items.Handlers.CommandsHandlers;
    public class UpdateItemCommandHandler : IRequestHandler<UpdateItemCommandRequest, Result<UpdateItemCommandResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateItemCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<UpdateItemCommandResponse>> Handle(UpdateItemCommandRequest request, CancellationToken cancellationToken)
        {
            var item = _mapper.Map<Item>(request);
            var isSuccess = await _unitOfWork.ItemRepository.UpdateAsync(item);

            if (!isSuccess)
            {
                return new Result<UpdateItemCommandResponse>(new List<string> { "Item not found or update failed" });
            }

            var updatedItem = await _unitOfWork.ItemRepository.GetByIdAsync(request.Id);
            var response = _mapper.Map<UpdateItemCommandResponse>(updatedItem);

            return new Result<UpdateItemCommandResponse> { Data = response };
        }
    }
