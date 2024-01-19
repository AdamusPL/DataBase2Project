CREATE OR ALTER VIEW CourseInfo AS
SELECT c.Id as CourseId, c.Name as CourseName, ECTS, u.Name as LecturerName, u.Surname as LecturerSurname 
FROM [Course] c
INNER JOIN Lecturer ON Lecturer.Id = c.LecturerId
INNER JOIN [User] u ON Lecturer.UserId = u.Id;