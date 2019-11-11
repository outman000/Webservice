using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using User.Domain.SeedWork.IRepository;

namespace User.Domain.AggregatesModel.UserAggregates
{
    public  interface IUserRespository : IRepository<UserInfo>
    {
        UserInfo Add(UserInfo userInfo);

        void Update(UserInfo  userInfo);

        Task<UserInfo> GetAsync(string userid);
    }
}
