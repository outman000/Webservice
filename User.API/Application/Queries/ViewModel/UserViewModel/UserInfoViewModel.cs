using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace User.API.Application.Queries.ViewModel.UserViewModel
{
    public class UserInfoViewModel
    {
        public string userId { get; set; }
        public string userName { get; set; }
        public string userPwd { get; set; }
        public string gender { get; set; }
        public string birthdate { get; set; }
        public string phoneCall { get; set; }
        public string mobileCall { get; set; }
        public string email { get; set; }
        public string createUserId { get; set; }
        public string createUserName { get; set; }
        public DateTime updateTime { get; set; }
        public DateTime createtime { get; set; }
        private string description { get; set; }
        private long? departid { get; set; }
        public string status { get;  set; }
    }
}
