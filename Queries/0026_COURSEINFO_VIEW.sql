CREATE VIEW CourseInfo AS
SELECT c.Name, ECTS, u.Name LecturerName, u.Surname LecturerSurname 
FROM [Course] c
INNER JOIN Lecturer ON Lecturer.Id = c.LecturerId
INNER JOIN [User] u ON Lecturer.UserId = u.Id;