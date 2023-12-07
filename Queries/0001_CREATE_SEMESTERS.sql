CREATE TABLE Semester (
    Id varchar(20) NOT NULL,
    StartDate date NOT NULL,
    EndDate date NOT NULL,
    PRIMARY KEY (Id)
);
CREATE INDEX Semester_StartDate ON Semester (StartDate DESC);
CREATE INDEX Semester_EndDate ON Semester (EndDate DESC);
CREATE INDEX Semester_StartDate_EndDate ON Semester (StartDate, EndDate DESC);
