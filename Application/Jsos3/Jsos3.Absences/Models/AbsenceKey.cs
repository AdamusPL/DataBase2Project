using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jsos3.Absences.Models;

public readonly record struct AbsenceKey(int StudentId, DateTime Occurence);
