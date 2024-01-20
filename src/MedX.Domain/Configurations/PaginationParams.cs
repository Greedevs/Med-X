namespace MedX.Domain.Configurations;

public class PaginationParams
{
    private const int maxSize = 20;
    private int pageSize;
    public int PageSize
    {
        get => pageSize == 0 ? 10 : pageSize;
        set { pageSize = value > maxSize ? maxSize : (value <= 0 ? 1 : value); }
    }
    public int PageIndex { get; set; } = 1;
}