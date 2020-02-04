using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using User.Domain.AggregatesModel.OrganizationAggregate.Entitys;
using User.Domain.AggregatesModel.UserAggregates.Entitys;

namespace User.Infrastructure.EntityConfigurations
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<UserInformation>
    {
        public void Configure(EntityTypeBuilder<UserInformation> UserConfiguration)
        {
            UserConfiguration.ToTable("User_Information", UserContext.DEFAULT_SCHEMA);
            UserConfiguration.HasKey(o=>o.Id);
            UserConfiguration.Ignore(b => b.DomainEvents);
            UserConfiguration.HasOne(o => o.status)
                .WithMany()
                .HasForeignKey("_userStatusId");
            UserConfiguration.OwnsOne(o => o.address);
            UserConfiguration.HasMany<DepartInformation>(o => o._departsId)
             .IsRequired(false)
             // .HasForeignKey("BuyerId");
             .HasForeignKey(s=>s._departsId);
        }
    }
}
