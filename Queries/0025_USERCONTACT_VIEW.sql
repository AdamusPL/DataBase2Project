CREATE OR ALTER VIEW UserContact AS
SELECT u.Id as UserId, [Name], Surname, Email, Phone FROM [User] u
LEFT JOIN Email ON u.Id = Email.UserId
LEFT JOIN Lecturer ON u.Id = Lecturer.UserId
LEFT JOIN WorkPhone ON Lecturer.UserId = WorkPhone.UserId;