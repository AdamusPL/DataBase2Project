CREATE OR ALTER PROCEDURE GetStudentAbsencesInGroup (@GroupId nvarchar(20), @StudentId INT)
AS
BEGIN
SELECT
    sg.GroupId AS GroupId,
    s.Id AS StudentId,
    u.Name AS StudentName,
    u.Surname AS StudentSurname,
    a.Date AS AbsenceDate
FROM [Student_Group] sg
INNER JOIN [Student] s ON sg.StudentId = s.Id
INNER JOIN [User] u ON s.UserId = u.Id
INNER JOIN [Absence] a ON a.StudentInGroupId = sg.Id
WHERE s.Id = @StudentId AND sg.GroupId LIKE @GroupId
END;