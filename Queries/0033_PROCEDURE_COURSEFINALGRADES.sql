CREATE OR ALTER PROCEDURE GetCourseFinalGrades (@CourseId INT, @SemesterId VARCHAR)
AS
SELECT Student.Id, u.Name, u.Surname, Grade
FROM Student
INNER JOIN Student_Group ON Student.Id = Student_Group.StudentId
INNER JOIN [Group] g ON Student_Group.GroupId = g.Id
INNER JOIN Semester ON g.SemesterId = Semester.Id
INNER JOIN Course ON g.CourseId = Course.Id
INNER JOIN Grade ON Student_Group.StudentId = Grade.StudentInGroupId
INNER JOIN [User] u ON Student.UserId = u.Id
WHERE @CourseId = Course.Id AND @SemesterId = Semester.Id AND isFinal = 1;