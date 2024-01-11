using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jsos3.Absences.Infrastructure.Models;

public readonly record struct StudentInGroup(
    int StudentInGroupId,
    string GroupId,
    int StudentId,
    string Name,
    string Surname
    );

