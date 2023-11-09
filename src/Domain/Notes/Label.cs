namespace neatbook.Domain.Notes; 

public class Label : ValueObject {
  public string Name { get; private set; }
  public Label(string name) {
    Name = name;
  }

  protected override IEnumerable<object> GetEqualityComponents() {
    yield return Name;
  }
}
