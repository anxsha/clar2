using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using neatbook.Domain.Notes;

namespace neatbook.Infrastructure.Data.Configurations; 

public class NoteCollaboratorConfiguration : IEntityTypeConfiguration<NoteCollaborator> {
  public void Configure(EntityTypeBuilder<NoteCollaborator> builder) {
    builder.Property(nc => nc.Permissions)
      .IsRequired();
  }
}

