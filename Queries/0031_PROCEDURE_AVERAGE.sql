CREATE OR ALTER PROCEDURE GetStudentWeightedAverageGradeInSemester(@StudentId INT, @SemesterId NVARCHAR(20))
AS
BEGIN
    CREATE TABLE #Grade (
        Id int,
        Grade DECIMAL(10, 3)
    );

    INSERT INTO #Grade
    SELECT s.Id, SUM(Grade * ECTS) / NULLIF(SUM(ECTS), 1) AS Grade
    FROM [Student] s
    INNER JOIN [Student_Group] sg ON s.Id = sg.StudentId
    INNER JOIN [Group] g ON sg.GroupId = g.Id
    INNER JOIN [Course] c ON g.CourseId = c.Id
    LEFT JOIN [Grade] gr ON sg.Id = gr.StudentInGroupId
    WHERE s.Id = @StudentId
        AND g.SemesterId = @SemesterId
        AND (gr.isFinal = 1 OR gr.IsFinal IS NULL)
    GROUP BY s.Id;

    SELECT TOP 1 Grade FROM #Grade;
    DROP TABLE #Grade;
END;
