CREATE OR ALTER PROCEDURE GetStudentsAbsencesInGroupCount (@GroupId varchar(20))
AS
BEGIN
    SELECT Student.Id, u.Name, u.Surname, Count(Absence.Id) NumberOfAbsences
    FROM Student
    INNER JOIN Student_Group ON Student.Id = Student_Group.StudentId
    INNER JOIN [Group] g ON Student_Group.GroupId = g.Id
    INNER JOIN [User] u ON Student.UserId = u.Id
    INNER JOIN Absence ON Student_Group.Id = Absence.StudentInGroupId
    WHERE @GroupId = g.Id
    GROUP BY Student.Id, u.Name, u.Surname;
END;