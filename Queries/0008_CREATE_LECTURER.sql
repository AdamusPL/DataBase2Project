CREATE TABLE Lecturer (
    Id int IDENTITY(1, 1) NOT NULL,
    UserId int NOT NULL UNIQUE FOREIGN KEY REFERENCES [User](Id),
    PRIMARY KEY (Id)
);
