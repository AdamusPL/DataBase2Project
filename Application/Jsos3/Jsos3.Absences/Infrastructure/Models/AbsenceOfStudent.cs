using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jsos3.Absences.Infrastructure.Models;

internal readonly record struct AbsenceOfStudent(

    string GroupId,
    int StudentId,
    string Name,
    string Surname,
    DateTime Date

);
