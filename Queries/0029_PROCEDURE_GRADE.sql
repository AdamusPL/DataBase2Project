CREATE OR ALTER PROCEDURE GetStudentGradesInGroup (@StudentId int, @GroupId VARCHAR)
AS
BEGIN
    SELECT Grade 
    FROM Grade 
    INNER JOIN Student_Group ON Student_Group.Id = Grade.StudentInGroupId
    INNER JOIN [Group] g ON Student_Group.GroupId = g.Id
    INNER JOIN Student ON Student_Group.StudentId = Student.Id
    WHERE @StudentId = Student.Id AND @GroupId = g.Id
END;