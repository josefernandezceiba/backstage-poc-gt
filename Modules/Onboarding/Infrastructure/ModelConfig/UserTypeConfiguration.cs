using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MoMo.Modules.Onboarding.Core;

namespace MoMo.Modules.Onboarding.Infrastructure.ModelConfig
{
    public class UserTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(typeof(User).Name, "onboarding");
            builder.HasIndex(e => e.Id).IsClustered(false);            
        }
    }
}
