using System.Data;
using Dapper;
using FluentAssertions;
using Microsoft.Data.SqlClient;
using Models;
using Shared;

namespace DBUnitTests.Tests
{
    [TestFixture]
    public class PermissionTests
    {
        private IDbConnection _unitOfWork;
        private IDbTransaction _transaction;

        private static readonly List<string> TableNames =
        [
            "Absence",
            "Course",
            "Email",
            "Faculty",
            "FieldOfStudy",
            "FieldOfStudy_Course",
            "Grade",
            "[Group]",
            "GroupType",
            "Group_Lecturer",
            "Lecturer",
            "Regularity",
            "Semester",
            "Student",
            "Student_FieldOfStudy",
            "Student_Group",
            "[User]",
            "UserLoginInformation",
            "WorkPhone",
            "Administrator"
        ];

        private static readonly List<string> TablesWithoutInsertPermissionsForLecturer = TableNames.Except(new List<string> { "Grade", "Absence" }).ToList();

        private static readonly List<string> TablesWithoutUpdatePermissionsForLecturer = TableNames.Except(new List<string> { "UserLoginInformation" }).ToList();

        private static readonly List<string> TablesWithoutUpdatePermissionsForStudent = TableNames.Except(new List<string> { "Grade", "UserLoginInformation" }).ToList();

        private static readonly List<string> Views =
            [
                "GroupInfo",
                "UserContact",
                "CourseInfo"
            ];

        private static readonly List<Func<IDbConnection>> UserConnections =
        [
            DBConnectionProvider.AdministrationConnection,
            DBConnectionProvider.StudentConnection,
            DBConnectionProvider.LecturerConnection
        ];

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void AllUsers_Should_HaveSelectPermissions_OnAllTables([ValueSource(nameof(TableNames))] string table, [ValueSource(nameof(UserConnections))] Func<IDbConnection> getUserConnection)
        {
            //Arrange
            _unitOfWork = getUserConnection();
            _unitOfWork.Open();
            _transaction = _unitOfWork.BeginTransaction();

            //Act
            var act = () => _unitOfWork.Query($"SELECT TOP 10 * FROM {table}", transaction: _transaction);

            //Assert
            act.Should().NotThrow();

            //Teardown
            _transaction.Rollback();
            _unitOfWork.Dispose();
        }

        [Test]
        public void AllUsers_Should_HaveSelectPermissions_OnAllViews([ValueSource(nameof(Views))] string view, [ValueSource(nameof(UserConnections))] Func<IDbConnection> getUserConnection)
        {
            //Arrange
            _unitOfWork = DBConnectionProvider.AdministrationConnection();
            _unitOfWork.Open();

            //Act
            var hasPermission = _unitOfWork.Query<bool>($"SELECT HAS_PERMS_BY_NAME(@ViewName, 'OBJECT', 'SELECT')",
                new { ViewName = view },
                transaction: _transaction).Single();

            //Assert
            hasPermission.Should().BeTrue();
        }

        [Test]
        public void Administration_Should_HaveDeletePermissions_OnAllTables([ValueSource(nameof(TableNames))] string table)
        {
            //Arrange
            _unitOfWork = DBConnectionProvider.AdministrationConnection();
            _unitOfWork.Open();

            //Act
            var hasPermission = _unitOfWork.Query<bool>($"SELECT HAS_PERMS_BY_NAME(@TableName, 'OBJECT', 'DELETE')",
                new { TableName = table },
                transaction: _transaction).Single();

            //Assert
            hasPermission.Should().BeTrue();
        }

        [Test]
        public void Student_ShouldNot_HaveDeletePermissions_OnAnyTables([ValueSource(nameof(TableNames))] string table)
        {
            //Arrange
            _unitOfWork = DBConnectionProvider.StudentConnection();
            _unitOfWork.Open();
            _transaction = _unitOfWork.BeginTransaction();

            //Act
            var act = () => _unitOfWork.Query($"DELETE FROM {table}", transaction: _transaction);

            //Assert
            act.Should().Throw<SqlException>();

            //Teardown
            _transaction.Rollback();
            _unitOfWork.Dispose();
        }

        [Test]
        public void Lecturer_ShouldNot_HaveDeletePermissions_OnAnyTables([ValueSource(nameof(TableNames))] string table)
        {
            //Arrange
            _unitOfWork = DBConnectionProvider.LecturerConnection();
            _unitOfWork.Open();
            _transaction = _unitOfWork.BeginTransaction();

            //Act
            var act = () => _unitOfWork.Query($"DELETE FROM {table}", transaction: _transaction);

            //Assert
            act.Should().Throw<SqlException>();

            //Teardown
            _transaction.Rollback();
            _unitOfWork.Dispose();
        }

