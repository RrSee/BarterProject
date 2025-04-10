﻿namespace BarterProject.Application.CQRS.Items.Queries.Responses;

public class SearchItemsByCategoryAndNameQueryResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string CategoryName { get; set; }
}
