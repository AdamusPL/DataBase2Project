CREATE OR ALTER PROCEDURE GetWeeklyPlanStudent(@StudentId INT, @StartDate DATE, @EndDate DATE)
AS
BEGIN
    WHILE (@StartDate <= @EndDate)
    BEGIN
        DECLARE @DayOfWeek AS int = DATEPART(WEEKDAY, @StartDate);
        set datefirst 1;
        DECLARE @WeekParity AS int = DATEPART(WEEKDAY, @StartDate) % 2;
        DECLARE @RegularityId AS int = CASE @WeekParity
            WHEN 0 THEN 2
            WHEN 1 THEN 1
        END;
        
        SELECT 
            @StartDate, 
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
        INNER JOIN Student_Group ON Student_Group.GroupId = g.Id
        WHERE Student_Group.StudentId = @StudentId AND (g.RegularityId = 0 OR g.RegularityId = @RegularityId)
        SET @StartDate = DATEADD(day, 1, @StartDate);
    END;
END;