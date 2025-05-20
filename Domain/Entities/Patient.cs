using Domain.Common;

namespace Domain.Entities;

public class Patient : BaseAuditableEntity
{
    public string Name { get; set; } = null!;
    public string ApplicationUserId { get; set; } = null!;

    public double Weight { get; set; }
}