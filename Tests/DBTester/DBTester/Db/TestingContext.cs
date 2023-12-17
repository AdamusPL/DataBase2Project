﻿using Microsoft.EntityFrameworkCore;
using Models;

namespace DBTester.Db;

public class TestingContext : DbContext
{
    public TestingContext(DbContextOptions<TestingContext> opts)
        : base(opts)
    {
    }

    public DbSet<Semester> Semesters => Set<Semester>();
}
