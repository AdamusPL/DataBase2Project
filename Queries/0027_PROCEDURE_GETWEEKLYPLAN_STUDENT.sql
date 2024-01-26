CREATE OR ALTER PROCEDURE GetStudentWeeklyPlan(@StudentId int, @StartDate date, @EndDate date)
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

        INSERT INTO #Plan (Date, RegularityId, Course, GroupTypeId, StartTime, EndTime, Classroom, Lecturer)
        SELECT 
            @StartDate, 
            g.RegularityId AS RegularityId,
            c.Name AS Course, 
            g.TypeId AS GroupTypeId,
            StartTime, 
            EndTime, 
            Classroom, 
            CONCAT(u.Name, ' ', u.Surname) Lecturer
        FROM [Group] g
        INNER JOIN [Student_Group] sg ON sg.GroupId = g.Id
        INNER JOIN [Course] c ON c.Id = g.CourseId
        INNER JOIN [Group_Lecturer] gl ON gl.GroupId = g.Id
        INNER JOIN [Lecturer] l ON l.Id = gl.LecturerId
        INNER JOIN [User] u ON u.Id = l.UserId
        INNER JOIN [Semester] s ON s.Id = g.SemesterId
        WHERE 
            sg.StudentId = @StudentId
            AND (g.RegularityId = 3 OR g.RegularityId = @RegularityId)
            AND DayOfTheWeek = @DayOfWeek
            AND @StartDate BETWEEN s.StartDate AND s.EndDate
            AND @EndDate BETWEEN s.StartDate AND s.EndDate;
        
        SET @StartDate = DATEADD(day, 1, @StartDate);
    END;

    SELECT * FROM #Plan ORDER BY Date, StartTime;
    DROP TABLE #Plan;
END;
