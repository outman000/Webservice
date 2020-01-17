using System;
using System.Collections.Generic;
using System.Text;
using User.Domain.SeedWork.BaseEntity;
using User.Domain.SeedWork.IAggregateRoot;

namespace User.Domain.AggregatesModel.OrganizationAggregate.Entitys
{
    public class DepartInformation : Entity, IAggregateRootBase
    {


        private string _orgName;
        private string _orgCode;
        private long _orgParentcode;
        private string _orgType;
        private string _unitId;
        private string _unionType;
        private string _orgPath;
        private string _orgSequence;
        private string _createUsername;
        private DateTime _createtime;
        private string _orgLevel;
        private string _orgGlobalNo;
        private List<long> _userList;




    } 
}
