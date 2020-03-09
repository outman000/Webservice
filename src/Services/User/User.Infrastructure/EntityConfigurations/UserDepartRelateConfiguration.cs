using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using User.Domain.AggregatesModel.UserDepartRelateAggregates;

namespace User.Infrastructure.EntityConfigurations
{
    public class UserDepartRelateConfiguration
    {
        public void Configure(EntityTypeBuilder<UserDepartRelate> DepartRelate)
        {
            //DepartRelate.ToTable("User_Organization_Relate", UserContext.DEFAULT_SCHEMA);

            DepartRelate
            .Property<int?>("userid")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .IsRequired(false);
            DepartRelate
           .Property<int?>("orgid")
           .UsePropertyAccessMode(PropertyAccessMode.Field)
           .IsRequired(false);
        }
    }
}
