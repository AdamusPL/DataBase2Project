CREATE OR ALTER PROCEDURE GetStudentAbsencesInGroup (@GroupId varchar(20), @StudentId INT)
AS
BEGIN
    SELECT Student.Id, u.Name, u.Surname, Absence.Date AbsenceDate
    FROM Student
    INNER JOIN Student_Group ON Student.Id = Student_Group.StudentId
    INNER JOIN [Group] g ON Student_Group.GroupId = g.Id
    INNER JOIN [User] u ON Student.UserId = u.Id
    INNER JOIN Absence ON Student_Group.Id = Absence.StudentInGroupId
    WHERE @GroupId = g.Id AND @StudentId = Student.Id;
END;