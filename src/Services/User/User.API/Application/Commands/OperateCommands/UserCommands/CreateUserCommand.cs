using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace User.API.Application.Commands.OperateCommands.UserCommands
{
    public class CreateUserCommand : IRequest<bool>
    {
        [DataMember]
        public string UserId { get; private set; }
        [DataMember]
        public string UserName { get; private set; }
        [DataMember]
        public string UserPwd { get; private set; }
        [DataMember]
        public string Gender { get; private set; }
        [DataMember]
        public string Birthdate { get; private set; }
        [DataMember]
        public string PhoneCall { get; private set; }
        [DataMember]
        public string MobileCall { get; private set; }
        [DataMember]
        public string Email { get; private set; }
        [DataMember]
        public string CreateUserId { get; private set; }
        [DataMember]
        public string CreateUserName { get; private set; }
        [DataMember]
        public DateTime UpdateTime { get; private set; }
        [DataMember]
        public DateTime Createtime { get; private set; }
        [DataMember]
        public long deptid { get; private set; }

        public CreateUserCommand(string userId, string userPwd,string username,
                                 string gender, string birthdate, string phoneCall,
                                 string mobileCall, string email, 
                                 String createUserId, string createUserName,
                                 DateTime updateTime, DateTime createtime,
                                 long deptid)
        {
            UserId = userId;
            UserPwd = userPwd;
            UserName = username;
            Gender = gender;
            Birthdate = birthdate;
            PhoneCall = phoneCall;
            MobileCall = mobileCall;
            Email = email;
            CreateUserId = createUserId;
            CreateUserName = createUserName;
            UpdateTime = updateTime;
            Createtime = createtime;
            this.deptid = deptid;
        }

    }
}
