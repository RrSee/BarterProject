using AutoMapper;
using BarterProject.Application.CQRS.BarterRequests.Commands.Requests;
using BarterProject.Application.CQRS.BarterRequests.Commands.Responses;
using BarterProject.Application.CQRS.Notifications.Commands.Requests;
using BarterProject.Application.CQRS.Notifications.Commands.Responses;
using BarterProject.Application.CQRS.Notifications.Queries.Responses;
using BarterProject.Domain.Entites;

namespace BarterProject.Application.AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateBarterRequestRequest, BarterRequest>();
        CreateMap<BarterRequest, CreateBarterRequestResponse>();
        //Notification
        CreateMap<Notification, GetAllNotificationsResponse>();
        CreateMap<Notification, GetNotificationByIdResponse>();
        CreateMap<Notification, GetNotificationsByUserResponse>();
        CreateMap<Notification, GetUnreadNotificationsResponse>();
        CreateMap<CreateNotificationCommand, Notification>();  
        CreateMap<Notification, CreateNotificationResponse>(); 
        CreateMap<UpdateNotificationCommand, Notification>(); 
        CreateMap<Notification, UpdateNotificationResponse>();
    }
}
