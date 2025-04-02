namespace BarterProject.Common.GlobalResponses.Generics;

public class Pagination<T>
{
    public Pagination(List<T> data, int totalDataCount, bool isSuccess)
    {
        Data = data;
        TotalDataCount = totalDataCount;
        IsSuccess = isSuccess;
    }

    public Pagination()
    {
        Data = [];
        TotalDataCount = 0;
        IsSuccess = true;
    }

    public List<T> Data { get; set; }
    public int TotalDataCount { get; set; }
    public bool IsSuccess { get; set; }
}
