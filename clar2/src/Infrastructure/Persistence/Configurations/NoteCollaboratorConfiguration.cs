using clar2.Domain.Notes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace clar2.Infrastructure.Persistence.Configurations; 

public class NoteCollaboratorConfiguration : IEntityTypeConfiguration<NoteCollaborator> {
  public void Configure(EntityTypeBuilder<NoteCollaborator> builder) {
    builder.Property(nc => nc.Permissions)
      .IsRequired();
  }
}
