CREATE OR ALTER PROCEDURE GetStudentWeeklyPlan(@StudentId int, @StartDate date, @EndDate date)
AS
BEGIN
    CREATE TABLE #Plan (
        Date date,
        RegularityId int,
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
        DECLARE @WeekParity AS int = DATEPART(ISO_WEEK, @StartDate) % 2;
        DECLARE @RegularityId AS int = CASE @WeekParity
            WHEN 0 THEN 2
            WHEN 1 THEN 1
        END;

        INSERT INTO #Plan (Date, RegularityId, Course, GroupType, StartTime, EndTime, Classroom, Lecturer)
        SELECT 
            @StartDate, 
            g.RegularityId AS RegularityId,
            c.Name AS Course, 
            gt.Name AS GroupType,
            StartTime, 
            EndTime, 
            Classroom, 
            CONCAT(u.Name, ' ', u.Surname) Lecturer
        FROM [Group] g
        INNER JOIN Student_Group sg ON sg.GroupId = g.Id
        INNER JOIN Course c ON c.Id = g.CourseId
        INNER JOIN GroupType gt ON gt.Id = g.TypeId
        INNER JOIN Group_Lecturer gl ON gl.GroupId = g.Id
        INNER JOIN Lecturer l ON l.Id = gl.LecturerId
        INNER JOIN [User] u ON u.Id = l.UserId
        WHERE sg.StudentId = @StudentId AND (g.RegularityId = 3 OR g.RegularityId = @RegularityId) AND DayOfTheWeek = @DayOfWeek
        
        SET @StartDate = DATEADD(day, 1, @StartDate);
    END;

    SELECT * FROM #Plan ORDER BY Date, StartTime;
    DROP TABLE #Plan;
END;
