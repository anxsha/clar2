using clar2.Domain.Common.Interfaces;

namespace clar2.Domain.Users;

public class User : BaseEntity, IAggregateRoot, IEquatable<User> {
  public required string Nickname { get; set; }

  public bool Equals(User? other) {
    if (other is null) {
      throw new ArgumentNullException(nameof(other));
    }

    return this.Nickname == other.Nickname;
  }
}
