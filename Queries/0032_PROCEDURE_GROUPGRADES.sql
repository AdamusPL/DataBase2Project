CREATE OR ALTER PROCEDURE GetGroupGrades (@GroupId varchar(20))
AS
BEGIN
    SELECT Student.Id, u.Name, u.Surname, Grade
    FROM Student
    INNER JOIN Student_Group ON Student.Id = Student_Group.StudentId
    INNER JOIN [Group] g ON Student_Group.GroupId = g.Id
    INNER JOIN Grade ON Student_Group.Id = Grade.StudentInGroupId
    INNER JOIN [User] u ON Student.UserId = u.Id
    WHERE @GroupId = g.Id
END;