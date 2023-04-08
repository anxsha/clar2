using clar2.Domain.Notes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace clar2.Infrastructure.Persistence.Configurations; 

public class LabelConfiguration : IEntityTypeConfiguration<Label> {
  public void Configure(EntityTypeBuilder<Label> builder) {
    builder.Property(l => l.Name)
      .IsRequired()
      .HasMaxLength(40);
  }
}
