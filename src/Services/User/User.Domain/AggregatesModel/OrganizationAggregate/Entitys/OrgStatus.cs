using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using User.Domain.Exceptions;
using User.Domain.SeedWork.Enumerate;

namespace User.Domain.AggregatesModel.OrganizationAggregate.Entitys
{
    public  class OrgStatus : Enumeration
    {
        public static OrgStatus Activated = new OrgStatus(1, nameof(Activated).ToLowerInvariant());
        public static OrgStatus Frozen = new OrgStatus(2, nameof(Frozen).ToLowerInvariant());
        public static OrgStatus Deleted = new OrgStatus(3, nameof(Deleted).ToLowerInvariant());
        //定义一个返回状态的集合
        public static IEnumerable<OrgStatus> List() => new[] { Activated, Frozen };
        /// <summary>
        /// 调用基类构造函数
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        public OrgStatus(int id, string name)
            : base(id, name)
        {

        }
        public static OrgStatus FromName(string name)
        {
            var state = List()
                .SingleOrDefault(s => String.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (state == null)
            {
                throw new UserDomainException($"Possible values for OrgStatus: {String.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }
        public static OrgStatus From(int id)
        {
            var state = List().SingleOrDefault(s => s.Id == id);

            if (state == null)
            {
                throw new UserDomainException($"Possible values for OrgStatus: {String.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }
    }
}
