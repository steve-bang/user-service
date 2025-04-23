
/*
* Author: Steve Bang
* History:
* - [2025-04-24] - Created by mrsteve.bang@gmail.com
*/

using System.Linq.Expressions;
using Steve.ManagerHero.UserService.Infrastructure.Shared;

namespace Steve.ManagerHero.Application.Processors;

public interface IScimFilterProcessor<T>
{
    Expression<Func<T, bool>> ParseFilter(string filter);
}

public class ScimFilterProcessor<T> : IScimFilterProcessor<T>
{
    public Expression<Func<T, bool>> ParseFilter(string filter)
    {
        return filter.ParseSimpleScimFilter<T>();
    }
}