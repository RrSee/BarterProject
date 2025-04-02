using AutoMapper;
using BarterProject.Application.CQRS.BarterRequests.Commands.Requests;
using BarterProject.Application.CQRS.BarterRequests.Commands.Responses;
using BarterProject.Application.CQRS.Comments.Commands.Requests;
using BarterProject.Application.CQRS.Comments.Commands.Responses;
using BarterProject.Application.CQRS.Comments.Queries.Responses;
using BarterProject.Application.CQRS.Items.Commands.Requests;
using BarterProject.Application.CQRS.Items.Commands.Responses;
using BarterProject.Application.CQRS.Items.Queries.Responses;
using BarterProject.Application.CQRS.Notifications.Commands.Requests;
using BarterProject.Application.CQRS.Notifications.Commands.Responses;
using BarterProject.Application.CQRS.Notifications.Queries.Responses;
using BarterProject.Application.CQRS.Users.Commads.Requests;
using BarterProject.Application.CQRS.Users.Commads.Responses;
using BarterProject.Application.CQRS.Users.Queries.Responses;
using BarterProject.Domain.Entites;

namespace BarterProject.Application.AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        //BarterRequest
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
        //Comment
        CreateMap<CreateCommentRequest, Comment>();
        CreateMap<Comment, CreateCommentResponse>();
        CreateMap<Comment, GetAllCommentResponse>();
        CreateMap<Comment, GetByItemIdCommentResponse>();
        //Item
        CreateMap<AddItemCommandRequest, Item>()
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.Now)) 
            .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => false));
        CreateMap<Item, AddItemCommandResponse>();
        CreateMap<UpdateItemCommandRequest, Item>()
            .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(src => DateTime.Now)); 
        CreateMap<Item, UpdateItemCommandResponse>();
        CreateMap<DeleteItemCommandRequest, Item>();
        CreateMap<Item, GetAllItemsQueryResponse>();
        CreateMap<Item, GetByIdItemQueryResponse>();
        CreateMap<Item, GetByUserIdItemsQueryResponse>();
        //User
        CreateMap<RegisterUserRequest,User>();
        CreateMap<User, RegisterUserResponse>();
        CreateMap<User,UpdateUserResponse>();
        CreateMap<User, GetAllUserResponse>();
        CreateMap<User, GetByEmailUserResponse>();
        CreateMap<User,GetByIdUserResponse>();
    }
}
