using Domain.Common;

namespace Domain.Entities;

public class Doctor : BaseAuditableEntity
{
    public string ApplicationUserId { get; set; } = null!;
    public string? Name { get; set; }
    public string? Crm { get; set; }

    ICollection<AvailableTime> AvailableTimes { get; set; } = new List<AvailableTime>();
}