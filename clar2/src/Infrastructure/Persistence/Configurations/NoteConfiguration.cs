using clar2.Domain;
using clar2.Domain.Notes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace clar2.Infrastructure.Persistence.Configurations;

public class NoteConfiguration : IEntityTypeConfiguration<Note> {
  public void Configure(EntityTypeBuilder<Note> builder) {
    builder.Property(n => n.Title)
      .IsRequired()
      .HasMaxLength(200);
    builder.Property(n => n.Content)
      .IsRequired();
    builder.Property(n => n.Background)
      .IsRequired();
    builder.Property(n => n.IsArchived)
      .IsRequired()
      .HasDefaultValue(false);

    builder.OwnsMany(n => n.Labels)
      .Property(l => l.Name)
      .IsRequired()
      .HasMaxLength(40);
    builder.OwnsMany(n => n.Pictures);

    builder.HasOne<ApplicationUser>().WithMany().HasForeignKey(n => n.OwnerId);
  }
}
