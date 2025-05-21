namespace Application.Common.Models.Scheduling;

public class PatientSchedulingsGroupedByDateDto
{
    public DateOnly Date { get; set; }

    public IEnumerable<PatientSchedulingDto> Schedulings { get; set; } = null!;
}

public class PatientSchedulingDto
{
    public string? DoctorName { get; set; } = null!;

    public TimeOnly Hour { get; set; }
}