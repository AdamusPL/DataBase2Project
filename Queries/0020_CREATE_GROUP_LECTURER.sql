CREATE TABLE Group_Lecturer (
    GroupId varchar(20) NOT NULL FOREIGN KEY REFERENCES [Group](Id),
    LecturerId int NOT NULL FOREIGN KEY REFERENCES Lecturer(Id),
    PRIMARY KEY (GroupId, LecturerId)
);
