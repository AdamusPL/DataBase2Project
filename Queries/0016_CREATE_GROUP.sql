CREATE TABLE [Group] (
    Id nvarchar(20) NOT NULL,
    DayOfTheWeek int NOT NULL,
    StartTime time(5) NOT NULL,
    EndTime time(5) NOT NULL,
    Classroom nvarchar(255) NOT NULL,
    Capacity int NOT NULL,
    RegularityId int NOT NULL FOREIGN KEY REFERENCES Regularity(Id) ON DELETE CASCADE,
    TypeId int NOT NULL FOREIGN KEY REFERENCES GroupType(Id) ON DELETE CASCADE,
    CourseId int NOT NULL FOREIGN KEY REFERENCES Course(Id) ON DELETE CASCADE,
    SemesterId nvarchar(20) NOT NULL FOREIGN KEY REFERENCES Semester(Id) ON DELETE CASCADE,
    PRIMARY KEY (Id)
);
CREATE INDEX Group_DayOfTheWeek ON [Group] (DayOfTheWeek);
