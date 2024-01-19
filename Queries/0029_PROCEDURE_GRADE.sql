CREATE OR ALTER PROCEDURE GetStudentGradesInGroup (@StudentId int, @GroupId varchar(20))
AS
BEGIN
    SELECT Grade 
    FROM Grade 
    INNER JOIN Student_Group ON Student_Group.Id = Grade.StudentInGroupId
    WHERE @StudentId = Student_Group.StudentId AND @GroupId = Student_Group.GroupId
END;