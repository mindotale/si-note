using SiNote.Application.Common.Interfaces;

namespace SiNote.Infrastructure.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
