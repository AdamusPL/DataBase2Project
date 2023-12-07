﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBTester.Db;

[Table("Semester")]
public class Semester
{
    [Key]
    public string Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
