CREATE TABLE GroupType (
    Id int IDENTITY(1, 1) NOT NULL,
    Name nvarchar(30) NOT NULL UNIQUE,
    PRIMARY KEY (Id)
);
INSERT INTO GroupType (Name) VALUES ('Lecture');
INSERT INTO GroupType (Name) VALUES ('Laboratory');
INSERT INTO GroupType (Name) VALUES ('Practice');
INSERT INTO GroupType (Name) VALUES ('Project');