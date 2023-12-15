using System.Data;
using Dapper;
using FluentAssertions;
using Microsoft.Data.SqlClient;
using Models;

namespace DBUnitTests.Tests
{
    [TestFixture]
    public class PermissionTests
    {

        /// <summary>
        /// TODO: moznaby to rozdzielic na rozne klasy z testami
        /// trzeba zaimplementowac puste testy
        /// i dopisac testy na permisje do procedur skladowanych i widokow
        /// </summary>
        /// 

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

        private static readonly List<Func<IDbConnection>> UserConnections =
        [
            DBConnectionProvider.AdministrationConnection,
            DBConnectionProvider.StudentConnection,
            DBConnectionProvider.LecturerConnection
        ];

        private static readonly Dictionary<string, string> TableNamesWithInsertStatements = new()
            {
                {"Absence", "INSERT INTO Absence (Date, StudentInGroupId) VALUES('12-', 'tests')"},
            };

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
        [Ignore("Not Implemented")]
        public void Student_ShouldNot_HaveInsertPermissions_OnAnyTables([ValueSource(nameof(TableNames))] string table)
        {
            ///TODO: Tutaj trzeba zrobic liste przykladowych queriesow z insertem i puscic je z odpowiednim userem w bazie
            ///schody sie zaczynaja w tym miejscu, ze te inserty maja porobione rozne constrainty
            ///a jest 2:40 w nocy i mi sie nie chce o tym myslec :)

            ////Arrange
            //_unitOfWork = DBConnectionProvider.LecturerConnection();
            //_unitOfWork.Open();
            //_transaction = _unitOfWork.BeginTransaction();

            ////Act
            //var act = () => _unitOfWork.Query($"DELETE FROM {table}", transaction: _transaction);

            ////Assert
            //act.Should().Throw<SqlException>();

            ////Teardown
            //_transaction.Rollback();
            //_unitOfWork.Dispose();
        }

        [Test]
        [Ignore("Not Implemented")]
        public void Lecturer_ShouldNot_HaveInsertPermissions_OnAnyTables([ValueSource(nameof(TableNames))] string table)
        {
            ///TODO: Tutaj trzeba zrobic liste przykladowych queriesow z insertem i puscic je z odpowiednim userem w bazie
            ///schody sie zaczynaja w tym miejscu, ze te inserty maja porobione rozne constrainty
            ///a jest 2:40 w nocy i mi sie nie chce o tym myslec :)

            ////Arrange
            //_unitOfWork = DBConnectionProvider.LecturerConnection();
            //_unitOfWork.Open();
            //_transaction = _unitOfWork.BeginTransaction();

            ////Act
            //var act = () => _unitOfWork.Query($"DELETE FROM {table}", transaction: _transaction);

            ////Assert
            //act.Should().Throw<SqlException>();

            ////Teardown
            //_transaction.Rollback();
            //_unitOfWork.Dispose();
        }

        [Test]
        [Ignore("Not Implemented")]
        public void Student_ShouldNot_HaveUpdatePermissions_OnAnyTables([ValueSource(nameof(TableNames))] string table)
        {
            ///TODO: Tutaj trzeba zrobic liste przykladowych queriesow z insertem i puscic je z odpowiednim userem w bazie
            ///oczywiscie najpierw musza istniec dane w bazie 
            ///i trzeba z listy tabelek wywalic te tabelki na ktore faktycznie jest permisja
            ///schody sie zaczynaja w tym miejscu, ze te inserty maja porobione rozne constrainty
            ///a jest 2:40 w nocy i mi sie nie chce o tym myslec :)

            ////Arrange
            //_unitOfWork = DBConnectionProvider.LecturerConnection();
            //_unitOfWork.Open();
            //_transaction = _unitOfWork.BeginTransaction();

            ////Act
            //var act = () => _unitOfWork.Query($"DELETE FROM {table}", transaction: _transaction);

            ////Assert
            //act.Should().Throw<SqlException>();

            ////Teardown
            //_transaction.Rollback();
            //_unitOfWork.Dispose();
        }

