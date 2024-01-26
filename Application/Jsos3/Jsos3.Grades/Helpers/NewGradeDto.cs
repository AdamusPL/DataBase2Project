﻿namespace Jsos3.Grades.Helpers;

public class NewGradeDto
{
    public required string GroupId { get; set; }
    public required int StudentId { get; set; }
    public required string GradeText { get; set; }
    public required decimal Grade { get; set; }
    public required bool IsFinal { get; set; }
}
