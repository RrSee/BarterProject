namespace BarterProject.Common.GlobalResponses.Generics;

public class ResultPagination<T> : Result<T>
{
    public Pagination<T>? Data { get; set; }

    public ResultPagination(List<string> message) : base(message)
    {

    }

    public ResultPagination()
    {

    }
}
