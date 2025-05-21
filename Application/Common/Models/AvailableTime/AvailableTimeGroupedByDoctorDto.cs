namespace Application.Common.Models.AvailableTime;

public class AvailableTimeGroupedByDoctorDto
{
    public int DoctorId { get; init; }

    public string? DoctorName { get; init; } = null!;

    public ICollection<AvailableDayDto> AvailableTimes { get; init; } =
        new List<AvailableDayDto>();
}

public class AvailableDayDto
{
    public DateOnly Date { get; init; }
    public IEnumerable<TimeOnly> Times { get; init; } = new List<TimeOnly>();
}