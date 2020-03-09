using System;
using System.Collections.Generic;
using System.Text;
using User.Domain.SeedWork.BaseEntity;
using User.Domain.SeedWork.IAggregateRoot;

namespace User.Domain.AggregatesModel.UserDepartRelateAggregates
{
    public class UserDepartRelate : Entity, IAggregateRootBase
    {
        public long userid { get; private set; }
        public long orgid { get; private set; }
        public string descript { get; private set; }
        public string relatetype { get; private set; }

        public UserDepartRelate(long userid, long orgid, string descript, string relatetype)
        {
            this.userid = userid;
            this.orgid = orgid;
            this.descript = descript ?? throw new ArgumentNullException(nameof(descript));
            this.relatetype = relatetype ?? throw new ArgumentNullException(nameof(relatetype));
        }
    }
}
