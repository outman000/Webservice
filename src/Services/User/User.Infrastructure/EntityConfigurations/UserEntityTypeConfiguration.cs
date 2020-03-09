using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using User.Domain.AggregatesModel.OrganizationAggregate.Entitys;
using User.Domain.AggregatesModel.UserAggregates.Entitys;

namespace User.Infrastructure.EntityConfigurations
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<UserInformation>
    {
        public void Configure(EntityTypeBuilder<UserInformation> UserConfiguration)
        {
            UserConfiguration.ToTable("User_Information", UserContext.DEFAULT_SCHEMA);
            //忽略
            UserConfiguration.Ignore(b => b.DomainEvents);
            //兼职
            UserConfiguration.HasKey(o=>o.Id);
            UserConfiguration
               .Property<int>("_userStatusId")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("userStatusId")
                .IsRequired();

            //值对象
            UserConfiguration.OwnsOne(o => o.address);
            UserConfiguration.OwnsOne(o => o.createUserInfo);
        }
    }
}
