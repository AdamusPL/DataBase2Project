SELECT c.Name, ECTS, u.Name LecturerName, u.Surname LecturerSurname from [Course] c
INNER JOIN Lecturer ON Lecturer.Id = c.LecturerId
INNER JOIN [User] u ON Lecturer.UserId = u.Id;