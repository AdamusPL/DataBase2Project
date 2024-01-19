CREATE TABLE Group_Lecturer (
    GroupId nvarchar(20) NOT NULL FOREIGN KEY REFERENCES [Group](Id) ON DELETE CASCADE,
    LecturerId int NOT NULL FOREIGN KEY REFERENCES Lecturer(Id),
    PRIMARY KEY (GroupId, LecturerId)
);
