using System;
using System.Collections.Generic;
using System.Text;
using User.Domain.SeedWork.ValueObject;

namespace User.Domain.AggregatesModel.UserAggregates.Entitys.ValueObjects
{
    public class Address : ValueObject
    {

        /// <summary>
        /// 省份
        /// </summary>
        public string Province { get; private set; }

        /// <summary>
        /// 城市
        /// </summary>
        public string City { get; private set; }

        /// <summary>
        /// 区县
        /// </summary>
        public string County { get; private set; }

        /// <summary>
        /// 街道
        /// </summary>
        public string Street { get; private set; }

        /// <summary>
        /// 邮政编码
        /// </summary>
        public string Zip { get; private set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            throw new NotImplementedException();
        }
    }
}
