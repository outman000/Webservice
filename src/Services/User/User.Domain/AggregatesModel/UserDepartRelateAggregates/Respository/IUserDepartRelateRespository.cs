using System;
using System.Collections.Generic;
using System.Text;

namespace User.Domain.AggregatesModel.UserDepartRelateAggregates.Respository
{
    public interface IUserDepartRelateRespository
    {
        /// <summary>
        /// 建议一个用户和部门的关系
        /// </summary>
        /// <param name="userDepartRelate"></param>
        void AddRelate(UserDepartRelate userDepartRelate);
        /// <summary>
        /// 建议一个用户和部门的关系
        /// </summary>
        /// <param name="userDepartRelate"></param>
        void DeleteRelate(UserDepartRelate userDepartRelate);
        /// <summary>
        /// 建议一个用户和部门的关系
        /// </summary>
        /// <param name="userDepartRelate"></param>
        void ModifyRelate(UserDepartRelate userDepartRelate);


    }
}
