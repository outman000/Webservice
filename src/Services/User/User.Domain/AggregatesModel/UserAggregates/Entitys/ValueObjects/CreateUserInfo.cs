using System;
using System.Collections.Generic;
using System.Text;
using User.Domain.SeedWork.ValueObject;

namespace User.Domain.AggregatesModel.UserAggregates.Entitys.ValueObjects
{
    public class CreateUserInfo:ValueObject
    {
        public string _createUserId { get; set; }
        public string _createUserName { get; set; }

        public CreateUserInfo(string createUserId, string createUserName)
        {
            _createUserId = createUserId ?? throw new ArgumentNullException(nameof(createUserId));
            _createUserName = createUserName ?? throw new ArgumentNullException(nameof(createUserName));
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            throw new NotImplementedException();
        }
    }
}
