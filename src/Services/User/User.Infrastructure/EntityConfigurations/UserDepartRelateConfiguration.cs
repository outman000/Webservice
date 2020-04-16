using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using User.Domain.AggregatesModel.UserDepartRelateAggregates;

namespace User.Infrastructure.EntityConfigurations
{
    public class UserDepartRelateConfiguration : IEntityTypeConfiguration<UserDepartRelate>
    {
        public void Configure(EntityTypeBuilder<UserDepartRelate> DepartRelate)
        {
            DepartRelate.ToTable("UserOrganization_Relate", UserContext.DEFAULT_SCHEMA);

            DepartRelate.HasKey(o => o.Id);
            DepartRelate.Ignore(b => b.DomainEvents);



            DepartRelate
            .Property<long>("userid")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .IsRequired();
            DepartRelate
            .Property<long> ("orgid")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .IsRequired();


    
        }
    }
}
