CREATE OR ALTER PROCEDURE GetStudentWeightedAverageGradeInSemester(@StudentId int, @SemesterId varchar(20))
AS
BEGIN
    SELECT SUM(Grade * ECTS) / CONVERT(decimal(4,2), ECTS) AS WeightedAverage
    FROM Student
    INNER JOIN Student_Group ON Student.Id = Student_Group.StudentId
    INNER JOIN Grade ON Student_Group.Id = Grade.StudentInGroupId
    INNER JOIN [Group] g ON Student_Group.GroupId = g.Id
    INNER JOIN Course ON g.CourseId = Course.Id
    WHERE Student.Id = @StudentId AND g.SemesterId = @SemesterId AND isFinal = 1
END;