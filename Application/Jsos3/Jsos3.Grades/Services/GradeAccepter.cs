using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jsos3.Grades.Repository;

namespace Jsos3.Grades.Services;

public interface IGradeAccepter
{
    Task Accept(int gradeId);
    Task Decline(int gradeId);
}
internal class GradeAccepter : IGradeAccepter
{
    private readonly IStudentGradeRepository _studentGradeRepository;

    public GradeAccepter(IStudentGradeRepository studentGradeRepository)
    {
        _studentGradeRepository = studentGradeRepository;
    }

    public async Task Accept(int gradeId)
    {
        await _studentGradeRepository.AcceptGrade(gradeId);
    }

    public async Task Decline(int gradeId)
    {
        await _studentGradeRepository.DeclineGrade(gradeId);
    }
}
