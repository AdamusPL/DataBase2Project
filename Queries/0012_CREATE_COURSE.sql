CREATE TABLE Course (
    Id int IDENTITY(1, 1) NOT NULL,
    Name int NOT NULL,
    ECTS int NOT NULL,
    LecturerId int NOT NULL FOREIGN KEY REFERENCES Lecturer(Id),
    PRIMARY KEY (Id)
);
CREATE INDEX Course_Name ON Course (Name);
