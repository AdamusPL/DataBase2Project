namespace Jsos3.Absences.Helpers;

public class AbsencePageDto
{
    public required string GroupId { get; set; }
    public required int StudentId { get; set; }
    public required DateTime Date { get; set; }
    public required bool IsChecked { get; set; }
}
