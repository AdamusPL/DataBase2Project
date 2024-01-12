using Jsos3.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jsos3.Absences.Infrastructure.Models;

public readonly record struct GroupDate(
    string GroupId,
    DateTime Start,
    DateTime End,
    DayOfWeek DayOfTheWeek,
    Regularity Regularity
    );
