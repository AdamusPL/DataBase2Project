CREATE TABLE Grade (
    Id int IDENTITY(1, 1) NOT NULL,
    Text nvarchar(50) NOT NULL,
    Grade decimal(2, 1) NOT NULL,
    Accepted bit NULL,
    IsFinal bit NOT NULL,
    StudentInGroupId int NOT NULL FOREIGN KEY REFERENCES Student_Group(Id) ON DELETE CASCADE,
    PRIMARY KEY (Id)
);
