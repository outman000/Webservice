using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using User.Domain.AggregatesModel.UserAggregates.Entitys;
using User.Domain.SeedWork.IRepository;

namespace User.Domain.AggregatesModel.UserAggregates.Respository
{
    public  interface IUserRespository:IRepository<UserInformation>
    {

        UserInformation Add(UserInformation order);

        void Update(UserInformation order);

        Task<UserInformation> GetAsync(string userid);

    }
}
