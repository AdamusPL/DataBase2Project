CREATE TABLE Student_Group (
    Id int IDENTITY(1, 1) NOT NULL,
    StudentId int NOT NULL FOREIGN KEY REFERENCES Student(Id) ON DELETE CASCADE,
    GroupId nvarchar(20) NOT NULL FOREIGN KEY REFERENCES [Group](Id),
    RegistrationDate date NOT NULL,
    PRIMARY KEY (Id)
);
CREATE INDEX Student_Group_RegistrationDate ON Student_Group (RegistrationDate DESC);
