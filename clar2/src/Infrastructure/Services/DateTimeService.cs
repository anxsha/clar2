using clar2.Application.Common.Interfaces;

namespace clar2.Infrastructure.Services;

public class DateTimeService : IDateTime {
  public DateTime Now => DateTime.Now;
}