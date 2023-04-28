namespace clar2.Domain.Notes; 

public class NotePicture : ValueObject {
  public string Url { get; private set; }

  public NotePicture(string url) {
    Url = url;
  }
  protected override IEnumerable<object> GetEqualityComponents() {
    yield return Url;
  }
}
