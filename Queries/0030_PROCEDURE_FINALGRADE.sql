CREATE OR ALTER PROCEDURE GetStudentFinalGradesInSemester (@StudentId INT, @SemesterId VARCHAR)
AS
BEGIN

    SELECT Course.Name, Grade, ECTS
    FROM Course
    INNER JOIN [Group] g ON Course.Id = g.CourseId
    INNER JOIN Student_Group ON g.Id = Student_Group.GroupId
    INNER JOIN Grade ON Student_Group.StudentId = Grade.StudentInGroupId
    INNER JOIN Student ON Student_Group.StudentId = Student.Id
    WHERE @StudentId = Student.Id AND @SemesterId = g.SemesterId AND isFinal = 1

END;