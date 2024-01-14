CREATE OR ALTER PROCEDURE GetLecturerWeeklyPlan(@LecturerId int, @StartDate date, @EndDate date)
AS
BEGIN
    CREATE TABLE #Plan (
        Date date,
        RegularityId int,
        Course varchar(255),
        GroupTypeId int,
        StartTime time,
        EndTime time,
        Classroom varchar(255),
        Lecturer varchar(255)
    );

    WHILE (@StartDate <= @EndDate)
    BEGIN
        DECLARE @DayOfWeek AS int = DATEPART(WEEKDAY, @StartDate);
        DECLARE @WeekParity AS int = DATEPART(ISO_WEEK, @StartDate) % 2;
        DECLARE @RegularityId AS int = CASE @WeekParity
            WHEN 0 THEN 2
            WHEN 1 THEN 1
        END;

        INSERT INTO #Plan (Date, RegularityId, Course, GroupTypeId, StartTime, EndTime, Classroom)
        SELECT 
            @StartDate, 
            g.RegularityId AS RegularityId,
            c.Name AS Course, 
            g.TypeId AS GroupTypeId,
            StartTime, 
            EndTime, 
            Classroom
        FROM [Group] g
        INNER JOIN Regularity r ON r.Id = g.RegularityId
        INNER JOIN Course c ON c.Id = g.CourseId
        INNER JOIN Group_Lecturer gl ON gl.GroupId = g.Id
        WHERE gl.LecturerId = @LecturerId AND (g.RegularityId = 3 OR g.RegularityId = @RegularityId) AND DayOfTheWeek = @DayOfWeek
        
        SET @StartDate = DATEADD(day, 1, @StartDate);
    END;

    SELECT * FROM #Plan ORDER BY Date, StartTime;
    DROP TABLE #Plan;
END;
