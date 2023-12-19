CREATE TABLE Grade (
    Id int IDENTITY(1, 1) NOT NULL,
    Grade int NOT NULL,
    Accepted bit NULL,
    IsFinal bit NOT NULL,
    StudentInGroupId int NOT NULL FOREIGN KEY REFERENCES Student_Group(Id) ON DELETE CASCADE,
    PRIMARY KEY (Id)
);
