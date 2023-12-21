CREATE OR ALTER PROCEDURE GetStudentGradesInGroup (@StudentId int, @GroupId VARCHAR)
AS
BEGIN
    SELECT Grade 
    FROM Grade 
    INNER JOIN Student_Group ON Student_Group.Id = Grade.StudentInGroupId
    INNER JOIN [Group] g ON Student_Group.GroupId = g.Id
    WHERE @StudentId = Student_Group.StudentId AND @GroupId = g.Id
END;