CREATE OR ALTER PROCEDURE GetWeeklyPlanLecturer(@UserId INT, @StartOfTheWeek DATE, @EndOfTheWeek DATE)
AS
BEGIN
    WHILE (@StartDate <= @EndDate)
    BEGIN
        SELECT 
            DayOfTheWeek, 
            Regularity.Name Regularity, 
            g.Id, 
            FieldOfStudy.Name, 
            Course.Name, 
            StartTime, 
            EndTime, 
            Classroom
        FROM [Group] g
        INNER JOIN Course ON g.CourseId = Course.Id
        INNER JOIN FieldOfStudy_Course ON Course.Id = FieldOfStudy_Course.CourseId
        INNER JOIN FieldOfStudy ON FieldOfStudy_Course.FieldOfStudyId = FieldOfStudy.Id
        INNER JOIN Regularity ON g.RegularityId = Regularity.Id
        INNER JOIN Group_Lecturer ON g.Id = Group_Lecturer.GroupId
        INNER JOIN Lecturer ON Group_Lecturer.LecturerId = Lecturer.Id
        INNER JOIN [User] u ON Lecturer.UserId = u.Id
END;