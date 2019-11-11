using System;
using System.Collections.Generic;
using System.Text;

namespace User.Domain.SeedWork.IAggregateRoot
{
    public  interface IAggregateRootBase
    {
        string _createUserId { get; set; }
        string _createUserName { get; set; }

        DateTime _updateTime { get; set; }
        DateTime _createtime{ get; set; }
    }
}
