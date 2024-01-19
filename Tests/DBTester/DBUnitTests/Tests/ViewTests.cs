using Dapper;
using FluentAssertions;
using Models;
using Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBUnitTests.Tests
{
    [TestFixture]
    public class ViewTests
    {
        private IDbConnection _unitOfWork;
        private IDbTransaction _transaction;

        [SetUp]
        public void Setup()
        {
            _unitOfWork = DBConnectionProvider.SuperAdminConnection();
            _unitOfWork.Open();
            _transaction = _unitOfWork.BeginTransaction();
        }

        [Test]
        public void GroupInfo_Should_ReturnCorrectGroupInfo()
        {
            var semesterId = Guid.NewGuid().ToString()[..20];
            var groupId = Guid.NewGuid().ToString()[..20];
            var typeName = Guid.NewGuid().ToString()[..30];
            var regularityName = Guid.NewGuid().ToString()[..10];

            var lecturerId = InsertRequiredDataForGroupInfo(semesterId, groupId, typeName, regularityName);

            var expectedGroupInfo = new GroupInfo()
            {
                DayOfTheWeek = 3,
                StartTime = new TimeSpan(9, 0, 0),
                EndTime = new TimeSpan(11, 0, 0),
                GroupId = groupId,
                Classroom = "Room 302",
                Regularity = regularityName,
                GroupType = typeName,
                LecturerSurname = "Kowalski",
                LecturerName = "Jan",
                LecturerId = lecturerId,
                CourseName = "Introduction to Mathematics",
                ECTS = 6
            };  

            var groupInfo = _unitOfWork.Query<GroupInfo>("SELECT * FROM GroupInfo WHERE GroupId = @GroupId", new { GroupId = groupId },  transaction: _transaction).First();

            groupInfo.Should().BeEquivalentTo(expectedGroupInfo);
        }

        [Test]
        public void UserContact_Should_ReturnCorrectUserContact()
        {
            var userId = InsertRequiredDataForUserContact();

            var expectedUserContact = new UserContact()
            {
                UserId = userId,
                Name = "Jan",
                Surname = "Kowalski",
                Email = "jan@buziaczek.pl",
                Phone = "123456789123"
            };

            var userContact = _unitOfWork.Query<UserContact>("SELECT * FROM UserContact WHERE UserId = @UserId", new { UserId = userId }, transaction: _transaction).First();

            userContact.Should().BeEquivalentTo(expectedUserContact);
        }

        [Test]
        public void CourseInfo_Should_ReturnCorrectCourseInfo()
        {
            var courseId = InsertRequiredDataForCourseInfo();

            var expectedCourseInfo = new CourseInfo()
            {
                CourseId = courseId,
                CourseName = "Introduction to Mathematics",
                ECTS = 6,
                LecturerName = "Jan",
                LecturerSurname = "Kowalski"
            };

            var courseInfo = _unitOfWork.Query<CourseInfo>("SELECT * FROM CourseInfo WHERE CourseId = @CourseId", new { CourseId = courseId }, transaction: _transaction).First();

            courseInfo.Should().BeEquivalentTo(expectedCourseInfo);
        }

        private int InsertRequiredDataForGroupInfo(string semesterId, string groupId, string typeName, string regularityName)
        {

            var lecturerUserId = _unitOfWork.Query<int>("INSERT INTO [dbo].[User] ([Name], [Surname]) VALUES ('Jan', 'Kowalski'); SELECT CAST(SCOPE_IDENTITY() as int);", transaction: _transaction).Single();

            var lecturerId = _unitOfWork.Query<int>("INSERT INTO [dbo].[Lecturer] ([UserId]) VALUES (@UserId); SELECT CAST(SCOPE_IDENTITY() as int);", new { UserId = lecturerUserId }, transaction: _transaction).Single();

            var courseId = _unitOfWork.Query<int>("INSERT INTO [dbo].[Course] ([Name], [ECTS], [LecturerId]) VALUES ('Introduction to Mathematics', 6, @LecturerId); SELECT CAST(SCOPE_IDENTITY() as int);", new { LecturerId = lecturerId }, transaction: _transaction).Single();

            var typeId = _unitOfWork.Query<int>("INSERT INTO [dbo].[GroupType] ([Name]) VALUES (@TypeName); SELECT CAST(SCOPE_IDENTITY() as int);", new { TypeName = typeName }, transaction: _transaction).Single();

            var regularityId = _unitOfWork.Query<int>("INSERT INTO [dbo].[Regularity] ([Name]) VALUES (@RegularityName); SELECT CAST(SCOPE_IDENTITY() as int);", new { RegularityName = regularityName }, transaction: _transaction).Single();

            _unitOfWork.Query("INSERT INTO Semester (Id, StartDate, EndDate) VALUES (@SemId, '2021-10-01', '2022-02-07');", new { @SemId = semesterId }, transaction: _transaction);

            _unitOfWork.Query("INSERT INTO [dbo].[Group] ([Id], [DayOfTheWeek], [StartTime], [EndTime], [Classroom], [Capacity], [RegularityId], [TypeId], [CourseId], [SemesterId]) VALUES (@GroupId, 3, '09:00:00', '11:00:00', 'Room 302', 25, @RegularityId, @TypeId, @CourseId, @SemId); SELECT CAST(SCOPE_IDENTITY() as int);", new { RegularityId = regularityId, TypeId = typeId, GroupId = groupId, SemId = semesterId, CourseId = courseId }, transaction: _transaction).Single();

            _unitOfWork.Query("INSERT INTO [dbo].[Group_Lecturer] ([GroupId], [LecturerId]) VALUES (@GroupId, @LecturerId)", new { GroupId = groupId, LecturerId = lecturerId }, transaction: _transaction);

            return lecturerId;
        }

        private int InsertRequiredDataForUserContact() 
        {
            var lecturerUserId = _unitOfWork.Query<int>("INSERT INTO [dbo].[User] ([Name], [Surname]) VALUES ('Jan', 'Kowalski'); SELECT CAST(SCOPE_IDENTITY() as int);", transaction: _transaction).Single();

            _unitOfWork.Query("INSERT INTO [dbo].[Lecturer] ([UserId]) VALUES (@UserId);", new { UserId = lecturerUserId }, transaction: _transaction);

            _unitOfWork.Query("INSERT INTO [dbo].[Email] (UserId, Email) VALUES (@UserId, 'jan@buziaczek.pl')", new { UserId = lecturerUserId }, transaction: _transaction);
            _unitOfWork.Query("INSERT INTO [dbo].[WorkPhone] (UserId, Phone) VALUES (@UserId, '123456789123')", new { UserId = lecturerUserId }, transaction: _transaction);

            return lecturerUserId;
        }

        private int InsertRequiredDataForCourseInfo() 
        {
            var lecturerUserId = _unitOfWork.Query<int>("INSERT INTO [dbo].[User] ([Name], [Surname]) VALUES ('Jan', 'Kowalski'); SELECT CAST(SCOPE_IDENTITY() as int);", transaction: _transaction).Single();

            var lecturerId = _unitOfWork.Query<int>("INSERT INTO [dbo].[Lecturer] ([UserId]) VALUES (@UserId); SELECT CAST(SCOPE_IDENTITY() as int);", new { UserId = lecturerUserId }, transaction: _transaction).Single();

            var courseId = _unitOfWork.Query<int>("INSERT INTO [dbo].[Course] ([Name], [ECTS], [LecturerId]) VALUES ('Introduction to Mathematics', 6, @LecturerId); SELECT CAST(SCOPE_IDENTITY() as int);", new { LecturerId = lecturerId }, transaction: _transaction).Single();

            return courseId;
        }

        [TearDown]
        public void TearDown()
        {
            _transaction.Rollback();
            _unitOfWork.Dispose();
        }
    }
}
