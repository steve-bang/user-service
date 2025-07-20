/*
* Author: Steve Bang
* History:
* - [2025-05-02] - Created by mrsteve.bang@gmail.com
*/

namespace Steve.ManagerHero.SharedKernel.Application.Pagination;

public class PaginationQuery
{
    /// <summary>
    /// The page number to retrieve.
    /// The default value is 1.
    /// </summary>
    public int PageNumber { get; set; } = PaginationConstant.PageNumberDefault;

    /// <summary>
    /// The number of items to retrieve per page.
    /// The default value is 10.
    /// </summary>
    public int PageSize { get; set; } = PaginationConstant.PageSizeDefault;

    public PaginationQuery()
    {
    }

    public PaginationQuery(int pageNumber, int pageSize)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
    }
}