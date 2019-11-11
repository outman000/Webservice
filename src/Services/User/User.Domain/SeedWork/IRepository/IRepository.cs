using System;
using System.Collections.Generic;
using System.Text;
using User.Domain.SeedWork.IAggregateRoot;
using User.Domain.SeedWork.IUnitWork;

namespace User.Domain.SeedWork.IRepository
{
    public interface IRepository<T> where T : IAggregateRootBase
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
