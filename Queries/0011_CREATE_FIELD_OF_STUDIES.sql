CREATE TABLE FieldOfStudy (
    Id int IDENTITY(1, 1) NOT NULL,
    Name nvarchar(50) NOT NULL,
    Degree int NOT NULL,
    FacultyId nvarchar(10) NOT NULL FOREIGN KEY REFERENCES Faculty(Id) ON DELETE CASCADE,
    PRIMARY KEY (Id)
);
CREATE INDEX FieldOfStudy_Name ON FieldOfStudy (Name);
