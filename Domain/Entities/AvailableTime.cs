using Domain.Common;

namespace Domain.Entities;

public class AvailableTime : BaseAuditableEntity
{
    private DateTime _hour;

    public int DoctorId { get; set; }

    public Doctor Doctor { get; set; } = null!;

    public DateTime Hour
    {
        get => _hour;
        set => _hour = new DateTime(value.Year, value.Month, value.Day, value.Hour, value.Minute, 0);
    }

    public Scheduling? Schedule { get; set; }
}