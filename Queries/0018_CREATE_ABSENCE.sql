CREATE TABLE Absence (
    Id int IDENTITY(1, 1) NOT NULL, 
    [Date] date NOT NULL, 
    StudentInGroupId int NOT NULL FOREIGN KEY REFERENCES Student_Group(Id), 
    PRIMARY KEY (Id)
);
