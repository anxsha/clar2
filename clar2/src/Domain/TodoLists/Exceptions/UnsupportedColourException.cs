namespace clar2.Domain.TodoLists.Exceptions;

public class UnsupportedColourException : Exception {
  public UnsupportedColourException(string code)
    : base($"Colour \"{code}\" is unsupported.") { }
}
