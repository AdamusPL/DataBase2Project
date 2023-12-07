CREATE TABLE Student_FieldOfStudy (
    StudentId int NOT NULL FOREIGN KEY REFERENCES Student(Id), 
    FieldOfStudyId int NOT NULL FOREIGN KEY REFERENCES FieldOfStudy(Id), 
    PRIMARY KEY (StudentId, FieldOfStudyId)
);
