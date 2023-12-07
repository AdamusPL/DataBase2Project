CREATE TABLE FieldOfStudy_Course (
    FieldOfStudyId int NOT NULL FOREIGN KEY REFERENCES FieldOfStudy(Id),
    CourseId int NOT NULL FOREIGN KEY REFERENCES Course(Id),
    PRIMARY KEY (FieldOfStudyId, CourseId)
);
