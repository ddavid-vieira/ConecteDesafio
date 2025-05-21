namespace Application.Common.Models.Scheduling;

public class DoctorSchedulingsGroupedByDateDto
{
    public DateOnly Date { get; set; }

    public IEnumerable<DoctorSchedulingDto> Schedulings { get; set; } = null!;
}

public class DoctorSchedulingDto
{
    public string? PatientName { get; set; } = null!;

    public double Weight { get; set; }

    public TimeOnly Hour { get; set; }
}