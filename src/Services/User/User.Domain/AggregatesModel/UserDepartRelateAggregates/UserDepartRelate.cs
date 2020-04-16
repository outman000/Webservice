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

        public DateTime?  createadate { get; private set; }

        public DateTime? updatedate { get; private set; }

        public UserDepartRelate()
        {
        }

        public UserDepartRelate(long userid, long orgid, string descript, string relatetype, DateTime? createadate, DateTime? updatedate)
        {
            this.userid = userid;
            this.orgid = orgid;
            this.descript = descript;
            this.relatetype = relatetype;
            this.createadate = createadate;
            this.updatedate = updatedate;
        }
    }
}
