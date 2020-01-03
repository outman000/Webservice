using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using User.Domain.AggregatesModel.UserAggregates;

namespace User.Infrastructure.EntityConfigurations
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<UserInfo>
    {
        public void Configure(EntityTypeBuilder<UserInfo> orderConfiguration)
        {
            orderConfiguration.ToTable("buyers", UserContext.DEFAULT_SCHEMA);

            orderConfiguration.HasKey(b => b.Id);

            orderConfiguration.Ignore(b => b.DomainEvents);

    

            orderConfiguration.Property(b => b._createUserName)
                .HasMaxLength(200)
                .IsRequired();

            orderConfiguration.HasIndex("IdentityGuid")
              .IsUnique(true);

          //  var navigation = orderConfiguration.Metadata.FindNavigation(nameof(Buyer.PaymentMethods));

            //navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
