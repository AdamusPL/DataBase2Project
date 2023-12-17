CREATE TABLE [Group] (
    Id varchar(20) NOT NULL,
    DayOfTheWeek varchar(255) NOT NULL,
    StartTime time(5) NOT NULL,
    EndTime time(5) NOT NULL,
    Classroom varchar(255) NOT NULL,
    Capacity int NOT NULL,
    RegularityId int NOT NULL FOREIGN KEY REFERENCES Regularity(Id),
    TypeId int NOT NULL FOREIGN KEY REFERENCES GroupType(Id),
    CourseId int NOT NULL FOREIGN KEY REFERENCES Course(Id),
    SemesterId varchar(20) NOT NULL FOREIGN KEY REFERENCES Semester(Id),
    PRIMARY KEY (Id)
);
CREATE INDEX Group_DayOfTheWeek ON [Group] (DayOfTheWeek);
