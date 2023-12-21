CREATE OR ALTER PROCEDURE GetStudentAbsencesInGroup (@GroupId VARCHAR, @StudentId INT)
AS
SELECT Date from Student.Id, u.Name, u.Surname, Absence
INNER JOIN Student_Group ON Student.Id = Student_Group.StudentId
INNER JOIN [Group] g ON Student_Group.GroupId = g.Id
INNER JOIN [User] u ON Student.UserId = User.Id
WHERE @GroupId = g.Id AND @StudentId = Student.Id;