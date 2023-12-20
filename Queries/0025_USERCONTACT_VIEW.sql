CREATE VIEW UserContact AS
SELECT Name, Surname, Email, Phone FROM [User] u
INNER JOIN Email ON u.Id = Email.Id
INNER JOIN Lecturer ON u.Id = Lecturer.UserId
INNER JOIN WorkPhone ON Lecturer.UserId = WorkPhone.UserId;