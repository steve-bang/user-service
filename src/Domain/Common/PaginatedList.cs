/*
* Author: Steve Bang
* History:
* - [2025-04-24] - Created by mrsteve.bang@gmail.com
*/

public class PaginatedList<T>
{
    public List<T>? Items { get; init; }
    public long TotalCount { get; init; }
}