using System;
using System.Collections.Generic;
using System.Text;
using User.Domain.AggregatesModel.OrganizationAggregate.Entitys;
using User.Domain.AggregatesModel.UserAggregates.Entitys.ValueObjects;
using User.Domain.Exceptions;
using User.Domain.SeedWork.BaseEntity;
using User.Domain.SeedWork.IAggregateRoot;

namespace User.Domain.AggregatesModel.UserAggregates.Entitys
{
    public class UserInformation : Entity, IAggregateRootBase
    {
        //字段
        public string userId { get; private set; }
        public string userName { get; private set; }
        public string userPwd { get; private set; }
        public string gender { get; private set; }
        public DateTime? birthdate { get; private set; }
        public string phoneCall { get; private set; }
        public string mobileCall { get; private set; }
        public string email { get; private set; }
        public string description { get; private set; }
        public DateTime? updateTime { get; private set; }
        public DateTime? createtime { get; private set; }
        public CreateUserInfo createUserInfo { get; private set; }
        //值对象

        public Address address { get; private set; }

        //建

        private int userStatusId ;
        public UserStatus status { get; private set; }
 
        protected UserInformation()
        { 
        //ef
        }
        /// <summary>
        ///  添加用户全部信息
        /// </summary>
        public UserInformation(string userId, string UserPwd, string Gender, DateTime Birthdate, string UserName,
                    string PhoneCall, string MobileCall, string Email, CreateUserInfo CreateInfo,Address address)
        {
            this.userId = userId;
            this.userName = UserName;
            this.userPwd = UserPwd;
            this.gender = Gender;
            this.birthdate = Birthdate;
            this.phoneCall = PhoneCall;
            this.mobileCall = MobileCall;
            this.email = Email;
            this.createUserInfo = CreateInfo;
            this.updateTime = DateTime.Now;
            this.createtime = DateTime.Now;
            this.userStatusId = UserStatus.Activated.Id;
      
            this.address = address;
        }
     




        /// <summary>
        /// 开始添加用户领域事件到事件集合中(再执行事务操作的时候调用领域事件)
        /// </summary>
        private void AddUserStartedDomainEvent(string Userid, string Gender, string Birthdate,
                    string PhoneCall, string MobileCall, string Email
                    , DateTime UpdateTime, DateTime Createtime, long deptid)
        {
            // var UserCreatedDomainEvent = new UserCreatedDomainEvent(this, Userid, Gender, Birthdate,
            //      PhoneCall, MobileCall, Email, UpdateTime
            //    , Createtime, deptid);

            //   this.AddDomainEvent(UserCreatedDomainEvent);
        }

        /// <summary>
        /// 设置冻结状态
        /// </summary>
        public void SetFrozendStatus()
        {
            if (userStatusId != UserStatus.Frozen.Id)
            {
                StatusChangeException(UserStatus.Activated);
            }

            userStatusId = UserStatus.Activated.Id;
            description = "The User was Frozend.";
            // AddDomainEvent(new OrderShippedDomainEvent(this));
        }
        /// <summary>
        /// 设置激活状态
        /// </summary>
        public void SetActivatedStatus()
        {
            if (userStatusId == UserStatus.Frozen.Id)
            {
                StatusChangeException(UserStatus.Frozen);
            }
            userStatusId = UserStatus.Activated.Id;
            description = $"The order was cancelled.";
            //     AddDomainEvent(new OrderCancelledDomainEvent(this));
        }
        /// <summary>
        /// 状态修改异常
        /// </summary>
        private void StatusChangeException(UserStatus userStatus)
        {
            throw new UserDomainException($"Is not possible to change the order status from {userStatus.Name} to {userStatus.Name}.");
        }
    }
}