        [Test]
        public void Administration_Should_HaveInsertPermissions_OnAllTables([ValueSource(nameof(TableNames))] string table)
        {
            //Arrange
            _unitOfWork = DBConnectionProvider.AdministrationConnection();
            _unitOfWork.Open();

            //Act
            var hasPermission = _unitOfWork.Query<bool>($"SELECT HAS_PERMS_BY_NAME(@TableName, 'OBJECT', 'INSERT')",
                new { TableName = table },
                transaction: _transaction).Single();

            //Assert
            hasPermission.Should().BeTrue();
        }

        [Test]
        public void Student_ShouldNot_HaveInsertPermissions_OnAnyTables([ValueSource(nameof(TableNames))] string table)
        {
            //Arrange
            _unitOfWork = DBConnectionProvider.StudentConnection();
            _unitOfWork.Open();

            //Act
            var hasPermission = _unitOfWork.Query<bool>($"SELECT HAS_PERMS_BY_NAME(@TableName, 'OBJECT', 'INSERT')",
                new { TableName = table },
                transaction: _transaction).Single();

            //Assert
            hasPermission.Should().BeFalse();
        }

        [Test]
        public void Lecturer_ShouldNot_HaveInsertPermissions_OnSpecifiedTables([ValueSource(nameof(TablesWithoutInsertPermissionsForLecturer))] string table)
        {
            //Arrange
            _unitOfWork = DBConnectionProvider.LecturerConnection();
            _unitOfWork.Open();

            //Act
            var hasPermission = _unitOfWork.Query<bool>($"SELECT HAS_PERMS_BY_NAME(@TableName, 'OBJECT', 'INSERT')",
                new { TableName = table },
                transaction: _transaction).Single();

            //Assert
            hasPermission.Should().BeFalse();
        }

        [Test]
        public void Administration_Should_HaveUpdatePermissions_OnAllTables([ValueSource(nameof(TableNames))] string table)
        {
            //Arrange
            _unitOfWork = DBConnectionProvider.AdministrationConnection();
            _unitOfWork.Open();

            //Act
            var hasPermission = _unitOfWork.Query<bool>($"SELECT HAS_PERMS_BY_NAME(@TableName, 'OBJECT', 'UPDATE')",
                new { TableName = table },
                transaction: _transaction).Single();

            //Assert
            hasPermission.Should().BeTrue();
        }

        [Test]
        public void Student_ShouldNot_HaveUpdatePermissions_OnSpecifiedTables([ValueSource(nameof(TablesWithoutUpdatePermissionsForStudent))] string table)
        {
            //Arrange
            _unitOfWork = DBConnectionProvider.StudentConnection();
            _unitOfWork.Open();

            //Act
            var hasPermission = _unitOfWork.Query<bool>($"SELECT HAS_PERMS_BY_NAME(@TableName, 'OBJECT', 'UPDATE')",
                new { TableName = table },
                transaction: _transaction).Single();

            //Assert
            hasPermission.Should().BeFalse();
        }

        [Test]
        public void Lecturer_ShouldNot_HaveUpdatePermissions_OnSpecifiedTables([ValueSource(nameof(TablesWithoutUpdatePermissionsForLecturer))] string table)
        {
            //Arrange
            _unitOfWork = DBConnectionProvider.LecturerConnection();
            _unitOfWork.Open();

            //Act
            var hasPermission = _unitOfWork.Query<bool>($"SELECT HAS_PERMS_BY_NAME(@TableName, 'OBJECT', 'UPDATE')",
                new { TableName = table },
                transaction: _transaction).Single();

            //Assert
            hasPermission.Should().BeFalse();
        }

        [Test]
        [TestCase("Grade")]
        [TestCase("UserLoginInformation")]
        public void Student_Should_HaveUpdatePermissions_OnSpecifiedTables(string table)
        {
            //Arrange
            _unitOfWork = DBConnectionProvider.StudentConnection();
            _unitOfWork.Open();

            //Act
            var hasPermission = _unitOfWork.Query<bool>($"SELECT HAS_PERMS_BY_NAME(@TableName, 'OBJECT', 'UPDATE')",
                new { TableName = table },
                transaction: _transaction).Single();

            //Assert
            hasPermission.Should().BeTrue();
        }

        [Test]
        [TestCase("Grade")]
        [TestCase("Absence")]
        public void Lecturer_Should_HaveInsertPermissions_OnSpecifiedTables(string table)
        {
            //Arrange
            _unitOfWork = DBConnectionProvider.LecturerConnection();
            _unitOfWork.Open();

            //Act
            var hasPermission = _unitOfWork.Query<bool>($"SELECT HAS_PERMS_BY_NAME(@TableName, 'OBJECT', 'INSERT')",
                new { TableName = table },
                transaction: _transaction).Single();

            //Assert
            hasPermission.Should().BeTrue();
        }

