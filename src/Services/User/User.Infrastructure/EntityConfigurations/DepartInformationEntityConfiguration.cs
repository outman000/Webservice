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
        public void Configure(EntityTypeBuilder<DepartInformation> UserConfiguration)
        {
            UserConfiguration.ToTable("User_Department", UserContext.DEFAULT_SCHEMA);
            UserConfiguration.HasKey(o => o.Id);
            UserConfiguration.Ignore(b => b.DomainEvents);
            UserConfiguration.HasOne<UserInformation>()
             .WithMany()
             .IsRequired(false)
             // .HasForeignKey("BuyerId");
             .HasForeignKey("_UserId");
        }
    }
}
