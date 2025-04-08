using AutoMapper;
using BarterProject.Application.CQRS.BarterRequests.Commands.Requests;
using BarterProject.Application.CQRS.BarterRequests.Commands.Responses;
using BarterProject.Application.CQRS.Categories.Command.Requests;
using BarterProject.Application.CQRS.Categories.Command.Responses;
using BarterProject.Application.CQRS.Categories.Queries.Responses;
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
        CreateMap<Notification, GetByIdNotificationResponse>();
        CreateMap<Notification, GetByUserIdNotificationsResponse>();
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
    .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => false))            
    .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId)); 



        CreateMap<Item, AddItemCommandResponse>();
        CreateMap<UpdateItemCommandRequest, Item>()
            .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(src => DateTime.Now)); 
        CreateMap<Item, UpdateItemCommandResponse>();
        CreateMap<DeleteItemCommandRequest, Item>();
        CreateMap<Item, GetAllItemsQueryResponse>();
        CreateMap<Item, GetByIdItemQueryResponse>();
        CreateMap<Item, GetByUserIdItemsQueryResponse>();
        CreateMap<Item, SearchItemsByNameQueryResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name)) 
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));
        CreateMap<Item, GetItemsByCategoryIdQueryResponse>()
    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
    .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));
        CreateMap<Item, SearchItemsByCategoryAndNameQueryResponse>()
         .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
         .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
         .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
         .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name)); 

    //User
    CreateMap<RegisterUserRequest,User>();
        CreateMap<User, RegisterUserResponse>();
        CreateMap<User,UpdateUserResponse>();
        CreateMap<User, GetAllUserResponse>();
        CreateMap<User, GetByEmailUserResponse>();
        CreateMap<User,GetByIdUserResponse>();
        //Category
        CreateMap<CreateCategoryCommandRequest, Category>()
        .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.Now));
        CreateMap<Category, CreateCategoryCommandResponse>();
        CreateMap<UpdateCategoryCommandRequest, Category>();
        CreateMap<Category, UpdateCategoryCommandResponse>();
        CreateMap<Category, GetCategoryByIdQueryResponse>();
        CreateMap<Category, GetAllCategoriesQueryResponse>();
    }
}
