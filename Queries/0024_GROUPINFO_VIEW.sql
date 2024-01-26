CREATE OR ALTER VIEW GroupInfo AS
SELECT 
    g.DayOfTheWeek, 
    g.StartTime, 
    g.EndTime,
    gl.GroupId, 
    g.Classroom, 
    r.Name AS Regularity, 
    gt.Name AS GroupType,
    u.Surname AS LecturerSurname, 
    u.Name AS LecturerName, 
    l.Id AS LecturerId,
    c.Name AS CourseName, 
    c.ECTS
FROM [Group] g 
INNER JOIN [Group_Lecturer] gl ON g.Id = gl.GroupId
INNER JOIN [Regularity] r ON g.RegularityId = r.Id
INNER JOIN [GroupType] gt ON g.Id = gt.Id 
INNER JOIN [Lecturer] l ON gl.LecturerId = l.Id
INNER JOIN [User] u ON l.UserId = u.Id
INNER JOIN [Course] c ON g.CourseId = c.Id;
