using System.Data;
using Dapper;
using FluentAssertions;
using Microsoft.Data.SqlClient;
using Models;
using Shared;

namespace DBUnitTests.Tests
{
    [TestFixture]
    public class CRUDTests
    {
        private IDbConnection _unitOfWork;
        private IDbTransaction _transaction;
        private int _userIdentity;

        [SetUp]
        public void Setup()
        {
            _unitOfWork = DBConnectionProvider.SuperAdminConnection();
            _unitOfWork.Open();
            _transaction = _unitOfWork.BeginTransaction();
            _userIdentity = _unitOfWork.Query<int>("SELECT IDENT_CURRENT('User')", transaction: _transaction).Single();
        }

        [Test]
        public void Insert_Semester_Should_InsertSemesterIntoDb()
        {
            //Arrange
            var id = Guid.NewGuid().ToString()[..20];
            var newSemester = new Semester()
            {
                Id = id,
                StartDate = new DateTime(2021, 10, 1),
                EndDate = new DateTime(2022, 2, 7)
            };
            var insertQuery = "INSERT INTO Semester (Id, StartDate, EndDate) VALUES (@Id, @StartDate, @EndDate);";
            //Act
            _unitOfWork.Execute(insertQuery, newSemester, _transaction);

            //Assert
            var selectQuery = "SELECT * FROM Semester WHERE Id = @Id";
            var insertedElement = _unitOfWork.Query<Semester>(selectQuery, new { Id = id }, _transaction).First();

            insertedElement.Should().BeEquivalentTo(newSemester);
        }

        [Test]
        public void Insert_User_Should_InsertUserIntoDb_WithAutoIncrementId()
        {
            //Arrange
            var newUser1 = new User()
            {
                Name = "Filip",
                Surname = "Murawski"
            };

            var newUser2 = new User()
            {
                Name = "Juan",
                Surname = "Pablo"
            };

            var insertQuery = "INSERT INTO [User] (Name, Surname) VALUES (@Name, @Surname); SELECT CAST(SCOPE_IDENTITY() as int)";

            //Act
            var id1 = _unitOfWork.Query<int>(insertQuery, newUser1, _transaction).Single();
            var id2 = _unitOfWork.Query<int>(insertQuery, newUser2, _transaction).Single();


            //Assert
            newUser1.Id = id1;
            newUser2.Id = id2;
            var selectQuery = "SELECT * FROM [User] WHERE Id = @Id";
            var insertedElement = _unitOfWork.Query<User>(selectQuery, new { Id = id1 }, _transaction).First();

            insertedElement.Name.Should().Be("Filip");
            insertedElement.Surname.Should().Be("Murawski");
            insertedElement.Id.Should().Be(id1);
            id2.Should().BeGreaterThan(id1);
        }

        [Test]
        public void Update_Should_UpdateElementInDb()
        {
            //Arrange
            var newUser1 = new User()
            {
                Name = "Filip",
                Surname = "Murawski"
            };
            var newUser2 = new User()
            {
                Name = "Olafito",
                Surname = "Szajda"
            };

            var newName = "Juan";

            var insertQuery = "INSERT INTO [User] (Name, Surname) VALUES (@Name, @Surname); SELECT CAST(SCOPE_IDENTITY() as int)";
            var id1 = _unitOfWork.Query<int>(insertQuery, newUser1, _transaction).Single();
            var id2 = _unitOfWork.Query<int>(insertQuery, newUser2, _transaction).Single();

            //Act
            var updateQuery = "UPDATE [User] SET Name = @Name WHERE Id = @Id";
            _unitOfWork.Execute(updateQuery, new { Name = newName, Id = id1 }, _transaction);


            //Assert
            var selectQuery = "SELECT * FROM [User] WHERE Id = @Id";
            var updatedElement = _unitOfWork.Query<User>(selectQuery, new { Id = id1 }, _transaction).First();
            var otherElement = _unitOfWork.Query<User>(selectQuery, new { Id = id2 }, _transaction).First();

            updatedElement.Name.Should().Be(newName);
            otherElement.Name.Should().NotBe(newName);
        }

        [Test]
        public void Delete_Should_DeleteElementFromDb()
        {
            //Arrange
            var newUser = new User()
            {
                Name = "Filip",
                Surname = "Murawski"
            };

            var insertQuery = "INSERT INTO [User] (Name, Surname) VALUES (@Name, @Surname); SELECT CAST(SCOPE_IDENTITY() as int)";
            var id = _unitOfWork.Query<int>(insertQuery, newUser, _transaction).Single();

            //Act
            var deleteQuery = "DELETE FROM [User] WHERE Id = @Id";
            _unitOfWork.Execute(deleteQuery, new { Id = id }, _transaction);


            //Assert
            var selectQuery = "SELECT * FROM [User] WHERE Id = @Id";
            var insertedElement = _unitOfWork.Query<User>(selectQuery, new { Id = id }, _transaction);

            insertedElement.Should().BeEmpty();
        }

        [TearDown]
        public void Teardown()
        {
            _transaction.Rollback();
            _unitOfWork.Execute("DBCC CHECKIDENT ('User', RESEED, @identity);", new { identity = _userIdentity });
            _unitOfWork.Dispose();
        }
    }
}