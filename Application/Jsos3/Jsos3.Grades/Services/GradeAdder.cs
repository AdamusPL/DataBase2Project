using Jsos3.Grades.Helpers;
using Jsos3.Grades.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jsos3.Grades.Services;

public interface IGradeAdder
{
    Task AddGrade(NewGradeDto newGrade);
}
internal class GradeAdder : IGradeAdder
{
    ILecturerGradePerository _lecturerGradePerository;

    public GradeAdder(ILecturerGradePerository lecturerGradePerository)
    {
        _lecturerGradePerository = lecturerGradePerository;
    }

    public async Task AddGrade(NewGradeDto newGrade)
    {
        if (newGrade.Grade >= 2 && newGrade.Grade <= 5.5m && newGrade.Grade % 0.5m == 0)
        {
            await _lecturerGradePerository.AddGrade(newGrade);
        }
        else
        {
            throw new InvalidDataException();
        }
    }
}
