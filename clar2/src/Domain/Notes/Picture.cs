namespace clar2.Domain.Notes; 

public class NotePicture : ValueObject {
  public required string Url { get; set; }
  protected override IEnumerable<object> GetEqualityComponents() {
    yield return Url;
  }
}