        [Test]
        [Ignore("Not Implemented")]
        public void Lecturer_ShouldNot_HaveUpdatePermissions_OnAnyTables([ValueSource(nameof(TableNames))] string table)
        {
            ///TODO: Tutaj trzeba zrobic liste przykladowych queriesow z updatem i puscic je z odpowiednim userem w bazie
            ///oczywiscie najpierw musza istniec dane w bazie 
            ///i trzeba z listy tabelek wywalic te tabelki na ktore faktycznie jest permisja
            ///schody sie zaczynaja w tym miejscu, ze te inserty maja porobione rozne constrainty
            ///a jest 2:40 w nocy i mi sie nie chce o tym myslec :)

            ////Arrange
            //_unitOfWork = DBConnectionProvider.LecturerConnection();
            //_unitOfWork.Open();
            //_transaction = _unitOfWork.BeginTransaction();

            ////Act
            //var act = () => _unitOfWork.Query($"DELETE FROM {table}", transaction: _transaction);

            ////Assert
            //act.Should().Throw<SqlException>();

            ////Teardown
            //_transaction.Rollback();
            //_unitOfWork.Dispose();
        }

        [Test]
        [Ignore("Not Implemented")]
        public void Student_Should_HaveUpdatePermissions_OnGrades()
        {

            ////Arrange
            //_unitOfWork = DBConnectionProvider.LecturerConnection();
            //_unitOfWork.Open();
            //_transaction = _unitOfWork.BeginTransaction();

            ////Act
            //var act = () => _unitOfWork.Query($"DELETE FROM {table}", transaction: _transaction);

            ////Assert
            //act.Should().Throw<SqlException>();

            ////Teardown
            //_transaction.Rollback();
            //_unitOfWork.Dispose();
        }

        [Test]
        [Ignore("Not Implemented")]
        public void Student_Should_HaveUpdatePermissions_OnLoginInformation()
        {

            ////Arrange
            //_unitOfWork = DBConnectionProvider.LecturerConnection();
            //_unitOfWork.Open();
            //_transaction = _unitOfWork.BeginTransaction();

            ////Act
            //var act = () => _unitOfWork.Query($"DELETE FROM {table}", transaction: _transaction);

            ////Assert
            //act.Should().Throw<SqlException>();

            ////Teardown
            //_transaction.Rollback();
            //_unitOfWork.Dispose();
        }

        [Test]
        [Ignore("Not Implemented")]
        public void Lecturer_Should_HaveInsertPermissions_OnGrades()
        {

            ////Arrange
            //_unitOfWork = DBConnectionProvider.LecturerConnection();
            //_unitOfWork.Open();
            //_transaction = _unitOfWork.BeginTransaction();

            ////Act
            //var act = () => _unitOfWork.Query($"DELETE FROM {table}", transaction: _transaction);

            ////Assert
            //act.Should().Throw<SqlException>();

            ////Teardown
            //_transaction.Rollback();
            //_unitOfWork.Dispose();
        }

        [Test]
        [Ignore("Not Implemented")]
        public void Lecturer_Should_HaveInsertPermissions_OnAbsences()
        {

            ////Arrange
            //_unitOfWork = DBConnectionProvider.LecturerConnection();
            //_unitOfWork.Open();
            //_transaction = _unitOfWork.BeginTransaction();

            ////Act
            //var act = () => _unitOfWork.Query($"DELETE FROM {table}", transaction: _transaction);

            ////Assert
            //act.Should().Throw<SqlException>();

            ////Teardown
            //_transaction.Rollback();
            //_unitOfWork.Dispose();
        }

        [Test]
        [Ignore("Not Implemented")]
        public void Lecturer_Should_HaveUpdatePermissions_OnLoginInformation()
        {

            ////Arrange
            //_unitOfWork = DBConnectionProvider.LecturerConnection();
            //_unitOfWork.Open();
            //_transaction = _unitOfWork.BeginTransaction();

            ////Act
            //var act = () => _unitOfWork.Query($"DELETE FROM {table}", transaction: _transaction);

            ////Assert
            //act.Should().Throw<SqlException>();

            ////Teardown
            //_transaction.Rollback();
            //_unitOfWork.Dispose();
        }

        [TearDown]
        public void Teardown()
        {
            _unitOfWork = DBConnectionProvider.SuperAdminConnection();
            _unitOfWork.Execute("DBCC CHECKIDENT ('User', RESEED, 0);");
            _unitOfWork.Dispose();
        }

    }
}