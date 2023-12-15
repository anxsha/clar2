using neatbook.Application.Common.Interfaces;
using neatbook.Application.Common.Models;
using neatbook.Application.Notes.Commands.AddNotePicture;
using neatbook.Application.Notes.Commands.ArchiveNote;
using neatbook.Application.Notes.Commands.CreateNote;
using neatbook.Application.Notes.Commands.DeleteNote;
using neatbook.Application.Notes.Commands.EditNote;
using neatbook.Application.Notes.Queries;
using neatbook.Application.Notes.Queries.GetArchivedAuthoredNotesWithPagination;
using neatbook.Application.Notes.Queries.GetCollaboratedNotesWithPagination;
using neatbook.Application.Notes.Queries.GetUnarchivedAuthoredNotesWithPagination;
using neatbook.Domain.Notes.Enums;
using neatbook.Web.Endpoints.RequestValidators;
using neatbook.Web.Services;

namespace neatbook.Web.Endpoints;

public class Notes : EndpointGroupBase {
  public override void Map(WebApplication app) {
    app.MapGroup(this)
      .RequireAuthorization()
      .MapGet(GetUnarchivedAuthoredNotes)
      .MapGet(GetArchivedAuthoredNotes, "archived")
      .MapGet(GetCollaboratedNotes, "collaborated")
      .MapPut(ArchiveNote, "{id}/archive")
      .MapPost(CreateNote)
      .MapPost(AddNotePicture, "{id}/pictures", true)
      .MapDelete(DeleteNote, "{id}")
      .MapPut(EditNote, "{id}");
  }

  private const int MaxNotePictureSize = 1024 * 1024 * 5; // 5MB
  private const string NotePicturesDirectory = "Files/uploads/note-pictures"; // relative to ContentRootPath

  public async Task<PaginatedList<AuthoredNoteBriefDto>> GetUnarchivedAuthoredNotes(ISender sender, IUser user,
    [AsParameters] RequestWithPagination req) {
    return await sender.Send(
      new GetUnarchivedAuthoredNotesWithPaginationQuery(user.Id!, req.PageNumber, req.PageSize));
  }

  public async Task<PaginatedList<AuthoredNoteBriefDto>> GetArchivedAuthoredNotes(ISender sender, IUser user,
    [AsParameters] RequestWithPagination req) {
    return await sender.Send(
      new GetArchivedAuthoredNotesWithPaginationQuery(user.Id!, req.PageNumber, req.PageSize));
  }

  public async Task<PaginatedList<CollaboratedNoteBriefDto>> GetCollaboratedNotes(ISender sender, IUser user,
    [AsParameters] RequestWithPagination req) {
    return await sender.Send(
      new GetCollaboratedNotesWithPaginationQuery(user.Id!, req.PageNumber, req.PageSize));
  }

  public async Task<IResult> ArchiveNote(ISender sender, IUser user, int id) {
    await sender.Send(new ArchiveNoteCommand(id, user.Id!));
    return Results.NoContent();
  }

  public async Task<IResult> CreateNote(ISender sender, IUser user, CreateNoteRequest req) {
    NoteBackground background = req.Background.ToLower() switch {
      "red" => NoteBackground.Red,
      "blue" => NoteBackground.Blue,
      "green" => NoteBackground.Green,
      "island" => NoteBackground.IslandImage,
      _ => NoteBackground.Default
    };

    var id = await sender.Send(new CreateNoteCommand(req.Title, req.Content, user.Id!, background));
    return Results.Ok(new {id});
  }

  public async Task<IResult> DeleteNote(ISender sender, IUser user, int id) {
    await sender.Send(new DeleteNoteCommand(id, user.Id!));
    return Results.NoContent();
  }

  public async Task<IResult> EditNote(ISender sender, IUser user, int id, EditNoteRequest req) {
    await sender.Send(new EditNoteCommand(id, req.Title, req.Content, user.Id!));
    return Results.NoContent();
  }

  public async Task<IResult> AddNotePicture(ISender sender, IWebHostEnvironment environment, IUser user, int id,
    IFormFile file) {
    // check file's size and extension
    if (!file.IsValidImage(MaxNotePictureSize)) {
      return Results.BadRequest(
        $"File is not a valid image or is too large. Supported formats: gif, png, jpeg, jpg. " +
        $"Maximum file size: {MaxNotePictureSize / 1024.0 / 1024.0} MB.");
    }

    var extension = Path.GetExtension(file.FileName);
    // generate unique filename
    var fileName = $"{Guid.NewGuid().ToString()}{extension}";
    var filePath = Path.Combine(environment.ContentRootPath, NotePicturesDirectory, fileName);

    var pictureAdded =
      await sender.Send(new AddNotePictureCommand(id, $"{NotePicturesDirectory}/{fileName}", user.Id!));

    if (!pictureAdded) {
      return Results.BadRequest();
    }

    // save file to disk
    await using (var fileStream = new FileStream(filePath, FileMode.Create)) {
      await file.CopyToAsync(fileStream);
    }

    return Results.NoContent();
  }


  public record struct RequestWithPagination(int PageNumber = 1, int PageSize = 10);

  public record struct CreateNoteRequest(string Title, string Content, string Background);

  public record struct EditNoteRequest(string Title, string Content);
}
