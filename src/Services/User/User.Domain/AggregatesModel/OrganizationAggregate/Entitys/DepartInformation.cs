using System;
using System.Collections.Generic;
using System.Text;
using User.Domain.AggregatesModel.OrganizationAggregate.Entitys.ValueObjects;
using User.Domain.SeedWork.BaseEntity;
using User.Domain.SeedWork.IAggregateRoot;

namespace User.Domain.AggregatesModel.OrganizationAggregate.Entitys
{
    public class DepartInformation : Entity, IAggregateRootBase
    {
        //字段
        public string orgName { get; private set; }
        public string orgCode { get; private set; }
        public long orgParentcode { get; private set; }
        public string orgType { get; private set; }
        public string unitId { get; private set; }
        public string unionType { get; private set; }
        public string orgPath { get; private set; }
        public string orgSequence { get; private set; }
        public string orgLevel { get; private set; }
        public string orgGlobalNo { get; private set; }
        //值对象
        public CreateUserInfo createUserInfo { get; private set; }
        //建
        public long? _departparentId { get; private set ; }
        public   DepartInformation departInformation { get;  set; }

        public DepartInformation()
        {
        }


        //方法
        public DepartInformation(string orgName, string orgCode, long orgParentcode, string orgType, string unitId,
            string unionType, string orgPath, string orgSequence, string orgLevel, string orgGlobalNo, 
            long parentId, CreateUserInfo createUserInfo, DepartInformation departInformation)
        {
            this.orgName = orgName ?? throw new ArgumentNullException(nameof(orgName));
            this.orgCode = orgCode ?? throw new ArgumentNullException(nameof(orgCode));
            this.orgParentcode = orgParentcode;
            this.orgType = orgType ?? throw new ArgumentNullException(nameof(orgType));
            this.unitId = unitId ?? throw new ArgumentNullException(nameof(unitId));
            this.unionType = unionType ?? throw new ArgumentNullException(nameof(unionType));
            this.orgPath = orgPath ?? throw new ArgumentNullException(nameof(orgPath));
            this.orgSequence = orgSequence ?? throw new ArgumentNullException(nameof(orgSequence));
            this.orgLevel = orgLevel ?? throw new ArgumentNullException(nameof(orgLevel));
            this.orgGlobalNo = orgGlobalNo ?? throw new ArgumentNullException(nameof(orgGlobalNo));
            _departparentId = parentId;
            this.createUserInfo = createUserInfo ?? throw new ArgumentNullException(nameof(createUserInfo));
            this.departInformation = departInformation ?? throw new ArgumentNullException(nameof(departInformation));
        }
    } 
}
