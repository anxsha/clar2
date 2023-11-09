using neatbook.Domain.Notes;
using neatbook.Domain.Notes.Enums;

namespace neatbook.Application.Notes.Queries; 


public class AuthoredNoteBriefDto : IMapFrom<Note> {
  public string Title { get; set; }
  public string Content { get; set; }
  public NoteBackground Background { get; set; }
  public List<string> Labels { get; set; } = new();

  // public static AuthoredNoteBriefDto MapFromNote(Note note) {
  //   var dto = new AuthoredNoteBriefDto(note.Title, note.Content, note.Background) {
  //     Labels = note.Labels.Select(l => l.Name).ToList()
  //   };
  //   return dto;
  // }
  //
  // public AuthoredNoteBriefDto(string title, string content, NoteBackground background) {
  //   Title = title;
  //   Content = content;
  //   Background = background;
  // }

  public virtual void Mapping(Profile profile) {
    profile.CreateMap<Note, AuthoredNoteBriefDto>()
      .ForMember(d => d.Labels,
        opt => opt.MapFrom(
          s => s.Labels.Select(l => l.Name).ToList()));
  }
}
