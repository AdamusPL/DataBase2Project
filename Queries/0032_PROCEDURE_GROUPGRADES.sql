CREATE OR ALTER PROCEDURE GetGroupGrades (@GroupId VARCHAR)
AS
SELECT Student.Id, u.Name, u.Surname, Grade
FROM Student
INNER JOIN Student_Group ON Student.Id = Student_Group.StudentId
INNER JOIN [Group] g ON Student_Group.GroupId = g.Id
INNER JOIN Grade ON Student_Group.StudentId = Grade.StudentInGroupId
INNER JOIN [User] u ON Student.UserId = u.Id
WHERE @GroupId = g.Id;