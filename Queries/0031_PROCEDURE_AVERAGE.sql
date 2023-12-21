-- tu musi byc srednia wazona!!!
CREATE PROCEDURE GetStudentWeightedAverageGradeInSemester(@StudentId INT, @SemesterId VARCHAR)
AS
BEGIN
SELECT Student.Id, SUM(Grade * ECTS) / NULLIF(SUM(ECTS), 1) AS WeightedAverage
FROM Student
INNER JOIN Student_Group ON Student.Id = Student_Group.StudentId
INNER JOIN Grade ON Student_Group.StudentId = Grade.StudentInGroupId
INNER JOIN [Group] g ON Student_Group.GroupId = g.Id
INNER JOIN Course ON g.CourseId = Course.Id
WHERE @StudentId = Student.Id AND @SemesterId = g.SemesterId AND isFinal = 1
GROUP BY Student.Id
END;