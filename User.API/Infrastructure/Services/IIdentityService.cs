using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace User.API.Infrastructure.Services
{
    /// <summary>
    /// 身份服务
    /// </summary>
    public  interface IIdentityService
    {
        /// <summary>
        /// 获得用户身份
        /// </summary>
        /// <returns></returns>
        string GetUserIdentity();
        /// <summary>
        /// 获取用户名称
        /// </summary>
        /// <returns></returns>
        string GetUserName();
    }
}
