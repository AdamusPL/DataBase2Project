CREATE OR ALTER PROCEDURE GetStudentWeeklyPlan(@StudentId int, @StartDate date, @EndDate date)
AS
BEGIN
    CREATE TABLE #Plan (
        Date date,
        Regularity varchar(255),
        Course varchar(255),
        GroupType varchar(255),
        StartTime time,
        EndTime time,
        Classroom varchar(255),
        Lecturer varchar(255)
    );

    WHILE (@StartDate <= @EndDate)
    BEGIN
        DECLARE @DayOfWeek AS int = DATEPART(WEEKDAY, @StartDate);
        set datefirst 1;
        DECLARE @WeekParity AS int = DATEPART(WEEKDAY, @StartDate) % 2;
        DECLARE @RegularityId AS int = CASE @WeekParity
            WHEN 0 THEN 2
            WHEN 1 THEN 1
        END;
        
        INSERT INTO #Plan (Date, Regularity, Course, GroupType, StartTime, EndTime, Classroom, Lecturer)
        SELECT 
            @StartDate, 
            Regularity.Name AS Regularity,
            Course.Name AS Course, 
            GroupType.Name AS GroupType,
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
        INNER JOIN GroupType ON g.TypeId = GroupType.Id
        WHERE Student_Group.StudentId = @StudentId AND (g.RegularityId = 0 OR g.RegularityId = @RegularityId)
        SET @StartDate = DATEADD(day, 1, @StartDate);
    END;

    SELECT * FROM #Plan ORDER BY Date, StartTime;
    DROP TABLE #Plan;
END;