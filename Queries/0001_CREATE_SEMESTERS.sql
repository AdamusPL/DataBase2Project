CREATE TABLE Semester (
    Id nvarchar(20) NOT NULL,
    StartDate datetime2 NOT NULL,
    EndDate datetime2 NOT NULL,
    PRIMARY KEY (Id)
);
CREATE INDEX Semester_StartDate ON Semester (StartDate DESC);
CREATE INDEX Semester_EndDate ON Semester (EndDate DESC);
CREATE INDEX Semester_StartDate_EndDate ON Semester (StartDate, EndDate DESC);
