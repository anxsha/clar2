using clar2.Domain.Common.Interfaces;

namespace clar2.Domain.Users;

public class User : BaseEntity, IAggregateRoot {
  public string Nickname { get; private set; }
  private string TestPassword { get; set; }
  public User(string nickname) {
    Nickname = nickname;
    TestPassword = "test";

  }
}
