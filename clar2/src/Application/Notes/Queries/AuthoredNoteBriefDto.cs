using AutoMapper;
using clar2.Application.Common.Mappings;
using clar2.Domain.Notes;
using clar2.Domain.Notes.Enums;

namespace clar2.Application.Notes.Queries;

public class AuthoredNoteBriefDto : IMapFrom<Note> {
  public string Title { get; set; }
  public string Content { get; set; }
  public NoteBackground Background { get; set; }
  public List<string> Labels { get; set; } = new();

  public virtual void Mapping(Profile profile) {
    profile.CreateMap<Note, AuthoredNoteBriefDto>()
      .ForMember(d => d.Labels,
        opt => opt.MapFrom(
          s => s.Labels.Select(l => l.Name).ToList()));
  }
}
