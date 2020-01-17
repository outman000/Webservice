using System;
using System.Collections.Generic;
using System.Text;
using User.Domain.AggregatesModel.UserAggregates.Entitys.ValueObjects;
using User.Domain.Exceptions;
using User.Domain.SeedWork.BaseEntity;
using User.Domain.SeedWork.IAggregateRoot;

namespace User.Domain.AggregatesModel.UserAggregates.Entitys
{
    public class UserInformation : Entity, IAggregateRootBase
    {
        private string userId;
        private string userName;
        private string userPwd;
        private string gender;
        private string birthdate;
        private string phoneCall;
        private string mobileCall;
        private string email;

        public DateTime updateTime { get; set; }
        public DateTime createtime { get; set; }
        private string description;
        private CreateUserInfo createUserInfo;

        private List<long> departs;



        public Address address;
        public UserStatus status { get; private set; }
        private int userStatusId;
        /// <summary>
        ///  添加用户全部信息
        /// </summary>
        public UserInformation(string Userid, string UserPwd, string Gender, string Birthdate, string UserName,
                    string PhoneCall, string MobileCall, string Email, CreateUserInfo CreateInfo, List<long> deptid,Address address)
        {
            userId = Userid;
            userName = UserName;
            userPwd = UserPwd;
            gender = Gender;
            birthdate = Birthdate;
            phoneCall = PhoneCall;
            mobileCall = MobileCall;
            email = Email;
            createUserInfo = CreateInfo;
            updateTime = DateTime.Now;
            createtime = DateTime.Now;
            userStatusId = UserStatus.Activated.Id;
            departs = deptid;
            this.address = address;
        }
        /// <summary>
        /// 设置用户部门
        /// </summary>
        public void AddDepart(int id)
        {
            departs.Add(id);
        }

        public void deleteDepart(List<int> departsIds)
        {
            for (int j = 0; j < departsIds.Count; j++)
            {
                for (int i = 0; i < departs.Count; i++)
                {
                    if (departsIds[j] == departs[i])
                    {
                        departs.Remove(i);
                    }
                }
            }
            
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
