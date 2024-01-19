CREATE OR ALTER PROCEDURE GetCourseFinalGrades (@CourseId int, @SemesterId varchar(20))
AS
BEGIN
    SELECT Student.Id, u.Name, u.Surname, Grade
    FROM Student
    INNER JOIN Student_Group ON Student.Id = Student_Group.StudentId
    INNER JOIN [Group] g ON Student_Group.GroupId = g.Id
    INNER JOIN Grade ON Student_Group.Id = Grade.StudentInGroupId
    INNER JOIN [User] u ON Student.UserId = u.Id
    WHERE g.CourseId = @CourseId AND g.SemesterId = @SemesterId AND isFinal = 1
END;