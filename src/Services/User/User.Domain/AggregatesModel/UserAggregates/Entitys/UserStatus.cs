using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using User.Domain.Exceptions;
using User.Domain.SeedWork.Enumerate;

namespace User.Domain.AggregatesModel.UserAggregates.Entitys
{
    public class UserStatus: Enumeration
    {
        public static UserStatus Activated = new UserStatus(1, nameof(Activated).ToLowerInvariant());
        public static UserStatus Frozen = new UserStatus(2, nameof(Frozen).ToLowerInvariant());
        public static UserStatus Deleted = new UserStatus(3, nameof(Deleted).ToLowerInvariant());
        //定义一个返回状态的集合
        public static IEnumerable<UserStatus> List() => new[] { Activated, Frozen };
        /// <summary>
        /// 调用基类构造函数
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        public UserStatus(int id, string name)
            : base(id, name)
        {

        }
        public static UserStatus FromName(string name)
        {
            var state = List()
                .SingleOrDefault(s => String.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (state == null)
            {
                throw new UserDomainException($"Possible values for OrderStatus: {String.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }
        public static UserStatus From(int id)
        {
            var state = List().SingleOrDefault(s => s.Id == id);

            if (state == null)
            {
                throw new UserDomainException($"Possible values for OrderStatus: {String.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }
    }
}
