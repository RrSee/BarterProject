using AutoMapper;
using BarterProject.Application.CQRS.BarterRequests.Commands.Requests;
using BarterProject.Application.CQRS.BarterRequests.Commands.Responses;
using BarterProject.Domain.Entites;

namespace BarterProject.Application.AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateBarterRequestRequest, BarterRequest>();
        CreateMap<BarterRequest, CreateBarterRequestResponse>();
    }
}
