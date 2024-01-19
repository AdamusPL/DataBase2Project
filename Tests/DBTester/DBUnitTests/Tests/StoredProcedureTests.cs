using Dapper;
using DataProducer.Generators;
using FluentAssertions;
using Models;
using Shared;
using System.Data;

namespace DBUnitTests.Tests
{
    [TestFixture]
    public class StoredProcedureTests
    {
        private int _userIdentity;
        private IDbConnection _unitOfWork;
        private IDbTransaction _transaction;

        [SetUp]
        public void Setup()
        {
            _unitOfWork = DBConnectionProvider.SuperAdminConnection();
            _unitOfWork.Open();
            _userIdentity = _unitOfWork.Query<int>("SELECT IDENT_CURRENT('[User]')").Single();
            _transaction = _unitOfWork.BeginTransaction();
        }

        [Test]
        public void RegisterStudent_ShouldAddUserAndStudentTable()
        {
            // Arrange
            var login = "Test";
            var passwordHash = "abab";
            var name = "Testtestcase";
            var surname = "Test";
            var fieldOfStudiesIdsJson = "[]";

            // Act
            _unitOfWork.Execute(@"
RegisterStudent @Login, @PasswordHash, @Name, @Surname, @FieldOfStudiesIdsJson
", new { login, passwordHash, name, surname, fieldOfStudiesIdsJson }, transaction: _transaction);

            // Assert
            var user = _unitOfWork.Query<User>(@"
SELECT TOP 1 *
FROM [User]
WHERE Name = @Name AND Surname = @Surname", new { name, surname }, transaction: _transaction).SingleOrDefault();

            user.Should().NotBeNull();

            var student = _unitOfWork.Query<Student>(@"
SELECT TOP 1 s.*
FROM [Student] s
INNER JOIN [User] u ON s.UserId = u.Id
WHERE u.Name = @Name AND u.Surname = @Surname", new { name, surname }, transaction: _transaction).SingleOrDefault();
            student.Should().NotBeNull();
            student?.UserId.Should().Be(user?.Id);
        }

        [Test]
        public void RegisterLecturer_ShouldAddUserAndLecturerAndEmailTable()
        {
            // Arrange
            var login = "Test";
            var passwordHash = "abab";
            var name = "Testtestcase";
            var surname = "Test";
            var emailsJson = "[\"email1\", \"email2\"]";

            var expectedEmails = new List<string> { "email1", "email2" };

            // Act
            _unitOfWork.Execute(@"
RegisterLecturer @Login, @PasswordHash, @Name, @Surname, @EmailsJson
", new { login, passwordHash, name, surname, emailsJson }, transaction: _transaction);

            // Assert
            var user = _unitOfWork.Query<User>(@"
SELECT TOP 1 *
FROM [User]
WHERE Name = @Name AND Surname = @Surname", new { name, surname }, transaction: _transaction).SingleOrDefault();

            user.Should().NotBeNull();

            var lecturer = _unitOfWork.Query<Lecturer>(@"
SELECT TOP 1 l.*
FROM [Lecturer] l
INNER JOIN [User] u ON l.UserId = u.Id
WHERE u.Name = @Name AND u.Surname = @Surname", new { name, surname }, transaction: _transaction).SingleOrDefault();
            lecturer.Should().NotBeNull();
            lecturer?.UserId.Should().Be(user?.Id);

            var emails = _unitOfWork.Query<string>(@"
SELECT Email
FROM [Email]
WHERE UserId = @UserId
", new { UserId = user?.Id }, transaction: _transaction).ToList();

            emails.Should().BeEquivalentTo(expectedEmails);
        }

        [Test]
        public void GetCourseFinalGrades_ShouleReturnGradesOfAllStudentsFromGroupsInSemester()
        {
            // Arrange
            var login = "Test";
            var passwordHash = "abab";
            var name = "Testtestcase";
            var surname = "Test";
            var fieldOfStudiesIdsJson = "[]";

            _unitOfWork.Execute(@"
RegisterStudent @Login, @PasswordHash, @Name, @Surname, @FieldOfStudiesIdsJson
", new { login, passwordHash, name, surname, fieldOfStudiesIdsJson }, transaction: _transaction);

            InsertGroup(out var courseId, out var groupId, out var semesterId, out var groupId2);
            var studentId = GetStudentId(login);

            var studentInGroupId = _unitOfWork.QuerySingle<int>(@"
INSERT INTO Student_Group (StudentId, GroupId, RegistrationDate)
VALUES (@StudentId, @GroupId, @RegistrationDate);
SELECT CAST(SCOPE_IDENTITY() as int);
", new { groupId, studentId, RegistrationDate = new DateTime(1990, 1, 1) }, transaction: _transaction);

            _unitOfWork.Execute(@"
INSERT INTO Grade (StudentInGroupId, Grade, IsFinal)
VALUES
(@StudentInGroupId, 5, 1),
(@StudentInGroupId, 4, 1),
(@StudentInGroupId, 4, 0)
", new { studentInGroupId }, transaction: _transaction);

            var expectedGrades = new List<CoureFinalGrade>
            {
                new()
                {
                    Id = studentId,
                    Name = name,
                    Surname = surname,
                    Grade = 5
                },
                new()
                {
                    Id = studentId,
                    Name = name,
                    Surname = surname,
                    Grade = 4
                }
            };

            // Act
            var grades = _unitOfWork.Query<CoureFinalGrade>("GetCourseFinalGrades",
                new { courseId, semesterId },
                commandType: CommandType.StoredProcedure,
                transaction: _transaction);

            // Assert
            grades.Should().BeEquivalentTo(expectedGrades);
        }

        [Test]
        public void GetGroupGrades_ShouleReturnGradesOfAllStudentsFromGroup()
        {
            // Arrange
            var login = "Test";
            var login2 = "Test2";
            var passwordHash = "abab";
            var name = "Testtestcase";
            var surname = "Test";
            var fieldOfStudiesIdsJson = "[]";

            _unitOfWork.Execute(@"
RegisterStudent @Login, @PasswordHash, @Name, @Surname, @FieldOfStudiesIdsJson
", new { login, passwordHash, name, surname, fieldOfStudiesIdsJson }, transaction: _transaction);

            _unitOfWork.Execute(@"
RegisterStudent @Login2, @PasswordHash, @Name, @Surname, @FieldOfStudiesIdsJson
", new { login2, passwordHash, name, surname, fieldOfStudiesIdsJson }, transaction: _transaction);

            InsertGroup(out var courseId, out var groupId, out var semesterId, out var groupId2);
            var studentId = GetStudentId(login);
            var studentId2 = GetStudentId(login2);

            var studentInGroupId = _unitOfWork.QuerySingle<int>(@"
INSERT INTO Student_Group (StudentId, GroupId, RegistrationDate)
VALUES (@StudentId, @GroupId, @RegistrationDate);
SELECT CAST(SCOPE_IDENTITY() as int);
", new { studentId, groupId, RegistrationDate = new DateTime(1990, 1, 1) }, transaction: _transaction);

            var studentInGroupId2 = _unitOfWork.QuerySingle<int>(@"
INSERT INTO Student_Group (StudentId, GroupId, RegistrationDate)
VALUES (@StudentId2, @GroupId, @RegistrationDate);
SELECT CAST(SCOPE_IDENTITY() as int);
", new { studentId2, groupId, RegistrationDate = new DateTime(1990, 1, 1) }, transaction: _transaction);

            _unitOfWork.Execute(@"
INSERT INTO Grade (StudentInGroupId, Grade, IsFinal)
VALUES
(@StudentInGroupId, 5, 1),
(@StudentInGroupId, 4, 0)
", new { studentInGroupId }, transaction: _transaction);

            _unitOfWork.Execute(@"
INSERT INTO Grade (StudentInGroupId, Grade, IsFinal)
VALUES
(@StudentInGroupId2, 2, 1),
(@StudentInGroupId2, 3, 0)
", new { studentInGroupId2 }, transaction: _transaction);

            var expectedGrades = new List<CoureFinalGrade>
            {
                new()
                {
                    Id = studentId,
                    Name = name,
                    Surname = surname,
                    Grade = 5
                },
                new()
                {
                    Id = studentId,
                    Name = name,
                    Surname = surname,
                    Grade = 4
                },
                new()
                {
                    Id = studentId2,
                    Name = name,
                    Surname = surname,
                    Grade = 2
                },
                new()
                {
                    Id = studentId2,
                    Name = name,
                    Surname = surname,
                    Grade = 3
                }
            };

            // Act
            var grades = _unitOfWork.Query<CoureFinalGrade>("GetGroupGrades",
                new { groupId },
                commandType: CommandType.StoredProcedure,
                transaction: _transaction);

            // Assert
            grades.Should().BeEquivalentTo(expectedGrades);
        }

        [Test]
        public void GetStudentGradesInGroup_ShouleReturnStudentGradesFromGroup()
        {
            // Arrange
            var login = "Test";
            var passwordHash = "abab";
            var name = "Testtestcase";
            var surname = "Test";
            var fieldOfStudiesIdsJson = "[]";

            _unitOfWork.Execute(@"
RegisterStudent @Login, @PasswordHash, @Name, @Surname, @FieldOfStudiesIdsJson
", new { login, passwordHash, name, surname, fieldOfStudiesIdsJson }, transaction: _transaction);

            InsertGroup(out var courseId, out var groupId, out var semesterId, out var groupId2);
            var studentId = GetStudentId(login);

            var studentInGroupId = _unitOfWork.QuerySingle<int>(@"
INSERT INTO Student_Group (StudentId, GroupId, RegistrationDate)
VALUES (@StudentId, @GroupId, @RegistrationDate);
SELECT CAST(SCOPE_IDENTITY() as int);
", new { groupId, studentId, RegistrationDate = new DateTime(1990, 1, 1) }, transaction: _transaction);

            _unitOfWork.Execute(@"
INSERT INTO Grade (StudentInGroupId, Grade, IsFinal)
VALUES
(@StudentInGroupId, 5, 1),
(@StudentInGroupId, 4, 1),
(@StudentInGroupId, 4, 0)
", new { studentInGroupId }, transaction: _transaction);

            List<int> expectedGrades = [5, 4, 4];

            // Act
            var grades = _unitOfWork.Query<int>("GetStudentGradesInGroup",
                new { groupId, studentId },
                commandType: CommandType.StoredProcedure,
                transaction: _transaction);

            // Assert
            grades.Should().BeEquivalentTo(expectedGrades);
        }

        [Test]
        public void GetStudentFinalGradesInSemester_ShouleReturnStudentFinalGradesByCourse()
        {
            // Arrange
            var login = "Test";
            var passwordHash = "abab";
            var name = "Maciej";
            var surname = "Padula";
            var fieldOfStudiesIdsJson = "[]";

            _unitOfWork.Execute(@"
RegisterStudent @Login, @PasswordHash, @Name, @Surname, @FieldOfStudiesIdsJson
", new { login, passwordHash, name, surname, fieldOfStudiesIdsJson }, transaction: _transaction);

            InsertGroup(out var courseId, out var groupId, out var semesterId, out var groupId2);
            var studentId = GetStudentId(login);

            var studentInGroupId = _unitOfWork.QuerySingle<int>(@"
INSERT INTO Student_Group (StudentId, GroupId, RegistrationDate)
VALUES (@StudentId, @GroupId, @RegistrationDate);
SELECT CAST(SCOPE_IDENTITY() as int);
", new { groupId, studentId, RegistrationDate = new DateTime(1990, 1, 1) }, transaction: _transaction);

            _unitOfWork.Execute(@"
INSERT INTO Grade (StudentInGroupId, Grade, IsFinal)
VALUES
(@StudentInGroupId, 5, 1),
(@StudentInGroupId, 4, 0)
", new { studentInGroupId }, transaction: _transaction);

            var expectedGrades = new List<StudentFinalGrades>
            {
                new()
                {
                    Course = "Introduction to Mathematics",
                    ECTS = 6,
                    Grade = 5
                }
            };

            // Act
            var grades = _unitOfWork.Query<StudentFinalGrades>("GetStudentFinalGradesInSemester",
                new { semesterId, studentId },
                commandType: CommandType.StoredProcedure,
                transaction: _transaction);

            // Assert
            grades.Should().BeEquivalentTo(expectedGrades);
        }

        [Test]
        public void GetStudentWeightedAverageGradeInSemester_ShouldCalculate()
        {
            // Arrange
            var login = "Test";
            var passwordHash = "abab";
            var name = "Maciej";
            var surname = "Padula";
            var fieldOfStudiesIdsJson = "[]";

            _unitOfWork.Execute(@"
RegisterStudent @Login, @PasswordHash, @Name, @Surname, @FieldOfStudiesIdsJson
", new { login, passwordHash, name, surname, fieldOfStudiesIdsJson }, transaction: _transaction);

            InsertGroup(out var courseId, out var groupId, out var semesterId, out var groupId2);
            var studentId = GetStudentId(login);

            var studentInGroupId = _unitOfWork.QuerySingle<int>(@"
INSERT INTO Student_Group (StudentId, GroupId, RegistrationDate)
VALUES (@StudentId, @GroupId, @RegistrationDate);
SELECT CAST(SCOPE_IDENTITY() as int);
", new { groupId, studentId, RegistrationDate = new DateTime(1990, 1, 1) }, transaction: _transaction);

            var studentInGroupId2 = _unitOfWork.QuerySingle<int>(@"
INSERT INTO Student_Group (StudentId, GroupId, RegistrationDate)
VALUES (@StudentId, @GroupId2, @RegistrationDate);
SELECT CAST(SCOPE_IDENTITY() as int);
", new { groupId2, studentId, RegistrationDate = new DateTime(1990, 1, 1) }, transaction: _transaction);

            _unitOfWork.Execute(@"
INSERT INTO Grade (StudentInGroupId, Grade, IsFinal)
VALUES
(@StudentInGroupId, 5, 1),
(@StudentInGroupId2, 4, 1)
", new { studentInGroupId, studentInGroupId2 }, transaction: _transaction);

            var expectedAverage = 4.5m;

            // Act
            var grade = _unitOfWork.ExecuteScalar<decimal>("GetStudentWeightedAverageGradeInSemester",
                new { semesterId, studentId },
                commandType: CommandType.StoredProcedure,
                transaction: _transaction);

            // Assert
            grade.Should().Be(expectedAverage);
        }

        [Test]
        public void GetStudentWeeklyPlan_ReturnOccurencesOfGroup()
        {
            // Arrange
            var login = "Test";
            var passwordHash = "abab";
            var name = "Maciej";
            var surname = "Padula";
            var fieldOfStudiesIdsJson = "[]";

            _unitOfWork.Execute(@"
RegisterStudent @Login, @PasswordHash, @Name, @Surname, @FieldOfStudiesIdsJson
", new { login, passwordHash, name, surname, fieldOfStudiesIdsJson }, transaction: _transaction);

            InsertGroup(out var courseId, out var groupId, out var semesterId, out var groupId2);
            var studentId = GetStudentId(login);

            _unitOfWork.Execute(@"
INSERT INTO Student_Group (StudentId, GroupId, RegistrationDate)
VALUES (@StudentId, @GroupId, @RegistrationDate)
", new { studentId, groupId, RegistrationDate = new DateTime(1990, 1, 1) }, transaction: _transaction);

            _unitOfWork.Execute(@"
INSERT INTO Student_Group (StudentId, GroupId, RegistrationDate)
VALUES (@StudentId, @GroupId2, @RegistrationDate)
", new { studentId, groupId2, RegistrationDate = new DateTime(1990, 1, 1) }, transaction: _transaction);

            var expectedTests = new List<PlanOccurence>
            {
                new()
                {
                    Date = new(2023, 12, 18),
                    StartTime = new(12, 0, 0),
                    Course = "Introduction to Mathematics",
                    Lecturer = "Jan Kowalski"
                },
                new()
                {
                    Date = new(2023, 12, 21),
                    StartTime = new(9, 0, 0),
                    Course = "Introduction to Mathematics",
                    Lecturer = "Jan Kowalski"
                }
            };

            // Act
            var plan = _unitOfWork.Query<PlanOccurence>("GetStudentWeeklyPlan",
                new { studentId, StartDate = new DateTime(2023, 12, 18), EndDate = new DateTime(2023, 12, 24) },
                commandType: CommandType.StoredProcedure,
                transaction: _transaction);

            // Assert
            plan.Should().BeEquivalentTo(expectedTests);
        }

        [TearDown]
        public void Teardown()
        {
            _transaction.Rollback();
            _transaction.Dispose();
            _unitOfWork.Execute("DBCC CHECKIDENT ('[User]', RESEED, @identity);", new { identity = _userIdentity });
            _unitOfWork.Dispose();
        }

        private void InsertGroup(out int courseId, out string groupId, out string semesterId, out string groupId2)
        {
            semesterId = Guid.NewGuid().ToString()[..20];
            groupId = Guid.NewGuid().ToString()[..20];
            groupId2 = Guid.NewGuid().ToString()[..20];
            var typeName = Guid.NewGuid().ToString()[..30];

            var lecturerUserId = _unitOfWork.Query<int>("INSERT INTO [dbo].[User] ([Name], [Surname]) VALUES ('Jan', 'Kowalski'); SELECT CAST(SCOPE_IDENTITY() as int);", transaction: _transaction).Single();

            var lecturerId = _unitOfWork.Query<int>("INSERT INTO [dbo].[Lecturer] ([UserId]) VALUES (@UserId); SELECT CAST(SCOPE_IDENTITY() as int);", new { UserId = lecturerUserId }, transaction: _transaction).Single();

            courseId = _unitOfWork.Query<int>("INSERT INTO [dbo].[Course] ([Name], [ECTS], [LecturerId]) VALUES ('Introduction to Mathematics', 6, @LecturerId); SELECT CAST(SCOPE_IDENTITY() as int);", new { LecturerId = lecturerId }, transaction: _transaction).Single();

            var typeId = _unitOfWork.Query<int>("INSERT INTO [dbo].[GroupType] ([Name]) VALUES (@TypeName); SELECT CAST(SCOPE_IDENTITY() as int);", new { TypeName = typeName }, transaction: _transaction).Single();

            _unitOfWork.Query("INSERT INTO Semester (Id, StartDate, EndDate) VALUES (@SemId, '2021-10-01', '2022-02-07');", new { @SemId = semesterId }, transaction: _transaction);

            _unitOfWork.Query("INSERT INTO [dbo].[Group] ([Id], [DayOfTheWeek], [StartTime], [EndTime], [Classroom], [Capacity], [RegularityId], [TypeId], [CourseId], [SemesterId]) VALUES (@GroupId, 5, '09:00:00', '11:00:00', 'Room 302', 25, @RegularityId, @TypeId, @CourseId, @SemId); SELECT CAST(SCOPE_IDENTITY() as int);", new { RegularityId = 3, TypeId = typeId, GroupId = groupId, SemId = semesterId, CourseId = courseId }, transaction: _transaction).Single();

            _unitOfWork.Query("INSERT INTO [dbo].[Group_Lecturer] ([GroupId], [LecturerId]) VALUES (@GroupId, @LecturerId)", new { GroupId = groupId, LecturerId = lecturerId }, transaction: _transaction);


            _unitOfWork.Query("INSERT INTO [dbo].[Group] ([Id], [DayOfTheWeek], [StartTime], [EndTime], [Classroom], [Capacity], [RegularityId], [TypeId], [CourseId], [SemesterId]) VALUES (@GroupId, 2, '12:00:00', '13:00:00', 'Room 302', 25, @RegularityId, @TypeId, @CourseId, @SemId); SELECT CAST(SCOPE_IDENTITY() as int);", new { RegularityId = 3, TypeId = typeId, GroupId = groupId2, SemId = semesterId, CourseId = courseId }, transaction: _transaction).Single();

            _unitOfWork.Query("INSERT INTO [dbo].[Group_Lecturer] ([GroupId], [LecturerId]) VALUES (@GroupId, @LecturerId)", new { GroupId = groupId2, LecturerId = lecturerId }, transaction: _transaction);
        }

        private int GetStudentId(string login)
        {
            var user = _unitOfWork.Query<User>(@"
SELECT TOP 1 *
FROM [User] u
INNER JOIN [UserLoginInformation] li ON u.Id = li.UserId
WHERE Login = @Login", new { login }, transaction: _transaction).SingleOrDefault();

            var student = _unitOfWork.Query<Student>(@"
SELECT TOP 1 s.*
FROM [Student] s
INNER JOIN [User] u ON s.UserId = u.Id
INNER JOIN [UserLoginInformation] li ON u.Id = li.UserId
WHERE Login = @Login", new { login }, transaction: _transaction).SingleOrDefault();


            return student.Id;
        }
    }
}
