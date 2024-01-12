namespace Jsos3.Grades.TrashCan;

public class StudentGrade
{
    public required int Id {  get; set; }
    public required string Text { get; set; }
    public required decimal Grade { get; set; }
    public required bool IsFinal { get; set; }
    public required bool Accepted { get; set; }

}
