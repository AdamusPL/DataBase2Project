using Models;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Dapper;

namespace DataProducer.Generators
{


    public class GroupGenerator : IEntityGenerator
    {
        private readonly Dictionary<int, Tuple<int, int>> _groupTypeCapacityRanges = new()
        {
            { 1, new Tuple<int, int>(130, 201) },
            { 2, new Tuple<int, int>(8, 13) },
            { 3, new Tuple<int, int>(15, 26) },
            { 4, new Tuple<int, int>(12, 19) }
        };

        private readonly Dictionary<int, string> _groupTypeRoomNames = new()
        {
            { 1, "W" },
            { 2, "L" },
            { 3, "C" },
            { 4, "P" }
        };

        public void Generate()
        {
            using var connection = DBConnectionProvider.SuperAdminConnection();

            var courseList = connection.Query<Course>("SELECT * FROM Course WITH(NOLOCK)").ToList();
            var studentList = connection.Query<Student>("SELECT * FROM Student WITH(NOLOCK)").ToList();
            var lecturerList = connection.Query<Lecturer>("SELECT * FROM Lecturer WITH(NOLOCK)").ToList();
            var semesterList = connection.Query<Semester>("SELECT * FROM Semester WITH(NOLOCK)").ToList();

            var random = new Random();


            if (!semesterList.Any()) return;
            if (!courseList.Any()) return;

            connection.Open();
            var transaction = connection.BeginTransaction();

            semesterList.ForEach(semester =>
            {
                int groupAmount = 0;
                courseList.ForEach(course =>
                {
                    for (int type = 1; type < 5; type++)
                    {
                        if (type > 1 && random.NextDouble() > 0.33) continue;

                        var startTime = TimeSpan.FromHours(random.Next(8, 21));
                        var endTime = TimeSpan.FromHours(random.Next(8, 21));

                        if (startTime > endTime)
                        {
                            (endTime, startTime) = (startTime, endTime);
                        }

                        var group = new Group
                        {
                            Id = Guid.NewGuid().ToString()[..20],
                            DayOfTheWeek = random.Next(1, 6),
                            StartTime = startTime,
                            EndTime = endTime,
                            Classroom = $"{_groupTypeRoomNames[type]}{random.Next(1, 100)}",
                            Capacity = random.Next(_groupTypeCapacityRanges[type].Item1, _groupTypeCapacityRanges[type].Item2),
                            RegularityId = random.Next(1, 4),
                            TypeId = type,
                            CourseId = course.Id,
                            SemesterId = semester.Id
                        };

                        connection.Execute("INSERT INTO [dbo].[Group] (Id, DayOfTheWeek, StartTime, EndTime, Classroom, Capacity, RegularityId, TypeId, CourseId, SemesterId) VALUES (@Id, @DayOfTheWeek, @StartTime, @EndTime, @Classroom, @Capacity, @RegularityId, @TypeId, @CourseId, @SemesterId)", group, transaction);
                        groupAmount++;

                        var groupLecturer = new
                        {
                            GroupId = group.Id,
                            LecturerId = lecturerList[random.Next(0, lecturerList.Count)].Id
                        };

                        connection.Execute("INSERT INTO [dbo].[Group_Lecturer] (GroupId, LecturerId) VALUES (@GroupId, @LecturerId)", groupLecturer, transaction);

                        var availableStudents = studentList.ToList();

                        for (int studentCount = 0; studentCount < group.Capacity * random.Next(80, 101) / 100; studentCount++)
                        {
                            if (!availableStudents.Any()) break;

                            var student = availableStudents[random.Next(0, availableStudents.Count)];

                            var year = random.Next(2010, 2021);
                            var month = random.Next(1, 13); // Month is 1-12
                            var day = random.Next(1, DateTime.DaysInMonth(year, month) + 1); // Day depends on the month and year

                            var groupStudent = new
                            {
                                GroupId = group.Id,
                                StudentId = student.Id,
                                RegistrationDate = new DateTime(year, month, day)
                            };

                            connection.Execute("INSERT INTO [dbo].[Student_Group] (GroupId, StudentId, RegistrationDate) VALUES (@GroupId, @StudentId, @RegistrationDate)", groupStudent, transaction);

                            availableStudents.Remove(student);
                        }
                    }
                });

                Console.WriteLine($"Added {groupAmount} groups for semester {semester.Id}");

            });

            Console.WriteLine($"Added groups for {courseList.Count} courses in {semesterList.Count} semesters");

            transaction.Commit();
            connection.Dispose();
        }
    }
}
