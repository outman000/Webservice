using MediatR;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace User.Domain.SeedWork.BaseEntity
{
    public abstract class EntityT<KeyTypeT>
    {
        /// <summary>
        /// 请求哈希值
        /// </summary>
        int? _requestedHashCode;
        KeyTypeT _Id;
        /// <summary>
        /// 领域标识
        /// </summary>
        public virtual KeyTypeT Id
        {
            get
            {
                return _Id;
            }
            protected set
            {
                _Id = value;
            }
        }
        /// <summary>
        /// 领域事件聚合
        /// </summary>
        private List<INotification> _domainEvents;
        public IReadOnlyCollection<INotification> DomainEvents => _domainEvents?.AsReadOnly();
        /// <summary>
        /// 添加一个领域事件
        /// </summary>
        /// <param name="eventItem"></param>
        public void AddDomainEvent(INotification eventItem)
        {
            _domainEvents = _domainEvents ?? new List<INotification>();
            _domainEvents.Add(eventItem);
        }
        /// <summary>
        ///  删除领域事件
        /// </summary>
        /// <param name="eventItem"></param>
        public void RemoveDomainEvent(INotification eventItem)
        {
            _domainEvents?.Remove(eventItem);
        }
        /// <summary>
        /// 清空领域事件
        /// </summary>
        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }

        public bool IsTransient()
        {
            return EqualityComparer<KeyTypeT>.Default.Equals(this.Id, default(KeyTypeT)); ;
        }
        /// <summary>
        /// 重写基类相等方法
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is EntityT<KeyTypeT>))
                return false;

            if (Object.ReferenceEquals(this, obj))
                return true;

            if (this.GetType() != obj.GetType())
                return false;

            EntityT<KeyTypeT> item = (EntityT<KeyTypeT>)obj;

            if (item.IsTransient() || this.IsTransient())
                return false;
            else
                return item.Id.Equals(this.Id);
        }





        /// <summary>
        /// 重写获取hash值
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            if (!IsTransient())
            {
                if (!_requestedHashCode.HasValue)
                    _requestedHashCode = this.Id.GetHashCode() ^ 31; // XOR for random distribution (http://blogs.msdn.com/b/ericlippert/archive/2011/02/28/guidelines-and-rules-for-gethashcode.aspx)

                return _requestedHashCode.Value;
            }
            else
                return base.GetHashCode();

        }

        /// <summary>
        /// 操作符重载，判断是否相等
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(EntityT<KeyTypeT> left, EntityT<KeyTypeT> right)
        {
            if (Object.Equals(left, null))
                return (Object.Equals(right, null)) ? true : false;
            else
                return left.Equals(right);
        }
        /// <summary>
        /// 操作符重载判断是否不相等
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>

        public static bool operator !=(EntityT<KeyTypeT> left, EntityT<KeyTypeT> right)
        {
            return !(left == right);
        }

    }
}
