﻿namespace BarterProject.Application.CQRS.Comments.Queries.Responses;

public class GetAllCommentResponse
{
    public string Description { get; set; }
    public int UserId { get; set; }
    public int ItemId { get; set; }
}
