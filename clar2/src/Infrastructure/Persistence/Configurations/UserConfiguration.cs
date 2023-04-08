using clar2.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace clar2.Infrastructure.Persistence.Configurations; 

public class UserConfiguration : IEntityTypeConfiguration<User> {
  public void Configure(EntityTypeBuilder<User> builder) {
    builder.Property(u => u.Nickname)
      .IsRequired()
      .HasMaxLength(100);
  }
}
