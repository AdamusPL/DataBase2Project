CREATE TABLE FieldOfStudy (
    Id int IDENTITY(1, 1) NOT NULL,
    Name varchar(50) NOT NULL,
    Degree int NOT NULL,
    FacultyId varchar(10) NOT NULL FOREIGN KEY REFERENCES Faculty(Id),
    PRIMARY KEY (Id)
);
CREATE INDEX FieldOfStudy_Name ON FieldOfStudy (Name);
