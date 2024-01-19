CREATE TABLE Student_FieldOfStudy (
    StudentId int NOT NULL FOREIGN KEY REFERENCES Student(Id) ON DELETE CASCADE, 
    FieldOfStudyId int NOT NULL FOREIGN KEY REFERENCES FieldOfStudy(Id) ON DELETE CASCADE, 
    PRIMARY KEY (StudentId, FieldOfStudyId)
);
