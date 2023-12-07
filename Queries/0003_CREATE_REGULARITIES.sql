CREATE TABLE Regularity (
    Id int IDENTITY(1, 1) NOT NULL,
    Name varchar(10) NOT NULL UNIQUE,
    PRIMARY KEY (Id)
);
INSERT INTO Regularity (Name) VALUES ('Odd');
INSERT INTO Regularity (Name) VALUES ('Even');
INSERT INTO Regularity (Name) VALUES ('EveryWeek');