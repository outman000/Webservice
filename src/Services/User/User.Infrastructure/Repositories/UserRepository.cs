using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using User.Domain.AggregatesModel.UserAggregates;
using User.Domain.AggregatesModel.UserAggregates.Entitys;
using User.Domain.AggregatesModel.UserAggregates.Respository;
using User.Domain.SeedWork.IUnitWork;

namespace User.Infrastructure.Repositories
{
    public class UserRepository : IUserRespository
    {
        private readonly UserContext _context;
        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public UserRepository(UserContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public UserInformation Add(UserInformation userInfo)
        {
            return _context.UserInformation.Add(userInfo).Entity;
        }

        public async Task<UserInformation> GetAsync(string userid)
        {
            var userInfo = await _context.UserInformation.FindAsync(userid);
            if (userInfo != null)
            {
                await _context.Entry(userInfo)
                    .Reference(i => i.status).LoadAsync();
            }
            return userInfo;
        }

        public void Update(UserInformation userInfo)
        {
            _context.Entry(userInfo).State = EntityState.Modified;
        }

        public void Delete(UserInformation userInfo)
        {
            _context.Entry(userInfo).State = EntityState.Deleted;
        }

    }
}
