-- tu musi byc srednia wazona!!!
CREATE PROCEDURE GetStudentWeightedAverageGradeInSemester(@StudentId INT, @SemesterId VARCHAR)
AS
SELECT Student.Id, AVG(Grade) AVERAGE
FROM Student
INNER JOIN Student_Group ON Student.Id = Student_Group.StudentId
INNER JOIN Grade ON Student_Group.StudentId = Grade.StudentInGroupId
INNER JOIN [Group] g ON Student_Group.GroupId = g.Id
INNER JOIN Semester ON g.SemesterId = Semester.Id
WHERE @StudentId = Student.Id AND @SemesterId = Semester.Id AND isFinal = 1