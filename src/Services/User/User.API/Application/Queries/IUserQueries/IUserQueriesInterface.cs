using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.Domain.AggregatesModel.UserAggregates;

namespace User.API.Application.Queries.IUserQueries
{
    //CQRS模式查询
     public  interface IUserQueriesInterface
    {
        Task<IEnumerable<UserInfo>> GetOrderAsync (int id);

      
    }
}
