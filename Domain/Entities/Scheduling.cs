using Domain.Common;

namespace Domain.Entities;

public class Scheduling : BaseAuditableEntity
{
    public int PatientId { get; set; }

    public Patient Patient { get; set; } = null!;

    public int AvailableTimeId { get; set; }

    public AvailableTime AvailableTime { get; set; } = null!;
}