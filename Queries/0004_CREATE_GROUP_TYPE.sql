CREATE TABLE GroupType (
    Id int IDENTITY(1, 1) NOT NULL,
    Name varchar(30) NOT NULL UNIQUE,
    PRIMARY KEY (Id)
);
INSERT INTO Regularity (Name) VALUES ('Lecture');
INSERT INTO Regularity (Name) VALUES ('Laboratory');
INSERT INTO Regularity (Name) VALUES ('Practice');
INSERT INTO Regularity (Name) VALUES ('Project');