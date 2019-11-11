using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using User.Domain.AggregatesModel.UserAggregates;
namespace User.Domain.Events.UserEvents
{
    public class UserCreatedDomainEvent : INotification 
    {
        private string _userId { get; }
       
        private string _gender { get; }
        private string _birthdate { get; }
        private string _phoneCall { get; }
        private string _mobileCall { get; }
        private string _email { get; }
        public DateTime _updateTime { get; }
        public DateTime _createtime { get; }
        public UserInfo UserInfo { get; }


        public UserCreatedDomainEvent(UserInfo UserInfo, string userid, string Gender, string Birthdate,
                   string PhoneCall, string MobileCall, string Email,
                   DateTime UpdateTime, DateTime Createtime, long deptid)
        {
            _userId = userid;
            _gender = Gender;
            _birthdate = Birthdate;
            _phoneCall = PhoneCall;
            _mobileCall = MobileCall;
            _email = Email;
            _updateTime = UpdateTime;
            _createtime = Createtime;
            //  添加完成后调用触发添加领域事件

        }

    }
}
