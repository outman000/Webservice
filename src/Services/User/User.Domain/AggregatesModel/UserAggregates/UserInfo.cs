using System;
using System.Collections.Generic;
using System.Text;
using User.Domain.AggregatesModel.UserRoleAggregates;
using User.Domain.Events.UserEvents;
using User.Domain.Exceptions;
using User.Domain.SeedWork.BaseEntity;
using User.Domain.SeedWork.IAggregateRoot;

namespace User.Domain.AggregatesModel.UserAggregates
{
    public class UserInfo : Entity, IAggregateRootBase
    {
        private string _userId;
        private string _userName;
        private string _userPwd ;
        private string _gender ;
        private string _birthdate ;
        private string _phoneCall ;
        private string _mobileCall ;
        private string _email ;
        public string _createUserId { get; set; }
        public string _createUserName { get; set; }
        public DateTime _updateTime { get; set; }
        public DateTime _createtime { get; set; }
        private string _description;
        public long? GetDepartid => _departid;
        private long? _departid;
        public UserStatus status { get; private set; }
        private int _userStatusId;   
        /// <summary>
        ///  添加用户全部信息
        /// </summary>
        public UserInfo(string Userid, string UserPwd, string Gender, string Birthdate,string UserName,
                    string PhoneCall, string MobileCall, string Email, string Createuserid,
                    string createUserName,long deptid)
        {
             _userId= Userid;
            _userName = UserName;
             _userPwd= UserPwd;
             _gender= Gender;
             _birthdate= Birthdate;
             _phoneCall= PhoneCall;
             _mobileCall= MobileCall;
             _email= Email;
             _createUserId = Createuserid;
             _createUserName = createUserName;
             _updateTime = DateTime.Now;
             _createtime = DateTime.Now;
             _userStatusId = UserStatus.Activated.Id;
             _departid = deptid;
            //  添加完成后调用触发添加领域事件

            AddUserStartedDomainEvent(_userId, _userPwd, _gender,
                     _birthdate, _phoneCall, _mobileCall, _updateTime, _createtime, deptid);

        }


        /// <summary>
        /// 设置用户部门
        /// </summary>
        public void SetDepartid(int id)
        {
            _departid = id;
        }    
        /// <summary>
        /// 开始添加用户领域事件到事件集合中(再执行事务操作的时候调用领域事件)
        /// </summary>
        private void AddUserStartedDomainEvent(string Userid, string Gender, string Birthdate,
                    string PhoneCall, string MobileCall, string Email
                    , DateTime UpdateTime, DateTime Createtime,long deptid)
        {
            var UserCreatedDomainEvent = new UserCreatedDomainEvent(this, Userid, Gender, Birthdate,
                                                                      PhoneCall, MobileCall, Email, UpdateTime
                                                                     , Createtime, deptid);
            
            this.AddDomainEvent(UserCreatedDomainEvent);
        }

        /// <summary>
        /// 设置冻结状态
        /// </summary>
        public void SetFrozendStatus()
        {
            if (_userStatusId != UserStatus.Frozen.Id)
            {
                StatusChangeException(UserStatus.Activated);
            }

            _userStatusId = UserStatus.Activated.Id;
            _description = "The User was Frozend.";
           // AddDomainEvent(new OrderShippedDomainEvent(this));
        }
        /// <summary>
        /// 设置激活状态
        /// </summary>
        public void SetActivatedStatus()
        {
            if (_userStatusId == UserStatus.Frozen.Id)   
            {
                StatusChangeException(UserStatus.Frozen);
            }
            _userStatusId = UserStatus.Activated.Id;
            _description = $"The order was cancelled.";
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
