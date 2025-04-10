﻿using BarterProject.Application.CQRS.Notifications.Commands.Responses;
using BarterProject.Common.GlobalResponses.Generics;
using MediatR;

namespace BarterProject.Application.CQRS.Notifications.Commands.Requests;

public class UpdateNotificationCommand : IRequest<Result<UpdateNotificationResponse>>
{
    public int NotificationId { get; set; }
    public string Description { get; set; }
    public int UpdatedBy { get; set; }

    public UpdateNotificationCommand(int notificationId, string description, int updatedBy)
    {
        NotificationId = notificationId;
        Description = description;
        UpdatedBy = updatedBy;
    }
}
