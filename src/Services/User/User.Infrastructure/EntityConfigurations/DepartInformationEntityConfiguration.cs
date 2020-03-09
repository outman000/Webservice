using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using User.Domain.AggregatesModel.OrganizationAggregate.Entitys;
using User.Domain.AggregatesModel.UserAggregates.Entitys;

namespace User.Infrastructure.EntityConfigurations
{
    public class DepartInformationEntityConfiguration
    {
        public void Configure(EntityTypeBuilder<DepartInformation> DepartRelate)
        {
            DepartRelate.ToTable("User_Organization", UserContext.DEFAULT_SCHEMA);
            DepartRelate.HasKey(o => o.Id);
            DepartRelate.Ignore(b => b.DomainEvents);

            DepartRelate.Property<long>("_departparentId")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("departparentId")
                .IsRequired();

                                 

            DepartRelate.OwnsOne(o => o.createUserInfo);



        }
    }
}
