﻿using Jsos3.Absences.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jsos3.Absences.ViewModels
{
    public class AbsenceIndexViewModel
    {
        public required List<AbsenceOfStudents> AbsenceOfStudents { get; set; }
        public required List<DateTime> Days { get; set; }
    }
}