        [Test]
        public void Lecturer_Should_HaveUpdatePermissions_OnLoginInformation()
        {
            //Arrange
            _unitOfWork = DBConnectionProvider.LecturerConnection();
            _unitOfWork.Open();

            //Act
            var hasPermission = _unitOfWork.Query<bool>($"SELECT HAS_PERMS_BY_NAME('UserLoginInformation', 'OBJECT', 'UPDATE')",
                transaction: _transaction).Single();

            //Assert
            hasPermission.Should().BeTrue();
        }

        [Test]
        [TestCase("RegisterStudent")]
        [TestCase("GetStudentWeeklyPlan")]
        [TestCase("GetLecturerWeeklyPlan")]
        [TestCase("GetStudentGradesInGroup")]
        [TestCase("GetStudentFinalGradesInSemester")]
        [TestCase("GetStudentWeightedAverageGradeInSemester")]
        [TestCase("GetStudentAbsencesInGroup")]
        [TestCase("GetGroupGrades")]
        [TestCase("GetCourseFinalGrades")]
        [TestCase("GetStudentsAbsencesInGroupCount")]
        [TestCase("RegisterLecturer")]
        public void Administration_Should_HaveExecPermissions_OnAllStoredProcedures(string procedure)
        {
            //Arrange
            _unitOfWork = DBConnectionProvider.AdministrationConnection();
            _unitOfWork.Open();

            //Act
            var hasPermission = _unitOfWork.Query<bool>($"SELECT HAS_PERMS_BY_NAME(@proc, 'OBJECT', 'EXECUTE')",
                                new { proc = procedure },
                                transaction: _transaction).Single();

            //Assert
            hasPermission.Should().BeTrue();
        }

        [Test]
        [TestCase("RegisterStudent", true)]
        [TestCase("GetStudentWeeklyPlan", true)]
        [TestCase("GetLecturerWeeklyPlan", false)]
        [TestCase("GetStudentGradesInGroup", true)]
        [TestCase("GetStudentFinalGradesInSemester", true)]
        [TestCase("GetStudentWeightedAverageGradeInSemester", true)]
        [TestCase("GetStudentAbsencesInGroup", true)]
        [TestCase("GetGroupGrades", false)]
        [TestCase("GetCourseFinalGrades", false)]
        [TestCase("GetStudentsAbsencesInGroupCount", false)]
        [TestCase("RegisterLecturer", false)]
        public void Student_Should_HaveExecPermissions_OnSpecifiedStoredProcedures(string procedure, bool shouldHavePermission)
        {
            //Arrange
            _unitOfWork = DBConnectionProvider.StudentConnection();
            _unitOfWork.Open();

            //Act
            var hasPermission = _unitOfWork.Query<bool>($"SELECT HAS_PERMS_BY_NAME(@proc, 'OBJECT', 'EXECUTE')",
                                new { proc = procedure },
                                transaction: _transaction).Single();

            //Assert
            hasPermission.Should().Be(shouldHavePermission);
        }

        [Test]
        [TestCase("RegisterStudent", false)]
        [TestCase("GetStudentWeeklyPlan", false)]
        [TestCase("GetLecturerWeeklyPlan", true)]
        [TestCase("GetStudentGradesInGroup", true)]
        [TestCase("GetStudentFinalGradesInSemester", false)]
        [TestCase("GetStudentWeightedAverageGradeInSemester", false)]
        [TestCase("GetStudentAbsencesInGroup", true)]
        [TestCase("GetGroupGrades", true)]
        [TestCase("GetCourseFinalGrades", true)]
        [TestCase("GetStudentsAbsencesInGroupCount", true)]
        [TestCase("RegisterLecturer", true)]
        public void Lecturer_Should_HaveExecPermissions_OnSpecifiedStoredProcedures(string procedure, bool shouldHavePermission)
        {
            //Arrange
            _unitOfWork = DBConnectionProvider.LecturerConnection();
            _unitOfWork.Open();

            //Act
            var hasPermission = _unitOfWork.Query<bool>($"SELECT HAS_PERMS_BY_NAME(@proc, 'OBJECT', 'EXECUTE')",
                                new { proc = procedure },
                                transaction: _transaction).Single();

            //Assert
            hasPermission.Should().Be(shouldHavePermission);
        }

        [TearDown]
        public void Teardown()
        {
            _unitOfWork = DBConnectionProvider.SuperAdminConnection();
            _unitOfWork.Dispose();
        }

    }
}