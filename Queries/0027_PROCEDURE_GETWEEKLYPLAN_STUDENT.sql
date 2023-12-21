CREATE OR ALTER PROCEDURE GetWeeklyPlan(@UserId INT, @StartOfTheWeek DATE, @EndOfTheWeek DATE)
AS
BEGIN
    SELECT 
        DayOfTheWeek, 
        Regularity.Name Regularity,
        Course.Name, 
        StartTime, 
        EndTime, 
        Classroom, 
        CONCAT(u.Name, ' ', u.Surname) Lecturer
    FROM [Group] g
    INNER JOIN Course ON g.CourseId = Course.Id
    INNER JOIN Regularity ON g.RegularityId = Regularity.Id
    INNER JOIN Group_Lecturer ON g.Id = Group_Lecturer.GroupId
    INNER JOIN Lecturer ON Group_Lecturer.LecturerId = Lecturer.Id
    INNER JOIN [User] u ON Lecturer.UserId = u.Id
    WHERE DayOfTheWeek >= @StartOfTheWeek AND DayOfTheWeek <= @EndOfTheWeek
END;