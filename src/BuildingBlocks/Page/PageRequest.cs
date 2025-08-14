
namespace Steve.ManagerHero.BuildingBlocks.Page;

public class PageRequest
{
    public int PageNumber { get; init; } = PageConstant.PageNumberDefault;
    public int PageSize { get; init; } = PageConstant.PageSizeDefault;
}