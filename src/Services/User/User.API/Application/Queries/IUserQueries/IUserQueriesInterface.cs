using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.Domain.AggregatesModel.UserAggregates;
using User.Domain.AggregatesModel.UserAggregates.Entitys;

namespace User.API.Application.Queries.IUserQueries
{
    //CQRS模式查询
     public  interface IUserQueriesInterface
    {
        Task<IEnumerable<UserInformation>> GetOrderAsync (int id);

      
    }
}
