﻿using Jsos3.Groups.Models;

namespace Jsos3.Groups.ViewModels.Models;

public class StudentIndexViewModel
{
    public required SemesterSummaryViewModel SemesterSummary { get; set; }
    public required List<string> Semesters { get; set; }
    public required List<StudentCourseDto> Courses { get; set; }
}
