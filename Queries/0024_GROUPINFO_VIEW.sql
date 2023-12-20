CREATE VIEW GroupInfo AS
SELECT 
    g.DayOfTheWeek, 
    g.StartTime, 
    g.EndTime,
    Group_Lecturer.GroupId, 
    g.Classroom, 
    Regularity.Name Regularity, 
    GroupType.Name GroupType,
    u.Surname LecturerSurname, 
    u.Name LecturerName, 
    l.Id LecturerId,
    Course.Name CourseName, 
    Course.ECTS
FROM [Group] g 
INNER JOIN Group_Lecturer ON g.Id = Group_Lecturer.GroupId
INNER JOIN Regularity ON g.RegularityId = Regularity.Id
INNER JOIN GroupType ON g.Id = GroupType.Id 
INNER JOIN [Lecturer] l ON Group_Lecturer.LecturerId = l.Id
INNER JOIN [User] u ON l.UserId = u.Id
INNER JOIN Course ON g.CourseId = Course.Id;

