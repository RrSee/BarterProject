﻿namespace BarterProject.Application.CQRS.Items.Queries.Responses;

public class GetByIdItemQueryResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string ImagePath { get; set; }
    public DateTime UpdatedDate { get; set; }
}
